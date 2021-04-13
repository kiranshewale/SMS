using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class CustomerDetailsTable
    {
        public int Id { get; set; }
        public string Salutation { get; set; }
        public string CustomerName { get; set; }
        public decimal? MobileNo1 { get; set; }
        public decimal MobileNo2 { get; set; }
        public string EmailId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Pin { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public int? State { get; set; }
        public string PanNo { get; set; }
        public string AdharNo { get; set; }
        public string Gstno { get; set; }
        public bool IsVehicleBookedOrPurchased { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
