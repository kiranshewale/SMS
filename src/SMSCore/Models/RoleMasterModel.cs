using SMSCore.ViewModels.BaseViewModels;

namespace SMSCore.Models
{
    public class RoleMasterModel:BaseViewModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }

        public bool Active { get; set; }
    }
}
