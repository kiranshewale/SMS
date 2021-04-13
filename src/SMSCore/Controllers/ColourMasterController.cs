using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.ColourMasterService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class ColourMasterController:BaseAdminController
    {
        private readonly IColourMasterService _colourMasterService;

        public ColourMasterController(IColourMasterService colourMasterService )
        {
            _colourMasterService = colourMasterService;
        }


        // GET: ModelMasterController
        public async Task<ActionResult> List()
        {
            var coloursList = await _colourMasterService.GetAllColoursListAsync();
            if (coloursList == null)
                return View(new ColourMasterModel());

            var model = coloursList.Select(d => new ColourMasterModel
            {
                Id = d.Id,
                ColourCode=d.ColourCode,
                ColourName=d.ColourName,
                ColourType=d.ColourType,               
                FriendlyName = d.FriendlyName,
                Active = d.Active,
                DateUpdated = d.DateUpdated,
                DateCreated = d.DateCreated
            }).ToList();

            return View(model);
        }


        // GET: ModelMasterController/Create
        public ActionResult Create()
        {
            var model = new ColourMasterModel();
            foreach (VarientTypeEnum e in Enum.GetValues(typeof(VarientTypeEnum)))
            {
                model.AvaillabelVarientTypes.Add(new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });
            }
            return View(model);
        }

        // POST: ModelMasterController/Create
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Create(ColourMasterModel model, bool continueEditing)
        {
            try
            {
                var colourInfo = new ColourMaster
                {
                    ColourCode = model.ColourCode,
                    ColourName = model.ColourName,
                    ColourType=model.ColourType,
                    FriendlyName = model.FriendlyName,
                    Active = model.Active,
                    Deleted = false,
                    DateUpdated = DateTime.UtcNow,
                    DateCreated = DateTime.UtcNow,
                    ByUser = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value)
                };

                await _colourMasterService.InsertAsync(colourInfo);

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = colourInfo.Id });

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
        public ActionResult Edit(int id)
        {
            var colourInfo = _colourMasterService.GetColourByIdAsync(id);
            if (colourInfo == null)
                return RedirectToAction("List");

            var viewModel = new ColourMasterModel
            {
                Id = colourInfo.Id,
                ColourCode = colourInfo.ColourCode,
                ColourName=colourInfo.ColourName,
                ColourType=colourInfo.ColourType,              
                FriendlyName = colourInfo.FriendlyName,
                Active = colourInfo.Active,
                ContinueEditing = IsContinueEditingAllowed()
            };

            foreach (VarientTypeEnum e in Enum.GetValues(typeof(VarientTypeEnum)))
            {
                viewModel.AvaillabelVarientTypes.Add(new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });
            }

            return View(viewModel);
        }

        // POST: ModelMasterController/Edit/5
        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<ActionResult> Edit(ColourMasterModel model, bool continueEditing)
        {
            try
            {
                var colourInfo = _colourMasterService.GetColourByIdAsync(model.Id);

                if (colourInfo == null)
                    return RedirectToAction("List");

                colourInfo.ColourCode = model.ColourCode;
                colourInfo.ColourName = model.ColourName;
                colourInfo.ColourType = model.ColourType;
                colourInfo.FriendlyName = model.FriendlyName;
                colourInfo.Active = model.Active;
                colourInfo.DateUpdated = DateTime.UtcNow;
                colourInfo.ByUser = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
                await _colourMasterService.UpdateAsync(colourInfo);

                AllowContinueEditing(continueEditing);
                TempData["UserMessageSuccess"] = "Record saved sucessfully.";

                if (continueEditing)
                    return RedirectToAction(nameof(Edit), new { id = colourInfo.Id });

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
            var existingColour = _colourMasterService.GetColourByIdAsync(id);

            if (existingColour == null)
                return RedirectToAction("List");

            await _colourMasterService.DeleteAsync(existingColour);

            return RedirectToAction("List");
        }
    }
}
