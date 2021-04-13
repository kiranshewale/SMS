using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class VehicleAllotmentDetails
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public int? CustomerId { get; set; }
        public int? Scid { get; set; }
        public bool IsAlloted { get; set; }
        public int? AllotedBy { get; set; }
        public int? ApprovedBy { get; set; }
        public int SubDealer { get; set; }
        public bool IsVehicleOutFromGodown { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? ModelId { get; set; }
        public int? VarientId { get; set; }
        public int? ColourId { get; set; }
        public string VarientType { get; set; }
        public int? BranchId { get; set; }
    }
}
