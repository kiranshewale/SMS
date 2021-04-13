using System;

namespace SMSCore.ViewModels.BaseViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool ContinueEditing { get; set; }
    }
}
