using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMSCore.Models
{
    public class PriceListModel:BaseViewModel
    {
        public PriceListModel()
        {
            AvailableModels = new List<SelectListItem>();
            AvailableVarients = new List<SelectListItem>();
            PriceList= new List<PriceListDetailsModel>();
        }

        [Display(Name = "Search By Model")]
        public int ModelId { get; set; }

        [Display(Name = "Search By Varient")]
        public int VarientId { get; set; }       

        public List<SelectListItem> AvailableModels { get; set; }
        public List<SelectListItem> AvailableVarients { get; set; }

        public List<PriceListDetailsModel> PriceList { get; set; }
    }

    public partial class PriceListDetailsModel : BaseViewModel
    {
        public string ModelCode { get; set; }
        public string ModelVarient { get; set; }
        public string VarientType { get; set; }
        public decimal? ExShowroomPrice { get; set; }
        public decimal? FastTag { get; set; }
        public decimal? Tcs { get; set; }
        public decimal? Hainsurance { get; set; }
        public decimal? HanilDepWithKeyProtect { get; set; }
        public decimal? HartiwithEngineProtect { get; set; }
        public decimal? Rtoindividual { get; set; }
        public decimal? AccessoryComboKit { get; set; }
        public decimal? ExtendedWarranty { get; set; }
        public decimal? Rsa { get; set; }
        public decimal? ClayBar { get; set; }
        public decimal? Antirust { get; set; }
        public decimal? CarpetLamination { get; set; }
        public decimal? TotatPrice { get; set; }
    }
}
