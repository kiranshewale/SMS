using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class SubDealerMaster
    {
        public int Id { get; set; }
        public string SubDealerName { get; set; }
        public string EmailId { get; set; }
        public decimal? ContactNo1 { get; set; }
        public decimal? ContactNo2 { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string DealerCode { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
