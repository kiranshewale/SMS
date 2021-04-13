using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class QuotationDetails
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public int? ModelId { get; set; }
        public int? VarientId { get; set; }
        public string VarientType { get; set; }
        public int? ColourId { get; set; }
        public int? CustomerId { get; set; }
        public int? ScId { get; set; }
        public string LeadSource { get; set; }
        public decimal? BookingAmount { get; set; }
        public bool IsExchange { get; set; }
        public string ExchangeVehicleDetails { get; set; }
        public DateTime? DateCretaed { get; set; }
        public int? CreatedBy { get; set; }
    }
}
