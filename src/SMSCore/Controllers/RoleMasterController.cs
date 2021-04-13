using Microsoft.AspNetCore.Mvc;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.RoleMasterService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class RoleMasterController:BaseAdminController
    {
        private readonly IRoleMasterService _roleMasterService;

        public RoleMasterController(IRoleMasterService roleMasterService)
        {
            _roleMasterService = roleMasterService;
        }


        // GET: DesignationMasterController
        public async Task<ActionResult> List()
        {
            var designationList = await _roleMasterService.GetAllRolesListAsync();
            if (designationList == null)
                return View(new RoleMasterModel());

            var model = designationList.Select(d => new RoleMasterModel
            {
                Id = d.Id,
                RoleName = d.RoleName,
                Active = d.Active,
                Description = d.Description,
                DateCreated = d.DateCreated
            }).ToList();

            return View(model);
        }


        // GET: DesignationMasterController/Create
        public ActionResult Create()
        {
            return View(new RoleMasterModel());
        }

        // POST: DesignationMasterController/Create
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Create(RoleMasterModel model, bool continueEditing)
        {
            try
            {
                var role = new RoleMaster
                {
                    RoleName = model.RoleName,
                    Active = model.Active,
                    Description=model.Description,
                    DateCreated = DateTime.UtcNow
                };

                await _roleMasterService.InsertAsync(role);

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = role.Id });

                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: DesignationMasterController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var roleInfo = await _roleMasterService.GetRoleByIdAsync(id);
            if (roleInfo == null)
                return RedirectToAction("List");

            var viewModel = new RoleMasterModel
            {
                Id = roleInfo.Id,
                RoleName = roleInfo.RoleName,
                Description= roleInfo.Description,
                Active = roleInfo.Active,
                ContinueEditing = IsContinueEditingAllowed()
            };

            return View(viewModel);
        }

        // POST: DesignationMasterController/Edit/5
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Edit(RoleMasterModel model, bool continueEditing)
        {
            try
            {
                var roleInfo =await _roleMasterService.GetRoleByIdAsync(model.Id);

                if (roleInfo == null)
                    return RedirectToAction("List");

                roleInfo.RoleName = model.RoleName;
                roleInfo.Description = model.Description;
                roleInfo.Active = model.Active;

                await _roleMasterService.UpdateAsync(roleInfo);

                AllowContinueEditing(continueEditing);

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = roleInfo.Id });

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var existingUser =await _roleMasterService.GetRoleByIdAsync(id);

            if (existingUser == null)
                return RedirectToAction("List");

            await _roleMasterService.DaleteAsync(existingUser);

            return RedirectToAction("List");
        }

    }
}
