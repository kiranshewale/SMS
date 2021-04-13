using SMSCore.ViewModels.BaseViewModels;
using System;

namespace SMSCore.Models
{
    public class DesignationMasterModel: BaseViewModel
    {
        public string Designation { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
