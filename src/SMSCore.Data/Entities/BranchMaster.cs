using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class BranchMaster
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public decimal? ContactNo { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
