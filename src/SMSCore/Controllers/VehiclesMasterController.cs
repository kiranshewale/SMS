using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.ColourMasterService;
using SMSCore.Services.ModelMasterService;
using SMSCore.Services.VarientMasterService;
using SMSCore.Services.VehiclesMasterService;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class VehiclesMasterController:BaseAdminController
    {
        private readonly IVehiclesMasterService _vehiclesMasterService;
        private readonly IModelMasterService _modelMasterService;
        private readonly IVarientMasterService _varientMasterService;
        private readonly IColourMasterService _colourMasterService;

        public VehiclesMasterController(IVehiclesMasterService vehiclesMasterService,
            IModelMasterService modelMasterService,
            IVarientMasterService varientMasterService,
            IColourMasterService colourMasterService)
        {
            _vehiclesMasterService = vehiclesMasterService;
            _modelMasterService = modelMasterService;
            _varientMasterService = varientMasterService;
            _colourMasterService = colourMasterService;
        }


        #region Utilities
        private async Task<VehiclesMaster> PrepareRepositoryTable(DataRow dataRow)
        {
            var modelId = _modelMasterService.GetModelbyModelNameAsync(Convert.ToString(dataRow["ModelName"])).Id;
            var varientId =_varientMasterService.GetVarientByNameAsync(Convert.ToString(dataRow["ModelVarient"])).Id;
            var colourId =_colourMasterService.GetColourByNameAsync( Convert.ToString(dataRow["ModelColour"])).Id;



            var isVehicleExist =await _vehiclesMasterService.GetVehicleDetailsByChassisnoAsync(Convert.ToString(dataRow["VINNumber"]));
            if (isVehicleExist != null)
                return null;

            var vehicle = new VehiclesMaster();
            if (dataRow.Table.Columns.Contains("HCILInvoiceDate"))
                vehicle.HcilinvoiceDate =Convert.ToDateTime(dataRow["HCILInvoiceDate"]);

            if (dataRow.Table.Columns.Contains("UnloadDate"))
                vehicle.UnloadDate = Convert.ToDateTime(dataRow["UnloadDate"]);

            if (dataRow.Table.Columns.Contains("AgingDays"))
                vehicle.AgingDays =0;

            if (dataRow.Table.Columns.Contains("VINNumber"))
                vehicle.Vinnumber = Convert.ToString(dataRow["VINNumber"]);

            if (dataRow.Table.Columns.Contains("EngineNumber"))
                vehicle.EngineNumber = dataRow["EngineNumber"].ToString();

            if (dataRow.Table.Columns.Contains("KeyNumber"))
                vehicle.KeyNumber = dataRow["KeyNumber"].ToString();

            if (dataRow.Table.Columns.Contains("ModelName"))
                vehicle.ModelName = dataRow["ModelName"].ToString();

            if (dataRow.Table.Columns.Contains("ModelVarient"))
                vehicle.ModelVarient = dataRow["ModelVarient"].ToString();

            if (dataRow.Table.Columns.Contains("ModelColour"))
                vehicle.ModelColour = dataRow["ModelColour"].ToString();

            if (dataRow.Table.Columns.Contains("ManufactureYear"))
                vehicle.ManufactureYear =Convert.ToInt32(dataRow["ManufactureYear"]);

            if (dataRow.Table.Columns.Contains("Pdi"))
                vehicle.Pdi = dataRow["Pdi"].ToString()=="Y"?true:false;

            if (dataRow.Table.Columns.Contains("InvoiceNumber"))
                vehicle.InvoiceNumber = Convert.ToString(dataRow["InvoiceNumber"]);

            if (dataRow.Table.Columns.Contains("TransporterName"))
                vehicle.TransporterName = dataRow["TransporterName"].ToString();

            if (dataRow.Table.Columns.Contains("TruckRegistrationNumber"))
                vehicle.TruckRegistrationNumber = dataRow["TruckRegistrationNumber"].ToString();

            if (dataRow.Table.Columns.Contains("DealerCode"))
                vehicle.DealerCode = Convert.ToString(dataRow["DealerCode"]);

            //if (dataRow.Table.Columns.Contains("VehicleStatus"))
                vehicle.VehicleStatus = "InStock";

            if (dataRow.Table.Columns.Contains("DiscpuntAsPerHonda"))
                vehicle.DiscountAsPerHonda =Convert.ToDecimal(dataRow["DiscpuntAsPerHonda"]);

            if (dataRow.Table.Columns.Contains("PurchaseAmount"))
                vehicle.PurchaseAmount = Convert.ToDecimal(dataRow["PurchaseAmount"]);

            if (dataRow.Table.Columns.Contains("Igst"))
                vehicle.Igst = Convert.ToDecimal(dataRow["Igst"]);

            if (dataRow.Table.Columns.Contains("Cgst"))
                vehicle.Cgst = Convert.ToDecimal(dataRow["Cgst"]);

            if (dataRow.Table.Columns.Contains("Sgst"))
                vehicle.Sgst = Convert.ToDecimal(dataRow["Sgst"]);

            if (dataRow.Table.Columns.Contains("Cess"))
                vehicle.Cess = Convert.ToDecimal(dataRow["Cess"]);

            if (dataRow.Table.Columns.Contains("GrossAmount"))
                vehicle.GrossAmount = Convert.ToDecimal(dataRow["GrossAmount"]);

            if (dataRow.Table.Columns.Contains("Ugst"))
                vehicle.Ugst = Convert.ToDecimal(dataRow["Ugst"]);

            if (dataRow.Table.Columns.Contains("TradeDiscount"))
                vehicle.TradeDiscount = Convert.ToDecimal(dataRow["TradeDiscount"]);

            vehicle.ModelId = modelId;
            vehicle.VarientId = varientId;
            vehicle.ColourId = colourId;
            vehicle.DateCreated = DateTime.UtcNow;
            vehicle.DateUpdated = DateTime.UtcNow;
            vehicle.CreatedOrUpdatedBy = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value); 
            return vehicle;
        }
        #endregion
        public async Task<IActionResult> List()
        {
            var vehiclesList = await _vehiclesMasterService.GetAllVehicleDetailsAsync();
            if (vehiclesList == null)
                return View(new VehiclesMasterModel());

            var model = vehiclesList.Select(v => new VehiclesMasterModel
            {
                Id = v.Id,
               HcilinvoiceDate=v.HcilinvoiceDate,
               UnloadDate=v.UnloadDate,
               AgingDays=v.AgingDays,
               Vinnumber=v.Vinnumber,
               EngineNumber=v.EngineNumber,
               KeyNumber=v.KeyNumber,
               ModelName=v.ModelName,
               ModelVarient=v.ModelVarient,
               ModelColour=v.ModelColour,
               InvoiceNumber=v.InvoiceNumber

            }).ToList();

            return View(model);
        }



        public IActionResult ImportVehiclesData()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> ImportVehiclesData(IFormFile importexcelfile)
        {
            try
            {
                if (importexcelfile != null && importexcelfile.Length > 0)
                {
                    var excelRecords = _vehiclesMasterService.GetVehiclesDataFromExcelFile(importexcelfile.OpenReadStream());

                    if (excelRecords.Rows.Count > 0)
                    {
                        for (int i = 0; i < excelRecords.Rows.Count; i++)
                        {
                            var vehicleRecord =await PrepareRepositoryTable(excelRecords.Rows[i]);
                            if (vehicleRecord != null)
                               await _vehiclesMasterService.InsertAsync(vehicleRecord);
                        }
                    }
                }
                else
                {
                    TempData["UserMessageError"] = "File is empty" ;
                    return RedirectToAction(nameof(VehiclesMasterController.ImportVehiclesData));
                }

                TempData["UserMessageSuccess"] ="Vehicles data imported sucessfully.";
                return RedirectToAction(nameof(VehiclesMasterController.List));
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
