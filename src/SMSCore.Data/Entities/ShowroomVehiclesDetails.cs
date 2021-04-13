using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class ShowroomVehiclesDetails
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public int? CustomerId { get; set; }
        public int? ScId { get; set; }
        public int SubDealerId { get; set; }
        public string VehicleCameFrom { get; set; }
        public bool AccountsClearance { get; set; }
        public bool IscheckListClear { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? DateDelivered { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? ByUser { get; set; }
        public int BranchId { get; set; }
    }
}
