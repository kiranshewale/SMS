using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.ModelMasterService;
using SMSCore.Services.VarientMasterService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class ModelMasterController : BaseAdminController
    {
        private readonly IModelMasterService _modelMasterService;
        private readonly IVarientMasterService _varientMasterService;

        public ModelMasterController(IModelMasterService modelMasterService,
            IVarientMasterService varientMasterService)
        {
            _modelMasterService = modelMasterService;
            _varientMasterService = varientMasterService;
        }

        // GET: ModelMasterController
        public async Task<ActionResult> List()
        {
            var modeList = await _modelMasterService.GetAllModelsListAsync();
            if (modeList == null)
                return View(new ModelsListModel());

            var model = modeList.Select(d => new ModelsListModel
            {
                Id = d.Id,
                ModelCode = d.ModelCode,
                ModelName = d.ModelName,
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
            var model = new ModelsListModel();
            var varientsList = await _varientMasterService.GetAllVarientsListAsync();
            foreach (var item in varientsList)
            {
                model.AvaillabelVarientsList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.VarientCode
                });
            }
            return View(new ModelsListModel());
        }

        // POST: ModelMasterController/Create
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Create(ModelsListModel model, bool continueEditing)
        {
            try
            {
                var modelInfo = new ModelMaster
                {
                    ModelCode = model.ModelCode,
                    ModelName = model.ModelName,
                    FriendlyName = model.FriendlyName,
                    Active = model.Active,
                    Deleted = false,
                    DateUpdated = DateTime.UtcNow,
                    DateCreated = DateTime.UtcNow,
                    ByUser = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value)
                };

                await _modelMasterService.InsertAsync(modelInfo);

                //add model varient mapping
                foreach (var varMap in model.SelectedVarientIds)
                {
                    await _modelMasterService.AddVarientMappingAsync(new ModelVarientMapping
                    {
                        ModelId = modelInfo.Id,
                        VarientId = varMap
                    });
                }

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = modelInfo.Id });

                TempData["UserMessageSuccess"] = "Record saved sucessfully.";
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                TempData["UserMessageError"] = ex.Message.ToString();
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: ModelMasterController/Edit/5
        public async Task<ActionResult> Edit(int id)
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
                SelectedVarientIds = (await _modelMasterService.GetModelVarientMappingByModelId(modelInfo.Id)).Select(x => x.VarientId).ToArray(),
                Active = modelInfo.Active,
                ContinueEditing = IsContinueEditingAllowed()
            };

            var varientsList = await _varientMasterService.GetAllVarientsListAsync();
            foreach (var item in varientsList)
            {
                viewModel.AvaillabelVarientsList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.VerientName
                });
            }
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

                if (model.SelectedVarientIds.Length > 0)
                {
                    //delete old mapping
                    await _modelMasterService.RemoveVarientMappingByModelAsync(await _modelMasterService.GetModelVarientMappingByModelId(modelInfo.Id));
                    foreach (var varMap in model.SelectedVarientIds)
                    {
                        await _modelMasterService.AddVarientMappingAsync(new ModelVarientMapping
                        {
                            ModelId = modelInfo.Id,
                            VarientId = varMap
                        });
                    }
                }

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
    }
}
