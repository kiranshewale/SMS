using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMSCore.Models
{
    public class CustomerDetailsSearchModel
    {
        public CustomerDetailsSearchModel()
        {
            CustomerDetails = new List<CustomerDetailsModel>();
        }

        //for search
        [Display(Name = "Search By Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string SearchByEmail { get; set; }

        [Display(Name = "Search By Mobile")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid mobile number")]
        public string SearchByMobileNo { get; set; }

        [Display(Name = "Search By Pan")]
        [RegularExpression(@"^(?:.*[a-z]){10,}$", ErrorMessage = "Invalid pan number")]
        public string SearchByPanNo { get; set; }

        [Display(Name = "Search By Aadhar")]
        [RegularExpression(@"^(\d{12})$", ErrorMessage = "Wrong mobile")]
        public string SearchByAadharNo { get; set; }

        public List<CustomerDetailsModel> CustomerDetails { get; set; }
    }

    public partial class CustomerDetailsModel : BaseViewModel
    {
        public CustomerDetailsModel()
        {
            AvailableSalutations = new List<SelectListItem>();
            AvailableStates = new List<SelectListItem>();
            ErrorNotificationModel = new ErrorModelAndNotificationModel();
        }
       public int CustId { get; set; }
        [Display(Name = "Select")]
        public string Salutation { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        public string CustomerName { get; set; }

        [Display(Name = "Mobile 1")]
        [Required(ErrorMessage = "Mobile Numbers is required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public decimal? MobileNo1 { get; set; }

        [Display(Name = "Mobile 2")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public decimal? MobileNo2 { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }

        [Display(Name = "Address 1")]
        [Required(ErrorMessage = "Address is required")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Display(Name = "Pincode")]
        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Please Enter Valid Pincode.")]
        public string Pin { get; set; }

        [Display(Name = "Taluka")]
        public string Taluka { get; set; }

        [Display(Name = "City")]
        public string District { get; set; }

        public string StateName { get; set; }

        [Display(Name = "Select State")]
        public int StateId { get; set; }

        [Display(Name = "Pan Number")]
        [RegularExpression(@"^(?:.*[a-z]){10,}$", ErrorMessage = "Invalid pan number")]
        public string PanNo { get; set; }

        [Display(Name = "Aadhar Number")]
        [RegularExpression(@"^(\d{12})$", ErrorMessage = "Invalid aadhar number")]
        public string AdharNo { get; set; }

        [Display(Name = "GST Number")]
        public string Gstno { get; set; }

        [Display(Name = "Dealer Code")]
        public string DealerCode { get; set; }
        public List<SelectListItem> AvailableSalutations { get; set; }

        public List<SelectListItem> AvailableStates { get; set; }

        public ErrorModelAndNotificationModel ErrorNotificationModel { get; set; }
    }
}
