using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class GatePassDetails
    {
        public int Id { get; set; }
        public string VehicleOutTo { get; set; }
        public string VehicleOutFor { get; set; }
        public int? BranchId { get; set; }
        public string ByHand { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Status { get; set; }
        public int? ByUser { get; set; }
    }
}
