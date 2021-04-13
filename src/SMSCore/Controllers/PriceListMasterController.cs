using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.PriceListMasterService;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class PriceListMasterController : BaseAdminController
    {
        private readonly IPriceListMasterService _priceListMasterService;

        public PriceListMasterController(IPriceListMasterService priceListMasterService)
        {
            _priceListMasterService = priceListMasterService;
        }

        #region Utilities
        private async Task<PriceListMaster> PrepareRepositoryTable(DataRow dataRow)
        {
            var modelCode = Convert.ToString(dataRow["ModelCode"]);
            var varient = Convert.ToString(dataRow["Varient"]);
            var varientType = Convert.ToString(dataRow["VarientType"]);
            if (string.IsNullOrEmpty(modelCode) || string.IsNullOrEmpty(varientType))
                return null;

            var isModelPriceExist = await _priceListMasterService.GetModelPriceByModelCodeAsync(modelCode, varient,varientType);
            if (isModelPriceExist != null)
                return null;

            var modelPrice = new PriceListMaster();
            if (dataRow.Table.Columns.Contains("ModelCode"))
                modelPrice.ModelCode = Convert.ToString(dataRow["ModelCode"]);

            if (dataRow.Table.Columns.Contains("Varient"))
                modelPrice.ModelVarient = Convert.ToString(dataRow["Varient"]);

            if (dataRow.Table.Columns.Contains("VarientType"))
                modelPrice.VarientType = Convert.ToString(dataRow["VarientType"]);

            if (dataRow.Table.Columns.Contains("ExShowroomPrice"))
                modelPrice.ExShowroomPrice = Convert.ToDecimal(dataRow["ExShowroomPrice"]);

            if (dataRow.Table.Columns.Contains("FastTag"))
                modelPrice.FastTag = Convert.ToDecimal(dataRow["FastTag"]);

            if (dataRow.Table.Columns.Contains("TCS"))
                modelPrice.Tcs = Convert.ToDecimal(dataRow["TCS"]);

            if (dataRow.Table.Columns.Contains("HAInsurance"))
                modelPrice.Hainsurance = Convert.ToDecimal(dataRow["HAInsurance"]);

            if (dataRow.Table.Columns.Contains("HANilDepWithKeyProtect"))
                modelPrice.HanilDepWithKeyProtect = Convert.ToDecimal(dataRow["HANilDepWithKeyProtect"]);

            if (dataRow.Table.Columns.Contains("HARTIWithEngineProtect"))
                modelPrice.HartiwithEngineProtect = Convert.ToDecimal(dataRow["HARTIWithEngineProtect"]);

            if (dataRow.Table.Columns.Contains("RTOIndividual"))
                modelPrice.Rtoindividual = Convert.ToDecimal(dataRow["RTOIndividual"]);

            if (dataRow.Table.Columns.Contains("AccessoryComboKit"))
                modelPrice.AccessoryComboKit = Convert.ToDecimal(dataRow["AccessoryComboKit"]);

            if (dataRow.Table.Columns.Contains("ExtendedWarranty"))
                modelPrice.ExtendedWarranty = Convert.ToDecimal(dataRow["ExtendedWarranty"]);

            if (dataRow.Table.Columns.Contains("RSA"))
                modelPrice.Rsa = Convert.ToDecimal(dataRow["RSA"]);

            if (dataRow.Table.Columns.Contains("ClayBar"))
                modelPrice.ClayBar = Convert.ToDecimal(dataRow["ClayBar"]);

            if (dataRow.Table.Columns.Contains("Antirust"))
                modelPrice.Antirust = Convert.ToDecimal(dataRow["Antirust"]);

            if (dataRow.Table.Columns.Contains("CarpetLamination"))
                modelPrice.CarpetLamination = Convert.ToDecimal(dataRow["CarpetLamination"]);

            if (dataRow.Table.Columns.Contains("TotatPrice"))
                modelPrice.TotatPrice = Convert.ToDecimal(dataRow["TotatPrice"]);




            modelPrice.DateCreated = DateTime.UtcNow;
            modelPrice.DateUpdated = DateTime.UtcNow;
            modelPrice.ByUser = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value); ;
            return modelPrice;
        }
        #endregion
        public async Task<IActionResult> List()
        {
            var model = new PriceListModel();

            var modelPriceList = await _priceListMasterService.GetAllPriceListAsync();
            if (modelPriceList == null)
                return View(model);

            var priceModel = modelPriceList.Select(v => new PriceListDetailsModel
            {
                Id = v.Id,
                ModelCode = v.ModelCode,
                ModelVarient = v.ModelVarient,
                VarientType = v.VarientType,
                ExShowroomPrice = v.ExShowroomPrice,
                FastTag = v.FastTag,
                Tcs = v.Tcs,
                Hainsurance = v.Hainsurance,
                HanilDepWithKeyProtect = v.HanilDepWithKeyProtect,
                HartiwithEngineProtect = v.HartiwithEngineProtect,
                Rtoindividual = v.Rtoindividual,
                AccessoryComboKit = v.AccessoryComboKit,
                ExtendedWarranty = v.ExtendedWarranty,
                Rsa = v.Rsa,
                ClayBar = v.ClayBar,
                Antirust = v.Antirust,
                CarpetLamination = v.CarpetLamination,
                TotatPrice = v.TotatPrice

            }).ToList();

            model.PriceList.AddRange(priceModel);
            return View(model);
        }



        public IActionResult ImportPriceListData()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> ImportPriceListData(IFormFile importexcelfile)
        {
            try
            {
                if (importexcelfile != null && importexcelfile.Length > 0)
                {
                    var excelRecords = _priceListMasterService.GetPriceListDataFromExcelFile(importexcelfile.OpenReadStream());

                    if (excelRecords.Rows.Count > 0)
                    {
                        for (int i = 0; i < excelRecords.Rows.Count; i++)
                        {
                            var vehicleRecord = await PrepareRepositoryTable(excelRecords.Rows[i]);
                            if (vehicleRecord != null)
                                await _priceListMasterService.InsertAsync(vehicleRecord);
                        }
                    }
                }
                else
                {
                    TempData["UserMessageError"] = "File is empty";
                    return RedirectToAction(nameof(PriceListMasterController.ImportPriceListData));
                }

                TempData["UserMessageSuccess"] = "Vehicles data imported sucessfully.";
                return RedirectToAction(nameof(PriceListMasterController.List));
            }
            catch (Exception exc)
            {
                // TempData["UserMessageError"] = exc.Message.ToString();
                throw exc;
                // return RedirectToAction("ImportProductData");
            }
        }
    }
}
