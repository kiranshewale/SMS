using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.CustomerService;
using SMSCore.Services.StateListService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class CustomerMasterController : BaseAdminController
    {
        private readonly ICustomerService _customerService;
        private readonly IStateListService _stateListService;

        public CustomerMasterController(ICustomerService customerService,
            IStateListService stateListService)
        {
            _customerService = customerService;
            _stateListService = stateListService;
        }

        #region Utilities
        private List<SelectListItem> BindSalutations()
        {
            var model = new CustomerDetailsModel();

            foreach (SalutationEnum e in Enum.GetValues(typeof(SalutationEnum)))
            {
                model.AvailableSalutations.Add(new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });
            }

            return model.AvailableSalutations;
        }
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

        private IList<CustomerDetailsModel> PrepareCustomerModel(IList<CustomerDetailsTable> custDetails)
        {
            var custDetailsModel = custDetails.Select(c => new CustomerDetailsModel
            {
                Id=c.Id,
                CustomerName = c.CustomerName,
                EmailId = c.EmailId,
                MobileNo1 = c.MobileNo1,
                Address1 = c.Address1,
                PanNo = c.PanNo

            });
            return custDetailsModel.ToList();
        }
        #endregion
        public async Task<IActionResult> List()
        {
            var model = new CustomerDetailsSearchModel();
            var customerInfo = await _customerService.GetAllCustomerListAsync();
            model.CustomerDetails.AddRange(PrepareCustomerModel(customerInfo));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> List(CustomerDetailsSearchModel searchModel)
        {
            var model = new CustomerDetailsSearchModel();

            var customerInfo = await _customerService.GetAllCustomerListAsync(searchModel.SearchByEmail, searchModel.SearchByMobileNo,
                searchModel.SearchByPanNo, searchModel.SearchByAadharNo);

            model.CustomerDetails.AddRange(PrepareCustomerModel(customerInfo));

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new CustomerDetailsModel();
            model.AvailableSalutations = BindSalutations();
            model.AvailableStates = await BindStates();
            return View(model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(CustomerDetailsModel model, bool continueEditing)
        {
            try
            {
                var existingcustomer = (await _customerService.GetAllCustomerListAsync(model.EmailId, model.MobileNo1.ToString(), model.PanNo, model.AdharNo)).FirstOrDefault();
                if (existingcustomer == null)
                {
                    TempData["UserMessageError"] = "Records allready exist.";
                    return View();
                }

                var customerInfo = new CustomerDetailsTable
                {
                    Salutation = model.Salutation,
                    CustomerName = model.CustomerName,
                    EmailId = model.EmailId,
                    MobileNo1 = model.MobileNo1,
                    MobileNo2 = Convert.ToDecimal(model.MobileNo2),
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    Pin = model.Pin,
                    Taluka = model.Taluka,
                    District = model.District,
                    State = model.StateId,
                    PanNo = model.PanNo,
                    AdharNo = model.AdharNo,
                    Gstno = model.Gstno,
                    DateUpdated = DateTime.UtcNow,
                    DateCreated = DateTime.UtcNow
                };

                await _customerService.InsertAsync(customerInfo);

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = customerInfo.Id });

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
            var existingcustomer = await _customerService.GetAllCustomerByIdAsync(id);
            if (existingcustomer == null)
                return RedirectToAction("List");

            var viewModel = new CustomerDetailsModel
            {
                Id = existingcustomer.Id,
                Salutation = existingcustomer.Salutation,
                CustomerName = existingcustomer.CustomerName,
                EmailId = existingcustomer.EmailId,
                MobileNo1 = existingcustomer.MobileNo1,
                MobileNo2 = Convert.ToDecimal(existingcustomer.MobileNo2),
                Address1 = existingcustomer.Address1,
                Address2 = existingcustomer.Address2,
                Pin = existingcustomer.Pin,
                Taluka = existingcustomer.Taluka,
                District = existingcustomer.District,
                StateId =(int) existingcustomer.State,
                PanNo = existingcustomer.PanNo,
                AdharNo = existingcustomer.AdharNo,
                Gstno = existingcustomer.Gstno,
                ContinueEditing = IsContinueEditingAllowed()
            };
            viewModel.AvailableSalutations = BindSalutations();
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
                var existingcustomer = await _customerService.GetAllCustomerByIdAsync(model.Id);

                if (existingcustomer == null)
                    return RedirectToAction("List");

                existingcustomer.Salutation = model.Salutation;
                existingcustomer.CustomerName = model.CustomerName;
                existingcustomer.EmailId = model.EmailId;
                existingcustomer.MobileNo1 = model.MobileNo1;
                existingcustomer.MobileNo2 = Convert.ToDecimal(model.MobileNo2);
                existingcustomer.Address1 = model.Address1;
                existingcustomer.Address2 = model.Address2;
                existingcustomer.Pin = model.Pin;
                existingcustomer.Taluka = model.Taluka;
                existingcustomer.District = model.District;
                existingcustomer.State = model.StateId;
                existingcustomer.PanNo = model.PanNo;
                existingcustomer.AdharNo = model.AdharNo;
                existingcustomer.Gstno = model.Gstno;
                existingcustomer.DateUpdated = DateTime.UtcNow;

                await _customerService.UpdateAsync(existingcustomer);

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
            var existingUser =await _customerService.GetAllCustomerByIdAsync(id);

            if (existingUser == null)
                return RedirectToAction("List");

            await _customerService.DeleteAsync(existingUser);

            return RedirectToAction("List");
        }
    }
}
