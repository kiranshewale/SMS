using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMSCore.Models
{
    public class UserMasterModel:BaseViewModel
    {

        public UserMasterModel()
        {
            AvailableDesignations = new List<SelectListItem>();
            AvailableUnderDesIds= new List<SelectListItem>();
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }       
        public string Designation { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "ContactNumber is required")]
        public decimal? ContactNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }


        [Display(Name = "Select Designation")]
        [Required(ErrorMessage = "Designation is required")]
        public int DesignationId { get; set; }
        public List<SelectListItem> AvailableDesignations { get; set; }

        [Display(Name = "Under SM/TL")]
        //[Required(ErrorMessage = "Designation is required")]
        public int? UnderDesId { get; set; }
        public List<SelectListItem> AvailableUnderDesIds { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
