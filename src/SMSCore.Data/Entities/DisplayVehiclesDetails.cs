using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class DisplayVehiclesDetails
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public int? BranchId { get; set; }
        public string ComeForm { get; set; }
        public string Purpose { get; set; }
        public bool? VehicleOut { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? OutDate { get; set; }
    }
}
