using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.ColourMasterService;
using SMSCore.Services.VarientMasterService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class VarientMasterController : BaseAdminController
    {
        private readonly IVarientMasterService _verientMasterService;
        private readonly IColourMasterService _colourMasterService;

        public VarientMasterController(IVarientMasterService verientMasterService,
            IColourMasterService colourMasterService)
        {
            _verientMasterService = verientMasterService;
            _colourMasterService = colourMasterService;
        }


        // GET: ModelMasterController
        public async Task<ActionResult> List()
        {
            var varientsList = await _verientMasterService.GetAllVarientsListAsync();
            if (varientsList == null)
                return View(new VarientMasterModel());

            var model = varientsList.Select(d => new VarientMasterModel
            {
                Id = d.Id,
                VarientCode=d.VarientCode,
                VerientName=d.VerientName,
                FriendlyName = d.FriendlyName,
                Active = d.Active,
                Deleted = d.Deleted,
                DateUpdated = d.DateUpdated,
                DateCreated = d.DateCreated
            }).ToList();

            return View(model);
        }


        // GET: ModelMasterController/Create
        public async Task<ActionResult> Create()
        {
            var model = new VarientMasterModel();
            var colourList = await _colourMasterService.GetAllColoursListAsync();
            foreach (var item in colourList)
            {
                model.AvaillabelColoursList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.ColourName
                });
            }
            return View(model);
        }

        // POST: ModelMasterController/Create
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Create(VarientMasterModel model, bool continueEditing)
        {
            try
            {
                var varientInfo = new VarientMaster
                {
                    VarientCode = model.VarientCode,
                    VerientName = model.VerientName,
                    FriendlyName = model.FriendlyName,
                    Active = model.Active,
                    Deleted = false,
                    DateUpdated = DateTime.UtcNow,
                    DateCreated = DateTime.UtcNow,
                    ByUser = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value)
                };

                await _verientMasterService.InsertAsync(varientInfo);

                foreach (var varColorMap in model.SelectedColourIds)
                {
                   await _verientMasterService.AddMappingAsync(new ModelVarientColourMapping {
                    VarientId= varientInfo.Id,
                    ColourId=varColorMap});
                }
                

                //if (continueEditing)
                //    return RedirectToAction(nameof(Edit), new { id = modelInfo.Id });

                TempData["UserMessageSuccess"] = "Record saved sucessfully.";
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                TempData["UserMessageError"] = ex.Message.ToString();
                return RedirectToAction(nameof(Create));
            }
        }
/*
        // GET: ModelMasterController/Edit/5
        public ActionResult Edit(int id)
        {
            var modelInfo = _modelMasterService.GetModelbyIdAsync(id);
            if (modelInfo == null)
                return RedirectToAction("List");

            var viewModel = new ModelsListModel
            {
                Id = modelInfo.Id,
                ModelCode = modelInfo.ModelCode,
                ModelName = modelInfo.ModelName,
                FriendlyName = modelInfo.FriendlyName,
                Active = modelInfo.Active,
                ContinueEditing = IsContinueEditingAllowed()
            };

            return View(viewModel);
        }

        // POST: ModelMasterController/Edit/5
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Edit(ModelsListModel model, bool continueEditing)
        {
            try
            {
                var modelInfo = _modelMasterService.GetModelbyIdAsync(model.Id);

                if (modelInfo == null)
                    return RedirectToAction("List");

                modelInfo.ModelCode = model.ModelCode;
                modelInfo.ModelName = model.ModelName;
                modelInfo.FriendlyName = model.FriendlyName;
                modelInfo.Active = model.Active;
                modelInfo.DateUpdated = DateTime.UtcNow;
                modelInfo.ByUser = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
                await _modelMasterService.UpdateAsync(modelInfo);

                AllowContinueEditing(continueEditing);
                TempData["UserMessageSuccess"] = "Record saved sucessfully.";

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = modelInfo.Id });

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
            var existingModel = _modelMasterService.GetModelbyIdAsync(id);

            if (existingModel == null)
                return RedirectToAction("List");

            await _modelMasterService.DeleteAsync(existingModel);

            return RedirectToAction("List");
        }
        */
    }
}
