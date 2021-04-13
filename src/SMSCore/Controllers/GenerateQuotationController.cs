using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;
using SMSCore.Data.Entities;
using SMSCore.Models;
using SMSCore.Services.ColourMasterService;
using SMSCore.Services.CustomerService;
using SMSCore.Services.DesignationMasterService;
using SMSCore.Services.ModelMasterService;
using SMSCore.Services.PriceListMasterService;
using SMSCore.Services.QuotationManagerService;
using SMSCore.Services.StateListService;
using SMSCore.Services.UserMasterService;
using SMSCore.Services.VarientMasterService;
using SMSCore.Services.ViewToStringService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class GenerateQuotationController : BaseAdminController
    {
        private readonly IUserMasterService _userMasterService;
        private readonly IModelMasterService _modelsMasterService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IViewToStringRendererService _viewToStringRendererService;
        private readonly IVarientMasterService _varientMasterService;
        private readonly IDesignationMasterService _designationMasterService;
        private readonly IStateListService _stateListService;
        private readonly ICustomerService _customerService;
        private readonly IQuotationManagerService _quotationManagerService;
        private readonly IColourMasterService _colourMasterService;
        private readonly IPriceListMasterService _priceListMasterService;

        public GenerateQuotationController(IUserMasterService userMasterService,
             IModelMasterService modelsMasterService,
             IWebHostEnvironment hostingEnvironment,
             IViewToStringRendererService viewToStringRendererService,
             IVarientMasterService varientMasterService,
             IDesignationMasterService designationMasterService,
             IStateListService stateListService,
             ICustomerService customerService,
             IQuotationManagerService quotationManagerService,
             IColourMasterService colourMasterService,
             IPriceListMasterService priceListMasterService)
        {
            _userMasterService = userMasterService;
            _modelsMasterService = modelsMasterService;
            _hostingEnvironment = hostingEnvironment;
            _viewToStringRendererService = viewToStringRendererService;
            _varientMasterService = varientMasterService;
            _designationMasterService = designationMasterService;
            _stateListService = stateListService;
            _customerService = customerService;
            _quotationManagerService = quotationManagerService;
            _colourMasterService = colourMasterService;
            _priceListMasterService = priceListMasterService;
        }


        #region Utilities
        private List<SelectListItem> BindModels(IList<ModelMaster> models)
        {
            var modelList = new GenerateQuotationModel();
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
            var model = new GenerateQuotationModel();
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

            var modelList = new GenerateQuotationModel();
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
            var model = new GenerateQuotationModel();
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

        private List<SelectListItem> BindEnquirySource()
        {
            var model = new GenerateQuotationModel();
            model.AvailableSourceOfEnquiry.Add(new SelectListItem
            {
                Value = "",
                Text = "Source Of Enquiry"
            });
            foreach (SourceEnquiryEnum e in Enum.GetValues(typeof(SourceEnquiryEnum)))
            {
                model.AvailableSourceOfEnquiry.Add(new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                });
            }

            return model.AvailableSourceOfEnquiry;
        }
        #endregion

        public async Task<IActionResult> GenarateQuote(int custId)
        {
            var model = new GenerateQuotationModel();
            model.Url = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;
            model.AvailableModels = BindModels(await _modelsMasterService.GetAllModelsListAsync());
            model.AvailableVarientTypes = BindVarientsType();
            //model.AvailableVarients = BindVarients();
            model.CustomerDetails.AvailableStates = await BindStates();
            model.AvailableTeamLeaders = await BindTeamLeaders();
            //model.AvailableSalesConsultants = BindSalesConsultants();
            model.CustomerDetails.AvailableSalutations = BindSalutations();
            model.AvailableSourceOfEnquiry = BindEnquirySource();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenarateQuote(GenerateQuotationModel model, IFormCollection form)
        {
            decimal totalAmount = 0;
            var custId = Convert.ToInt32(form["CustId"]);
            var varientInfo = _varientMasterService.GetVarientByIdAsync(model.VarientId);
            var priceList = await _priceListMasterService.GetModelPriceByModelCodeAsync(varientInfo.VarientCode, varientInfo.VerientName, model.VarientType);
            if (priceList != null)
            {
                model.CustomerDetails.CustomerName = form["CustomerName"].ToString();
                model.CustomerDetails.Address1 = form["Address1"].ToString();
                model.CustomerDetails.Address2 = !string.IsNullOrEmpty(form["Address2"].ToString()) ? form["Address2"].ToString() : string.Empty;
                model.CustomerDetails.EmailId = form["EmailId"].ToString();
                model.CustomerDetails.Pin = form["Pin"].ToString();
                model.CustomerDetails.MobileNo1 = Convert.ToDecimal(form["MobileNo1"]);
                model.CustomerDetails.MobileNo2 = Convert.ToDecimal(form["MobileNo2"]);

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

                //add quotation info
                var quotation = new QuotationDetails
                {
                    BranchId = 1,
                    ModelId = model.ModelId,
                    VarientId = model.VarientId,
                    VarientType = model.VarientType,
                    ColourId = model.VarientColourId,
                    CustomerId = custId,
                    ScId = model.SalesConsultantId,
                    LeadSource = model.SourceOfEnquiry,
                    BookingAmount = model.BookingAmount,
                    DateCretaed = DateTime.UtcNow,
                    CreatedBy = Convert.ToInt32(HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value)
                };
                 await _quotationManagerService.InsertAsync(quotation);

                //prepare quote model
                model.QuoteDate = DateTime.UtcNow.ToString("dd/MM/yyyy");
                model.quoteNo = quotation.Id;
                var scInfo = await _userMasterService.GetUserByIdAsync(model.SalesConsultantId);
                model.ModelName = _modelsMasterService.GetModelbyIdAsync(model.ModelId).ModelName;
                model.VarientName = varientInfo.VerientName;
                model.ColourName = _colourMasterService.GetColourByIdAsync(model.VarientColourId).ColourName;
                model.SCName = scInfo.FirstName + " " + scInfo.LastName;
                model.SCMobileNumber = scInfo.ContactNumber.ToString();
                var tlInfo = await _userMasterService.GetUserByIdAsync(model.TeamLeaderId);
                model.TLName = tlInfo.FirstName + " " + tlInfo.LastName;

                if (Convert.ToBoolean(form["Ex Showroom Price"]))
                {
                    model.VehiclePriceList.ExShowroomPrice = Convert.ToDecimal(priceList.ExShowroomPrice);
                    totalAmount += Convert.ToDecimal(priceList.ExShowroomPrice);
                }

                if (Convert.ToBoolean(form["Fast Tag"]))
                {
                    model.VehiclePriceList.FastTag = Convert.ToDecimal(priceList.FastTag);
                    totalAmount += Convert.ToDecimal(priceList.FastTag);
                }

                if (Convert.ToBoolean(form["TCS"]))
                {
                    model.VehiclePriceList.TCS = Convert.ToDecimal(priceList.Tcs);
                    totalAmount += Convert.ToDecimal(priceList.Tcs);
                }

                if (Convert.ToBoolean(form["Honda Assure Insurance"]))
                {
                    model.VehiclePriceList.Insurance = Convert.ToDecimal(priceList.Hainsurance);
                    totalAmount += Convert.ToDecimal(priceList.Hainsurance);
                }

                if (Convert.ToBoolean(form["Honda Assure Insurance + Key Protect"]))
                {
                    model.VehiclePriceList.InsuranceWithKeyProtect = Convert.ToDecimal(priceList.HanilDepWithKeyProtect);
                    totalAmount += Convert.ToDecimal(priceList.HanilDepWithKeyProtect);
                }

                if (Convert.ToBoolean(form["Honda Assure Insurance + Engine Protect"]))
                {
                    model.VehiclePriceList.InsuranceWithEngineProtect = Convert.ToDecimal(priceList.HartiwithEngineProtect);
                    totalAmount += Convert.ToDecimal(priceList.HartiwithEngineProtect);
                }

                if (Convert.ToBoolean(form["Registration (Indv/Comp)"]))
                {
                    model.VehiclePriceList.Registration = Convert.ToDecimal(priceList.Rtoindividual);
                    totalAmount += Convert.ToDecimal(priceList.Rtoindividual);
                }

                if (Convert.ToBoolean(form["Accessory Combo Kit"]))
                {
                    model.VehiclePriceList.Accessories = Convert.ToDecimal(priceList.AccessoryComboKit);
                    totalAmount += Convert.ToDecimal(priceList.AccessoryComboKit);
                }

                if (Convert.ToBoolean(form["Extended Warranty"]))
                {
                    model.VehiclePriceList.ExtendedWarranty = Convert.ToDecimal(priceList.ExtendedWarranty);
                    totalAmount += Convert.ToDecimal(priceList.ExtendedWarranty);
                }

                if (Convert.ToBoolean(form["RSA"]))
                {
                    model.VehiclePriceList.RSA = Convert.ToDecimal(priceList.Rsa);
                    totalAmount += Convert.ToDecimal(priceList.Rsa);
                }

                if (Convert.ToBoolean(form["Clay Bar"]))
                {
                    model.VehiclePriceList.ClayBar = Convert.ToDecimal(priceList.ClayBar);
                    totalAmount += Convert.ToDecimal(priceList.ClayBar);
                }

                if (Convert.ToBoolean(form["Antirust"]))
                {
                    model.VehiclePriceList.Antirust = Convert.ToDecimal(priceList.Antirust);
                    totalAmount += Convert.ToDecimal(priceList.Antirust);
                }

                if (Convert.ToBoolean(form["Carpet Lamination"]))
                {
                    model.VehiclePriceList.Lamination = Convert.ToDecimal(priceList.CarpetLamination);
                    totalAmount += Convert.ToDecimal(priceList.CarpetLamination);
                }

                model.VehiclePriceList.Total = totalAmount;
                model.VehiclePriceList.GrandTotal = model.VehiclePriceList.Total;

                model.Url =HttpContext.Request.Scheme+"://"+ HttpContext.Request.Host.Value; 

                //generate quote
                var quote = await _viewToStringRendererService.RenderToStringAsync("GenerateQuotation/QuoteTemplate", model);

                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertHtmlString(quote.ToString());
                byte[] pdf = doc.Save();
                doc.Close();

                return File(pdf, "application/pdf", "quote.pdf");
            }

            model.IsErrorFound = true;
            model.Error = "Vehicle price list not found";
            return View(model);
        }


        public IActionResult Test()
        {
            return View("QuoteTemplate");
        }

        [HttpGet]
        public async Task<IActionResult> GetVarientsByModelId(int modelId)
        {
            var list = new List<SelectListItemForJson>();
            var varientsList = await _modelsMasterService.GetModelVarientsByModelId(modelId);

            if (varientsList != null && varientsList.Any())
            {
                foreach (var item in varientsList.OrderBy(x => x.Id))
                {
                    list.Add(new SelectListItemForJson
                    {
                        id = item.Id.ToString(),
                        name = item.VerientName
                    });
                }

            }
            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetVarientColoursByVarient(int varientId, string varientType)
        {
            var list = new List<SelectListItemForJson>();
            var coloursList = await _varientMasterService.GetVarientColoursByVarienAsync(varientId, varientType);

            if (coloursList != null && coloursList.Any())
            {
                foreach (var item in coloursList.OrderBy(x => x.Id))
                {
                    list.Add(new SelectListItemForJson
                    {
                        id = item.Id.ToString(),
                        name = item.ColourName
                    });
                }

            }
            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesConsultantsByTL(int teamLeaderId)
        {
            var list = new List<SelectListItemForJson>();
            var scList = (await _userMasterService.GetAllUsers()).Where(x => x.UnderDesId == teamLeaderId);

            if (scList != null && scList.Any())
            {
                foreach (var item in scList.OrderBy(x => x.Id))
                {
                    list.Add(new SelectListItemForJson
                    {
                        id = item.Id.ToString(),
                        name = item.FirstName + " " + item.LastName
                    });
                }

            }
            return Json(list);
        }


        #region Ajax Calls
        [HttpGet]
        public async Task<IActionResult> GetExistingCustomer(string searchTerm)
        {
            var existingcustomer = await _customerService.GetCustomerByAnyAsync(searchTerm);
            if (existingcustomer == null)
                return Json("Not Found");

            var model = new CustomerDetailsModel
            {
                CustId = existingcustomer.Id,
                Salutation = existingcustomer.Salutation,
                CustomerName = existingcustomer.CustomerName,
                EmailId = existingcustomer.EmailId,
                MobileNo1 = existingcustomer.MobileNo1,
                MobileNo2 = Convert.ToDecimal(existingcustomer.MobileNo2),
                Address1 = existingcustomer.Address1,
                Address2 = existingcustomer.Address2,
                Pin = existingcustomer.Pin,
                Taluka = existingcustomer.Taluka,
                District = existingcustomer.District,
                StateId =(int) existingcustomer.State,
                PanNo = existingcustomer.PanNo,
                AdharNo = existingcustomer.AdharNo,
                Gstno = existingcustomer.Gstno,
            };
            model.AvailableSalutations = BindSalutations();
            model.AvailableStates = await BindStates();
            return PartialView("~/Views/CustomerMaster/_CreateOrUpdate.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceList(int varientId, string varientType)
        {
            var varientInfo = _varientMasterService.GetVarientByIdAsync(varientId);
            var priceList = await _priceListMasterService.GetModelPriceByModelCodeAsync(varientInfo.VarientCode, varientInfo.VerientName, varientType);
            if (priceList == null)
                return Json("Not Found");

            var list = new List<PriceListFilterModel>();

            list.Add(new PriceListFilterModel
            {
                IsSelected = true,
                Name = "Ex Showroom Price",
                Price = Convert.ToDecimal(priceList.ExShowroomPrice)
            });

            list.Add(new PriceListFilterModel
            {
                IsSelected = true,
                Name = "Fast Tag",
                Price = Convert.ToDecimal(priceList.FastTag)
            });
            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "TCS",
                Price = Convert.ToDecimal(priceList.Tcs)
            });
            list.Add(new PriceListFilterModel
            {
                IsSelected = true,
                Name = "Honda Assure Insurance",
                Price = Convert.ToDecimal(priceList.Hainsurance)
            });
            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "Honda Assure Insurance + Key Protect",
                Price = Convert.ToDecimal(priceList.HanilDepWithKeyProtect)
            });
            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "Honda Assure Insurance + Engine Protect",
                Price = Convert.ToDecimal(priceList.HartiwithEngineProtect)
            });

            list.Add(new PriceListFilterModel
            {
                IsSelected = true,
                Name = "Registration (Indv/Comp)",
                Price = Convert.ToDecimal(priceList.Rtoindividual)
            });

            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "Accessory Combo Kit",
                Price = Convert.ToDecimal(priceList.AccessoryComboKit)
            });
            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "Extended Warranty",
                Price = Convert.ToDecimal(priceList.ExtendedWarranty)
            });
            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "RSA",
                Price = Convert.ToDecimal(priceList.Rsa)

            });

            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "Clay Bar",
                Price = Convert.ToDecimal(priceList.ClayBar)

            });

            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "Antirust",
                Price = Convert.ToDecimal(priceList.Antirust)

            });

            list.Add(new PriceListFilterModel
            {
                IsSelected = false,
                Name = "Carpet Lamination",
                Price = Convert.ToDecimal(priceList.CarpetLamination)

            });

            return PartialView("_ModelPriceList", list);
        }
        #endregion
    }
}
