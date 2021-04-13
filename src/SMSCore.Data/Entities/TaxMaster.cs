using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class TaxMaster
    {
        public int Id { get; set; }
        public string TaxName { get; set; }
        public decimal? Percentage { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
