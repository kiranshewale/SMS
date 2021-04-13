using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System.Collections.Generic;

namespace SMSCore.Models
{
    public class VarientMasterModel:BaseViewModel
    {
        public VarientMasterModel()
        {
            AvaillabelColoursList = new List<SelectListItem>();
        }
        public string VerientName { get; set; }
        public string FriendlyName { get; set; }
        public string VarientCode { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        public int[] SelectedColourIds { get; set; }
        public List<SelectListItem> AvaillabelColoursList { get; set; }

    }
   
}
