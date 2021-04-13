using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMSCore.Models
{
    public class GatePassModel : BaseViewModel
    {
        public GatePassModel()
        {
            AllotedVehicleDetails = new List<AllotedVehicleDetailsModel>();
            AvaillablePlaces = new List<SelectListItem>();
        }

        public string GatePassDate { get; set; }
        public string GatePassNo { get; set; }
        public string AuthPerson { get; set; }
        public string VehicleoutTo{ get; set; }
        public string VehicleoutFor { get; set; }

        [Display(Name = "Vehicle Hand Over To")]
        [Required(ErrorMessage = "Vehicle hand over to whome is required.")]
        public string ByHand { get; set; }

        public List<AllotedVehicleDetailsModel> AllotedVehicleDetails { get; set; }

        [Display(Name ="Vehicle Out To")]
        public string OutTo { get; set; }
        public List<SelectListItem> AvaillablePlaces { get; set; }   

    }
    public partial class AllotedVehicleDetailsModel
    {
        public int AllotId { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string VINNumber { get; set; }
        public string EngineNumber { get; set; }
        public string CustomerName { get; set; }
        public string SalesConsultant { get; set; }
        public string BranchName { get; set; }
        public string AllotDate { get; set; }
        public string KeyNo { get; set; }
        public SelectListItem IsSelected { get; set; }
    }
}
