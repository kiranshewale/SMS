using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class DesignationMaster
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
