using Microsoft.AspNetCore.Mvc;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.DesignationMasterService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class DesignationMasterController : BaseAdminController
    {
        private readonly IDesignationMasterService _designationMasterService;

        public DesignationMasterController(IDesignationMasterService designationMasterService)
        {
            _designationMasterService = designationMasterService;
        }


        // GET: DesignationMasterController
        public async Task<ActionResult> List()
        {
            var designationList = await _designationMasterService.GetAllDesignationsListAsync();
            if (designationList == null)
                return View(new DesignationMasterModel());

            var model = designationList.Select(d => new DesignationMasterModel
            {
                Id=d.Id,
                Designation=d.Designation,
                Active=Convert.ToBoolean(d.Active),
                DateUpdated=d.DateUpdated,
                DateCreated=d.DateCreated
            }).ToList();

            return View(model);
        }

       
        // GET: DesignationMasterController/Create
        public ActionResult Create()
        {
            return View(new DesignationMasterModel());
        }

        // POST: DesignationMasterController/Create
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Create(DesignationMasterModel model, bool continueEditing)
        {
            try
            {
                var designation=new DesignationMaster{ 
                    Designation=model.Designation,
                    Active=model.Active,
                    Deleted=false,
                    DateUpdated=DateTime.UtcNow,
                    DateCreated=DateTime.UtcNow
                };

                await _designationMasterService.InsertAsync(designation);

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = designation.Id });

                TempData["UserMessageSuccess"] = "Record saved sucessfully.";
                return RedirectToAction(nameof(List));
            }
            catch(Exception ex)
            {
                TempData["UserMessageError"] = ex.Message.ToString();
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: DesignationMasterController/Edit/5
        public ActionResult Edit(int id)
        {
            var designationInfo = _designationMasterService.GetDesignationbyIdAsync(id);
            if (designationInfo == null)
                return RedirectToAction("List");

            var viewModel = new DesignationMasterModel
            {
                Id = designationInfo.Id,
                Designation = designationInfo.Designation,
                Active = Convert.ToBoolean(designationInfo.Active),
                ContinueEditing = IsContinueEditingAllowed()
            };

            return View(viewModel);
        }

        // POST: DesignationMasterController/Edit/5
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Edit(DesignationMasterModel model, bool continueEditing)
        {
            try
            {
                var designationInfo = _designationMasterService.GetDesignationbyIdAsync(model.Id);

                if (designationInfo == null)
                    return RedirectToAction("List");

                designationInfo.Designation = model.Designation;
                designationInfo.Active = model.Active;
                designationInfo.DateUpdated = DateTime.UtcNow;
                await _designationMasterService.UpdateAsync(designationInfo);

                AllowContinueEditing(continueEditing);
                TempData["UserMessageSuccess"] = "Record saved sucessfully.";

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = designationInfo.Id });
                
                return RedirectToAction(nameof(List));
            }
            catch(Exception ex)
            {
                TempData["UserMessageError"] = ex.Message.ToString();
                return RedirectToAction(nameof(Create));
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var existingUser = _designationMasterService.GetDesignationbyIdAsync(id);

            if (existingUser == null)
                return RedirectToAction("List");

            await _designationMasterService.DeleteAsync(existingUser);

            return RedirectToAction("List");
        }
    }
}
