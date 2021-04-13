using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMSCore.Models
{
    public class GenerateQuotationModel:BaseViewModel
    {
        public GenerateQuotationModel()
        {
            VehiclePriceList = new VehiclePriceModel();
            AvailableModels = new List<SelectListItem>();
            AvailableVarients = new List<SelectListItem>();
            AvailableVarientTypes = new List<SelectListItem>();
            AvailableTeamLeaders = new List<SelectListItem>();
            AvailableSalesConsultants = new List<SelectListItem>();
            AvailableVarientColours = new List<SelectListItem>();
            CustomerDetails = new CustomerDetailsModel();
            AvailableSourceOfEnquiry = new List<SelectListItem>();
            PriceListFilter = new List<PriceListFilterModel>();
        }
        public string QuoteDate { get; set; }
        public int quoteNo { get; set; }

        public string SearchByAny { get; set; }
        
        [Display(Name = "Source Of Enquiry")]
        public string SourceOfEnquiry { get; set; }

        [Display(Name = "Booking Amount")]
        public decimal BookingAmount { get; set; }

        public string ModelName { get; set; }
        [Display(Name = "Select Model")]
        public int ModelId { get; set; }
        public List<SelectListItem> AvailableModels { get; set; }

        public string VarientName { get; set; }
        [Display(Name = "Select Model Varient")]
        public int VarientId { get; set; }
        public List<SelectListItem> AvailableVarients { get; set; }

        [Display(Name = "Select Varient Type")]
        public string VarientType { get; set; }
        public List<SelectListItem> AvailableVarientTypes { get; set; }

        public string ColourName { get; set; }
        [Display(Name = "Select Colour")]
        public int VarientColourId { get; set; }
        public List<SelectListItem> AvailableVarientColours { get; set; }

        public string StateName { get; set; }
        
        public string TLName { get; set; }
        [Display(Name = "Select Team Leader")]
        public int TeamLeaderId { get; set; }
        public List<SelectListItem> AvailableTeamLeaders{ get; set; }

        public string SCMobileNumber { get; set; }
        public string SCName { get; set; }
        [Display(Name = "Select Sales Consultant")]
        public int SalesConsultantId { get; set; }
        public List<SelectListItem> AvailableSalesConsultants { get; set; }
        public List<SelectListItem> AvailableSourceOfEnquiry { get; set; }

        public VehiclePriceModel VehiclePriceList { get; set; }
        public CustomerDetailsModel CustomerDetails { get; set; }
        public List<PriceListFilterModel> PriceListFilter { get; set; }
        public string Url { get; set; }
        public bool IsErrorFound { get; set; }
        public string Error { get; set; }
    }

    public partial class VehiclePriceModel : BaseViewModel
    {
        public decimal ExShowroomPrice { get; set; }
        public decimal FastTag { get; set; }
        public decimal TCS { get; set; }
        public decimal Registration { get; set; }
        public decimal CRTM { get; set; }
        public decimal Insurance { get; set; }
        public decimal InsuranceWithKeyProtect { get; set; }
        public decimal InsuranceWithEngineProtect { get; set; }
        public decimal HConnect { get; set; }
        public decimal ExtendedWarranty { get; set; }
        public decimal RSA { get; set; }

        public decimal Accessories { get; set; }
        public decimal ClayBar { get; set; }
        public decimal Antirust { get; set; }
        public decimal Lamination { get; set; }
        public decimal Total { get; set; }
        public decimal ConsumerBonus { get; set; }
        public decimal CorporateRuralInd { get; set; }
        public decimal ExchangeBonus { get; set; }
        public decimal GrandTotal { get; set; }
       
    }

    public partial class PriceListFilterModel
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
