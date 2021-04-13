using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class VarientMaster
    {
        public int Id { get; set; }
        public string VerientName { get; set; }
        public string FriendlyName { get; set; }
        public string VarientCode { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? ByUser { get; set; }
    }
}
