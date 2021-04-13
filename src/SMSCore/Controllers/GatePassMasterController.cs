using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services;
using SMSCore.Services.BranchMasterService;
using SMSCore.Services.CustomerService;
using SMSCore.Services.GatePassService;
using SMSCore.Services.PDIVehiclesService;
using SMSCore.Services.ShowroomVehiclesService;
using SMSCore.Services.UserMasterService;
using SMSCore.Services.VehicleAllotmentService;
using SMSCore.Services.VehiclesMasterService;
using SMSCore.Services.ViewToStringService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class GatePassMasterController : BaseAdminController
    {
        private readonly IVehicleAllotmentService _vehicleAllotmentService;
        private readonly IVehiclesMasterService _vehicleMasterService;
        private readonly IUserMasterService _userMasterService;
        private readonly ICustomerService _customerService;
        private readonly IBranchMasterService _branchMasterService;
        private readonly IViewToStringRendererService _viewToStringRendererService;
        private readonly IShowroomVehiclesService _showroomVehiclesService;
        private readonly IPDIVehiclesService _pdiVehicesService;
        private readonly IGatePassService _gatePassService;

        public GatePassMasterController(IVehicleAllotmentService vehicleAllotmentService,
            IVehiclesMasterService vehicleMasterService,
            IUserMasterService userMasterService,
            ICustomerService customerService,
            IBranchMasterService branchMasterService,
            IViewToStringRendererService viewToStringRendererService,
            IShowroomVehiclesService showroomVehiclesService,
            IPDIVehiclesService pdiVehicesService,
            IGatePassService gatePassService)
        {
            _vehicleAllotmentService = vehicleAllotmentService;
            _vehicleMasterService = vehicleMasterService;
            _userMasterService = userMasterService;
            _customerService = customerService;
            _branchMasterService = branchMasterService;
            _viewToStringRendererService = viewToStringRendererService;
            _showroomVehiclesService = showroomVehiclesService;
            _pdiVehicesService = pdiVehicesService;
            _gatePassService = gatePassService;
        }

        #region Utilities
        private List<SelectListItem> BindVehicleOutTo()
        {
            var model = new GatePassModel();
            model.AvaillablePlaces.Add(new SelectListItem
            {
                Value = "0",
                Text = " Select "
            });
            foreach (VehicleOutToEnum e in Enum.GetValues(typeof(VehicleOutToEnum)))
            {
                model.AvaillablePlaces.Add(new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });
            }

            return model.AvaillablePlaces;
        }
        #endregion


        public async Task<IActionResult> List()
        {

            var list = new GatePassModel();
            var allotedVehicesList = await _vehicleAllotmentService.GetAllAllotedVehiclesAsync();
            if (allotedVehicesList != null)
            {
                foreach (var item in allotedVehicesList)
                {
                    var vehicle = await _vehicleMasterService.GetByIdAsync((int)item.VehicleId);
                    var sc = await _userMasterService.GetUserByIdAsync((int)item.Scid);
                    var model = new AllotedVehicleDetailsModel();
                    model.AllotId = item.Id;
                    model.Model = vehicle.ModelVarient;
                    model.Colour = vehicle.ModelColour;
                    model.VINNumber = vehicle.Vinnumber;
                    model.EngineNumber = vehicle.EngineNumber;
                    model.KeyNo = vehicle.KeyNumber;
                    model.CustomerName = (await _customerService.GetAllCustomerByIdAsync((int)item.CustomerId)).CustomerName;
                    model.SalesConsultant = sc.FirstName + " " + sc.LastName;
                    model.BranchName = (await _branchMasterService.GetBranchByIdAsync((int)item.BranchId)).BranchName;
                    model.AllotDate = Convert.ToDateTime(item.DateCreated).ToString("dd/MM/yyyy");
                    list.AllotedVehicleDetails.Add(model);
                }
            }
            list.AvaillablePlaces = BindVehicleOutTo();


            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> List(GatePassModel model, IFormCollection form)
        {
            try
            {
                string bName = "";
                int bId = 0;
                var user = await _userMasterService.GetUserByIdAsync(Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value));
                if (model.OutTo == VehicleOutToEnum.Showroom.ToString())
                {
                    foreach (var v in model.AllotedVehicleDetails.Where(x => x.IsSelected.Selected == true))
                    {
                        var vehicle = await _vehicleAllotmentService.GetByIdAsync(v.AllotId);
                        var showroomVeh = new ShowroomVehiclesDetails
                        {
                            VehicleId = vehicle.VehicleId,
                            CustomerId = vehicle.CustomerId,
                            ScId = vehicle.Scid,
                            SubDealerId = vehicle.SubDealer,
                            VehicleCameFrom = "Godown",
                            DateCreated = DateTime.UtcNow,
                            BranchId = (int)vehicle.BranchId,
                            ByUser = user.Id
                        };
                        await _showroomVehiclesService.InsertAsync(showroomVeh);
                        bName = v.BranchName;
                        bId = showroomVeh.BranchId;
                    }
                }
                else
                {
                    foreach (var v in model.AllotedVehicleDetails.Where(x => x.IsSelected.Selected == true))
                    {
                        var pVehicle = await _vehicleAllotmentService.GetByIdAsync(v.AllotId);
                        var pdiVeh = new PdiVehiclesDetails
                        {
                            VehicleId = pVehicle.VehicleId,
                            CustomerId = pVehicle.CustomerId,
                            ScId = pVehicle.Scid,
                            SubDealerId = pVehicle.SubDealer,
                            VehicleCameFrom = "Godown",
                            DateCreated = DateTime.UtcNow,
                            BranchId = (int)pVehicle.BranchId,
                            ByUser = user.Id
                        };
                        await _pdiVehicesService.InsertAsync(pdiVeh);
                        bName = v.BranchName;
                        bId = pdiVeh.BranchId;
                    }

                }

                //add entry in gate pass tale
                var gatePass = new GatePassDetails
                {
                    VehicleOutTo = model.OutTo,
                    VehicleOutFor = bName,
                    BranchId = bId,
                    ByHand = model.ByHand,
                    Status = "Successfull",
                    DateCreated = DateTime.UtcNow,
                    ByUser = user.Id
                };
                await _gatePassService.InsertAsync(gatePass);

                foreach (var v in model.AllotedVehicleDetails.Where(x => x.IsSelected.Selected == true))
                {
                    var pVehicle1 = await _vehicleAllotmentService.GetByIdAsync(v.AllotId);
                    var gatePassVehicles = new GatePassVehicles
                    {
                        GatePassId = gatePass.Id,
                        VehicleId = (int)pVehicle1.VehicleId,
                        CustomerId = (int)pVehicle1.CustomerId,
                        SubDealerId = pVehicle1.SubDealer,
                        DateCreated = DateTime.UtcNow
                    };
                    await _gatePassService.InsertGatePassVehicleAsync(gatePassVehicles);

                    //update alloted vehicle status
                    pVehicle1.IsVehicleOutFromGodown = true;
                    pVehicle1.DateUpdated = DateTime.UtcNow;
                    await _vehicleAllotmentService.UpdateAsync(pVehicle1);

                    // update vehicle track status in vehicle master table
                    var vehicleInfo = await _vehicleMasterService.GetByIdAsync((int)pVehicle1.VehicleId);
                    vehicleInfo.TrackStatus = model.OutTo;
                    vehicleInfo.DateUpdated = DateTime.UtcNow;
                    await _vehicleMasterService.UpdateAsync(vehicleInfo);
                }



                //generate gate pass
                model.GatePassDate = DateTime.UtcNow.ToString("dd-MMM-yyyy");
                model.GatePassNo = gatePass.Id.ToString();
                model.VehicleoutTo = model.OutTo;
                model.VehicleoutFor = bName;
                model.AuthPerson = user.FirstName + " " + user.LastName;
                var gatepass = await _viewToStringRendererService.RenderToStringAsync("GatePassMaster/GatePassTemplate", model);

                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertHtmlString(gatepass.ToString());
                byte[] pdf = doc.Save();
                doc.Close();

                return File(pdf, "application/pdf", "gatepass.pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IActionResult Test()
        {
            return View("GatePassTemplate");
        }
    }
}
