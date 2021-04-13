using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.DesignationMasterService;
using SMSCore.Services.UserMasterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class UserMasterController:BaseAdminController
    {
        private readonly IUserMasterService _userMasterService;
        private readonly IDesignationMasterService _designationMasterService;

        public UserMasterController(IUserMasterService userMasterService,
            IDesignationMasterService designationMasterService)
        {
            _userMasterService = userMasterService;
            _designationMasterService = designationMasterService;

        }


        #region Utilities
        private async Task<List<SelectListItem>> BindDesignationsList()
        {
            var model = new UserMasterModel();
            model.AvailableDesignations.Add(new SelectListItem
            {
                Value = "0",
                Text = "Select Designation"
            });
            var designationsList =await _designationMasterService.GetAllDesignationsListAsync();
            foreach (var item in designationsList)
            {
                model.AvailableDesignations.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Designation
                });
            }

            return model.AvailableDesignations;
        }
        private async Task<List<SelectListItem>> BindSMandTLList()
        {
            var model = new UserMasterModel();
            model.AvailableUnderDesIds.Add(new SelectListItem
            {
                Value = "0",
                Text = "Select Under This SM/TL"
            });
          
            var usersList = await _userMasterService.GetSMandTLList();
            foreach (var item in usersList)
            {
                model.AvailableUnderDesIds.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.FirstName+" "+ item.LastName
                });
            }
            return model.AvailableUnderDesIds;
        }
        #endregion


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var userList = await _userMasterService.GetAllUsers();
            var model = userList.Select(u => new UserMasterModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.EmailId,
                Designation = _designationMasterService.GetDesignationbyIdAsync(u.DesignationId).Designation,
                ContactNumber = u.ContactNumber
            }).ToList();

            return View(model);
        }


        public async Task<IActionResult> Create()
        {
            var model = new UserMasterModel();
            model.AvailableDesignations = await BindDesignationsList();
            model.AvailableUnderDesIds =await BindSMandTLList();
            return View(model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(UserMasterModel viewModel, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var userModel = new UserMaster
                {
                    UserGuid=Guid.NewGuid(),
                    FirstName=viewModel.FirstName,
                    LastName = viewModel.LastName,
                    EmailId=viewModel.Email,
                    ContactNumber=viewModel.ContactNumber,
                    Address=viewModel.Address,
                    DesignationId=viewModel.DesignationId,
                    UnderDesId=viewModel.UnderDesId,
                    UserName=viewModel.UserName,
                    Password=viewModel.Password,
                    DateCreated= DateTime.UtcNow,                 
                };

                await _userMasterService.InsertAsync(userModel);

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = userModel.Id });

                return RedirectToAction(nameof(List));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var userInfo = await _userMasterService.GetUserByIdAsync(id);
            if (userInfo == null)
                return RedirectToAction("List");

            var viewModel = new UserMasterModel
            {
                Id = userInfo.Id,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Email = userInfo.EmailId,
                ContactNumber = userInfo.ContactNumber,
                Address = userInfo.Address,
                DesignationId=userInfo.DesignationId,
                UnderDesId = userInfo.UnderDesId,
                UserName=userInfo.UserName,
                Password=userInfo.Password,               
                ContinueEditing = IsContinueEditingAllowed()
            };

            viewModel.AvailableDesignations =await BindDesignationsList();
            viewModel.AvailableUnderDesIds =await BindSMandTLList();

            return View(viewModel);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(UserMasterModel model, bool continueEditing)
        {

            var userInfo = await _userMasterService.GetUserByIdAsync(model.Id);

            if (userInfo == null)
                return RedirectToAction("List");

            userInfo.FirstName = model.FirstName;
            userInfo.LastName = model.LastName;
            userInfo.EmailId = model.Email;
            userInfo.ContactNumber = model.ContactNumber;
            userInfo.Address = model.Address;
            userInfo.DesignationId = model.DesignationId;
            userInfo.UnderDesId = model.UnderDesId;
            userInfo.DateCreated = DateTime.UtcNow;
            await _userMasterService.UpdateAsync(userInfo);

            AllowContinueEditing(continueEditing);

            if (continueEditing)
                return RedirectToAction(nameof(UserMasterController.Edit), new { id = userInfo.Id });

            return RedirectToAction(nameof(UserMasterController.List));
           
        }

        public async Task<ActionResult> Delete(int id)
        {
            var existingUser = await _userMasterService.GetUserByIdAsync(id);
            
            if (existingUser == null)
                return RedirectToAction("List");

             await _userMasterService.DelateAsync(existingUser);

            return RedirectToAction("List");
        }
    }
}
