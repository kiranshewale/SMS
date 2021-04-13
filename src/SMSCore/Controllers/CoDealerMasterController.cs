using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.CoDealerMasterService;
using SMSCore.Services.StateListService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class CoDealerMasterController:BaseAdminController
    {
        private readonly IStateListService _stateListService;
        private readonly ICoDealerMasterService _coDealerMasterService;

        public CoDealerMasterController(IStateListService stateListService,
            ICoDealerMasterService coDealerMasterService)
        {
            _stateListService = stateListService;
            _coDealerMasterService = coDealerMasterService;

        }


        #region Utilities
       
        private async Task<List<SelectListItem>> BindStates()
        {

            var modelList = new CustomerDetailsModel();
            modelList.AvailableStates.Add(new SelectListItem
            {
                Value = "0",
                Text = "Select State"
            });

            var stateList = await _stateListService.GetStateList();
            foreach (var state in stateList)
            {
                modelList.AvailableStates.Add(new SelectListItem
                {
                    Value = state.Id.ToString(),
                    Text = state.State
                });
            }

            return modelList.AvailableStates;
        }

        private IList<CustomerDetailsModel> PrepareCustomerModel(IList<SubDealerMaster> custDetails)
        {
            var custDetailsModel = custDetails.Select(c => new CustomerDetailsModel
            {
                Id = c.Id,
                CustomerName = c.SubDealerName,
                EmailId = c.EmailId,
                MobileNo1 = c.ContactNo1,
                Address1 = c.Address,
                District = c.City,
                DealerCode=c.DealerCode
                
            });
            return custDetailsModel.ToList();
        }
        #endregion
        public async Task<IActionResult> List()
        {
            var model = new CustomerDetailsSearchModel();
            var customerInfo = await _coDealerMasterService.GetAllCoDealerListAsync();
            model.CustomerDetails.AddRange(PrepareCustomerModel(customerInfo));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> List(CustomerDetailsSearchModel searchModel)
        {
            var model = new CustomerDetailsSearchModel();

            var customerInfo = await _coDealerMasterService.GetAllCoDealerListAsync(searchModel.SearchByEmail, searchModel.SearchByMobileNo);

            model.CustomerDetails.AddRange(PrepareCustomerModel(customerInfo));

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new CustomerDetailsModel();
            model.AvailableStates = await BindStates();
            return View(model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(CustomerDetailsModel model, bool continueEditing)
        {
            try
            {
                var existingcustomer = (await _coDealerMasterService.GetAllCoDealerListAsync(model.EmailId, model.MobileNo1.ToString())).FirstOrDefault();
                if (existingcustomer != null)
                {
                    TempData["UserMessageError"] = "Records allready exist.";
                    return await Create();
                }

                var coDealerInfo = new SubDealerMaster
                {
                    
                    SubDealerName = model.CustomerName,
                    EmailId = model.EmailId,
                    ContactNo1 = model.MobileNo1,
                    ContactNo2 = model.MobileNo2,
                    Address = model.Address1,
                    Pincode = model.Pin,
                    City = model.District,
                    State =(await _stateListService.GetStateById(model.StateId)).State,
                    DealerCode=model.DealerCode,
                    DateCreated = DateTime.UtcNow
                };

                await _coDealerMasterService.InsertAsync(coDealerInfo);

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = coDealerInfo.Id });

                TempData["UserMessageSuccess"] = "Record saved sucessfully.";
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                TempData["UserMessageError"] = ex.Message.ToString();
                return RedirectToAction(nameof(Create));
            }

        }

        // GET: DesignationMasterController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var existingcustomer = await _coDealerMasterService.GetAllCoDealerByIdAsync(id);
            if (existingcustomer == null)
                return RedirectToAction("List");

            var viewModel = new CustomerDetailsModel
            {
                Id = existingcustomer.Id,
                CustomerName = existingcustomer.SubDealerName,
                EmailId = existingcustomer.EmailId,
                MobileNo1 = existingcustomer.ContactNo1,
                MobileNo2 = Convert.ToDecimal(existingcustomer.ContactNo2),
                Address1 = existingcustomer.Address,
                Pin = existingcustomer.Pincode,
                District = existingcustomer.City,
                StateId =(await _stateListService.GetStateList()).Where(x=>x.State==existingcustomer.State).FirstOrDefault().Id,
                DealerCode=existingcustomer.DealerCode,
                ContinueEditing = IsContinueEditingAllowed()
            };
            viewModel.AvailableStates = await BindStates();

            return View(viewModel);
        }

        // POST: DesignationMasterController/Edit/5
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Edit(CustomerDetailsModel model, bool continueEditing)
        {
            try
            {
                var existingcustomer = await _coDealerMasterService.GetAllCoDealerByIdAsync(model.Id);

                if (existingcustomer == null)
                    return RedirectToAction("List");

                existingcustomer.SubDealerName = model.CustomerName;
                existingcustomer.EmailId = model.EmailId;
                existingcustomer.ContactNo1 = model.MobileNo1;
                existingcustomer.ContactNo2 = Convert.ToDecimal(model.MobileNo2);
                existingcustomer.Address = model.Address1;
                existingcustomer.Pincode = model.Pin;
                existingcustomer.City = model.District;
                existingcustomer.State = (await _stateListService.GetStateById(model.StateId)).State;
                existingcustomer.DealerCode = model.DealerCode;
                await _coDealerMasterService.UpdateAsync(existingcustomer);

                AllowContinueEditing(continueEditing);
                TempData["UserMessageSuccess"] = "Record saved sucessfully.";

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = existingcustomer.Id });

                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                TempData["UserMessageError"] = ex.Message.ToString();
                return RedirectToAction(nameof(Create));
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var existingUser = await _coDealerMasterService.GetAllCoDealerByIdAsync(id);

            if (existingUser == null)
                return RedirectToAction("List");

            await _coDealerMasterService.DeleteAsync(existingUser);

            return RedirectToAction("List");
        }
    }
}
