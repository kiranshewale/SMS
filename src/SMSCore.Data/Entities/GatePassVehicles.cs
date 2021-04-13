using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class GatePassVehicles
    {
        public int Id { get; set; }
        public int GatePassId { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public int SubDealerId { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
