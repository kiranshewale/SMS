using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMSCore.Models
{
    public class VehicleAllotmentModel:BaseViewModel
    {
        public VehicleAllotmentModel()
        {
            AvailableModels = new List<SelectListItem>();
            AvailableVarients = new List<SelectListItem>();
            AvailableVarientTypes = new List<SelectListItem>();
            AvailableVarientColours = new List<SelectListItem>();
            AvailableTeamLeaders = new List<SelectListItem>();
            AvailableSalesConsultants = new List<SelectListItem>();
            CustomerDetails = new CustomerDetailsModel();
            AvailableBranches = new List<SelectListItem>();
        }

        public int VehicleId { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Select Model")]
        public int ModelId { get; set; }
        public List<SelectListItem> AvailableModels { get; set; }

        [Display(Name = "Select Model Varient")]
        public int VarientId { get; set; }
        public List<SelectListItem> AvailableVarients { get; set; }

        [Display(Name = "Select Varient Type")]
        public string VarientType { get; set; }
        public List<SelectListItem> AvailableVarientTypes { get; set; }

        [Display(Name = "Select Colour")]
        public int VarientColourId { get; set; }
        public List<SelectListItem> AvailableVarientColours { get; set; }

        public string TLName { get; set; }
        [Display(Name = "Select Team Leader")]
        public int TeamLeaderId { get; set; }
        public List<SelectListItem> AvailableTeamLeaders { get; set; }

        public string SCMobileNumber { get; set; }
        public string SCName { get; set; }
        [Display(Name = "Select Sales Consultant")]
        public int SalesConsultantId { get; set; }
        public string BranchName { get; set; }
        [Display(Name = "For Branch")]
        public int BranchId { get; set; }
        public List<SelectListItem> AvailableBranches { get; set; }

        [Display(Name = "Select Co-Dealer")]
        public int CoDealerId { get; set; }
        public List<SelectListItem> AvailableCoDealers { get; set; }


        //for admin
        public string Model { get; set; }
        public string Colour { get; set; }
        public string VINNumber { get; set; }
        public string EngineNumber { get; set; }
        public string CustomerName { get; set; }
        public string SalesConsultant { get; set; }
        public bool IsAlloted { get; set; }
        public string AllotDate { get; set; } 
        public int AgingDays { get; set; }
        //end
        public List<SelectListItem> AvailableSalesConsultants { get; set; }

        public CustomerDetailsModel CustomerDetails { get; set; }
    }
}
