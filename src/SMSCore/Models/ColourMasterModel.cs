using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System.Collections.Generic;

namespace SMSCore.Models
{
    public class ColourMasterModel:BaseViewModel
    {
        public ColourMasterModel()
        {
            AvaillabelVarientTypes = new List<SelectListItem>();
        }
        public string ColourName { get; set; }
        public string ColourCode { get; set; }
        public string ColourType { get; set; }
        public string FriendlyName { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }       
        public int? ByUser { get; set; }

        public List<SelectListItem> AvaillabelVarientTypes { get; set; }
    }
}
