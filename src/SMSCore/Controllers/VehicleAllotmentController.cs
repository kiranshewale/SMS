using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ActionFilters;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services;
using SMSCore.Services.BranchMasterService;
using SMSCore.Services.CoDealerMasterService;
using SMSCore.Services.CustomerService;
using SMSCore.Services.DesignationMasterService;
using SMSCore.Services.ModelMasterService;
using SMSCore.Services.StateListService;
using SMSCore.Services.UserMasterService;
using SMSCore.Services.VarientMasterService;
using SMSCore.Services.VehicleAllotmentService;
using SMSCore.Services.VehiclesMasterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class VehicleAllotmentController : BaseAdminController
    {
        private readonly IUserMasterService _userMasterService;
        private readonly IModelMasterService _modelsMasterService;
        private readonly IVarientMasterService _varientMasterService;
        private readonly IDesignationMasterService _designationMasterService;
        private readonly IStateListService _stateListService;
        private readonly IVehiclesMasterService _vehicleMasterService;
        private readonly ICustomerService _customerService;
        private readonly IVehicleAllotmentService _vehicleAllotmentService;
        private readonly IBranchMasterService _branchMasterService;
        private readonly ICoDealerMasterService _coDealerMasterService;

        public VehicleAllotmentController(IUserMasterService userMasterService,
             IModelMasterService modelsMasterService,
               IVarientMasterService varientMasterService,
             IDesignationMasterService designationMasterService,
             IStateListService stateListService,
             IVehiclesMasterService vehicleMasterService,
             ICustomerService customerService,
             IVehicleAllotmentService vehicleAllotmentService,
             IBranchMasterService branchMasterService,
             ICoDealerMasterService coDealerMasterService )
        {
            _userMasterService = userMasterService;
            _modelsMasterService = modelsMasterService;
            _varientMasterService = varientMasterService;
            _designationMasterService = designationMasterService;
            _stateListService = stateListService;
            _vehicleMasterService = vehicleMasterService;
            _customerService = customerService;
            _vehicleAllotmentService = vehicleAllotmentService;
            _branchMasterService = branchMasterService;
            _coDealerMasterService = coDealerMasterService;
        }

        #region Utilities
        private List<SelectListItem> BindModels(IList<ModelMaster> models)
        {
            var modelList = new VehicleAllotmentModel();
            modelList.AvailableModels.Add(new SelectListItem
            {
                Value = "",
                Text = "Select Model"
            });
            foreach (var model in models)
            {
                modelList.AvailableModels.Add(new SelectListItem
                {
                    Value = model.Id.ToString(),
                    Text = model.ModelName.ToString()
                });
            }

            return modelList.AvailableModels;
        }
        private List<SelectListItem> BindVarientsType()
        {
            var model = new VehicleAllotmentModel();
            model.AvailableVarientTypes.Add(new SelectListItem
            {
                Value = "",
                Text = "Varient Type"
            });
            foreach (VarientTypeEnum e in Enum.GetValues(typeof(VarientTypeEnum)))
            {
                model.AvailableVarientTypes.Add(new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });
            }

            return model.AvailableVarientTypes;
        }
        private async Task<List<SelectListItem>> BindStates()
        {

            var modelList = new VehicleAllotmentModel();
            modelList.CustomerDetails.AvailableStates.Add(new SelectListItem
            {
                Value = "0",
                Text = "Select State"
            });

            var stateList = await _stateListService.GetStateList();
            foreach (var state in stateList)
            {
                modelList.CustomerDetails.AvailableStates.Add(new SelectListItem
                {
                    Value = state.Id.ToString(),
                    Text = state.State
                });
            }

            return modelList.CustomerDetails.AvailableStates;
        }


        private async Task<List<SelectListItem>> BindTeamLeaders()
        {
            var model = new VehicleAllotmentModel();
            model.AvailableTeamLeaders.Add(new SelectListItem
            {
                Value = "",
                Text = "Select Team Leader"
            });
            var designId = (await _designationMasterService.GetAllDesignationsListAsync()).Where(x => x.Designation.ToLower() == "team leader").FirstOrDefault().Id;
            var TeamLieadersList = (await _userMasterService.GetSMandTLList()).Where(x => x.DesignationId == designId);
            foreach (var tl in TeamLieadersList)
            {
                model.AvailableTeamLeaders.Add(new SelectListItem
                {
                    Value = tl.Id.ToString(),
                    Text = tl.FirstName + " " + tl.LastName.ToString()
                });
            }

            return model.AvailableTeamLeaders;
        }

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

        private async Task<List<SelectListItem>> BindBranches()
        {
            var model = new VehicleAllotmentModel();
            model.AvailableBranches.Add(new SelectListItem
            {
                Value = "0",
                Text = "Select Branch"
            });
            var branchList = (await _branchMasterService.GetBranchListAsync());
            foreach (var branch in branchList)
            {
                model.AvailableBranches.Add(new SelectListItem
                {
                    Value = branch.Id.ToString(),
                    Text = branch.BranchName
                });
            }

            return model.AvailableBranches;
        }

        private async Task<List<SelectListItem>> BindCoDealers()
        {
            var model = new VehicleAllotmentModel();
            model.AvailableBranches.Add(new SelectListItem
            {
                Value = "0",
                Text = "Select Co-Dsaler"
            });
            var coDealers = (await _coDealerMasterService.GetAllCoDealerListAsync());
            foreach (var dealer in coDealers)
            {
                model.AvailableBranches.Add(new SelectListItem
                {
                    Value = dealer.Id.ToString(),
                    Text = dealer.SubDealerName
                });
            }

            return model.AvailableBranches;
        }

        #endregion

        #region Front End
        public async Task<IActionResult> CustomerVehicleAllotment()
        {

            var model = new VehicleAllotmentModel();
            model.AvailableModels = BindModels(await _modelsMasterService.GetAllModelsListAsync());
            model.AvailableVarientTypes = BindVarientsType();
            //model.AvailableVarients = BindVarients();
            model.CustomerDetails.AvailableStates = await BindStates();
            model.AvailableTeamLeaders = await BindTeamLeaders();
            //model.AvailableSalesConsultants = BindSalesConsultants();
            model.CustomerDetails.AvailableSalutations = BindSalutations();
            model.AvailableBranches = await BindBranches();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerVehicleAllotment(IFormCollection form)
        {
            try
            {
                var custId = Convert.ToInt32(form["CustId"]);
                var vehId = Convert.ToInt32(form["item.VID"]);
                var custInfo = await _customerService.GetAllCustomerByIdAsync(custId);
                if (custInfo == null)
                {
                    //create cust entry
                    var customer = new CustomerDetailsTable
                    {
                        CustomerName = form["CustomerName"],
                        Address1 = form["Address1"],
                        Address2 = !string.IsNullOrEmpty(form["Address2"].ToString()) ? form["Address2"].ToString() : string.Empty,
                        EmailId = form["EmailId"],
                        Pin = form["Pin"],
                        Taluka = form["Taluka"],
                        District = form["District"],
                        State = Convert.ToInt32(form["StateId"]),
                        MobileNo1 = Convert.ToDecimal(form["MobileNo1"]),
                        MobileNo2 = Convert.ToDecimal(form["MobileNo2"]),
                        DateUpdated = DateTime.UtcNow,
                        DateCreated = DateTime.UtcNow
                    };
                    await _customerService.InsertAsync(customer);
                    custId = customer.Id;
                }

                //add record in table
                if (vehId != 0)
                {
                    var vehicleAllotDetails = new VehicleAllotmentDetails
                    {
                        VehicleId = Convert.ToInt32(form["item.VID"]),
                        CustomerId = custId,
                        ModelId = Convert.ToInt32(form["ModelId"]),
                        VarientId = Convert.ToInt32(form["VarientId"]),
                        ColourId = Convert.ToInt32(form["VarientColourId"]),
                        VarientType = form["VarientType"],
                        Scid = Convert.ToInt32(form["SalesConsultantId"]),
                        BranchId= Convert.ToInt32(form["BranchId"]),
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                        AllotedBy = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value),
                    };
                    await _vehicleAllotmentService.InsertAsync(vehicleAllotDetails);

                    //update vehicle status in vehicle master
                    var vehicle = await _vehicleMasterService.GetByIdAsync(vehId);
                    if (vehicle != null)
                    {
                        vehicle.TrackStatus = vehicleAllotDetails.IsAlloted ? VehicleStatusEnum.Alloted.ToString() : VehicleStatusEnum.PartiallyAlloted.ToString();
                        vehicle.DateUpdated = DateTime.UtcNow;
                        await _vehicleMasterService.UpdateAsync(vehicle);
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IActionResult> GetVehicleList(int modelId, int varientId, int colourId)
        {
            var vehiclesList = await _vehicleMasterService.GetVehicleListByModelVarientColurAsync(modelId, varientId, colourId);
            if (vehiclesList.Count > 0)
           {
                var list = new List<VehiclesListModel>();
                list.AddRange(vehiclesList.Select(x => new VehiclesListModel
                {
                    VID = x.Id,
                    Model = x.ModelName,
                    Varient = x.ModelVarient,
                    Colour = x.ModelColour,
                    VINNumber = x.Vinnumber,
                    EngineNumber = x.EngineNumber,
                    KeyNumber = x.KeyNumber
                }));
                return PartialView("_VehiclesList", list);
            }
            return Json("NotFound");
        }
        #endregion

        #region CoDealer Vehicle Allotment
        public async Task<IActionResult> CoDealerVehicleAllotment()
        {
            var model = new VehicleAllotmentModel();
            model.AvailableModels = BindModels(await _modelsMasterService.GetAllModelsListAsync());
            model.AvailableVarientTypes = BindVarientsType();
            model.AvailableCoDealers =await BindCoDealers();
            model.AvailableTeamLeaders = await BindTeamLeaders();
            model.AvailableBranches = await BindBranches();
            return View(model);
        }
        #endregion



        #region Admin
        public async Task<IActionResult> List()
        {
            var list = new List<VehicleAllotmentModel>();
            var allotedVehicesList = await _vehicleAllotmentService.GetAllAllotedVehiclesAsync();
            foreach (var item in allotedVehicesList)
            {
                var vehicle = await _vehicleMasterService.GetByIdAsync((int)item.VehicleId);
                var sc = await _userMasterService.GetUserByIdAsync((int)item.Scid);
                var model = new VehicleAllotmentModel();
                model.Id = item.Id;
                model.Model = vehicle.ModelVarient;
                model.Colour = vehicle.ModelColour;
                model.VINNumber = vehicle.Vinnumber;
                model.EngineNumber = vehicle.EngineNumber;
                model.IsAlloted = item.IsAlloted;
                model.CustomerName = (await _customerService.GetAllCustomerByIdAsync((int)item.CustomerId)).CustomerName;
                model.SalesConsultant = sc.FirstName + " " + sc.LastName;
                model.AllotDate =Convert.ToDateTime(item.DateCreated).ToString("dd/MM/yyyy");
                model.AgingDays = vehicle.AgingDays;
                model.BranchName = (await _branchMasterService.GetBranchByIdAsync((int)item.BranchId)).BranchName;
                list.Add(model);
            }
            
            return View(list);
        }

        //[HttpPost]
        //public IActionResult List(VehicleAllotmentModel model)
        //{
        //    return View();
        //}

        public async Task<IActionResult> Edit(int Id)
        {
            var existingAllotment = await _vehicleAllotmentService.GetByIdAsync(Id);
            var vehicle = await _vehicleMasterService.GetByIdAsync((int)existingAllotment.VehicleId);
            var sc = await _userMasterService.GetUserByIdAsync((int)existingAllotment.Scid);
            var model = new VehicleAllotmentModel();
            model.Id = existingAllotment.Id;
            model.Model = vehicle.ModelVarient;
            model.Colour = vehicle.ModelColour;
            model.VINNumber = vehicle.Vinnumber;
            model.EngineNumber = vehicle.EngineNumber;
            model.IsAlloted = existingAllotment.IsAlloted;
            model.CustomerName = (await _customerService.GetAllCustomerByIdAsync((int)existingAllotment.CustomerId)).CustomerName;
            model.SalesConsultant = sc.FirstName + " " + sc.LastName;
            model.AllotDate = Convert.ToDateTime(existingAllotment.DateCreated).ToString("dd/MM/yyyy");
            model.AgingDays = vehicle.AgingDays;

            return View(model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(VehicleAllotmentModel viewModel,bool continueEditing)
        {
            var existingAllotment = await _vehicleAllotmentService.GetByIdAsync(viewModel.Id);
            existingAllotment.IsAlloted = viewModel.IsAlloted;
            existingAllotment.ApprovedBy = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            existingAllotment.DateUpdated = DateTime.UtcNow;
            await _vehicleAllotmentService.UpdateAsync(existingAllotment);

            //update vehicle track status
            var existingVehicle = await _vehicleMasterService.GetByIdAsync((int)existingAllotment.VehicleId);
            if(existingVehicle!=null)
            {
                existingVehicle.TrackStatus = VehicleStatusEnum.Alloted.ToString();
                existingVehicle.DateUpdated = DateTime.UtcNow;
                await _vehicleMasterService.UpdateAsync(existingVehicle);
            }
            AllowContinueEditing(continueEditing);
            //TempData["UserMessageSuccess"] = "Record saved sucessfully.";

            if (continueEditing)
                return RedirectToAction(nameof(Edit), new { id = existingAllotment.Id });

            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}
