using Microsoft.AspNetCore.Mvc.Rendering;
using SMSCore.ViewModels.BaseViewModels;
using System.Collections.Generic;

namespace SMSCore.Models
{
    public class ModelsListModel : BaseViewModel
    {
        public ModelsListModel()
        {
            AvaillabelVarientsList = new List<SelectListItem>();
        }
        public string ModelName { get; set; }
        public string ModelCode { get; set; }
        public string FriendlyName { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public int? ByUser { get; set; }

        public int[] SelectedVarientIds { get; set; }
        public List<SelectListItem> AvaillabelVarientsList { get; set; }
    }
}
