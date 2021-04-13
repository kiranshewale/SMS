using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class VehiclesMaster
    {
        public int Id { get; set; }
        public DateTime? HcilinvoiceDate { get; set; }
        public DateTime? UnloadDate { get; set; }
        public int AgingDays { get; set; }
        public string Vinnumber { get; set; }
        public string EngineNumber { get; set; }
        public string KeyNumber { get; set; }
        public int? ModelId { get; set; }
        public int? VarientId { get; set; }
        public int? ColourId { get; set; }
        public string ModelName { get; set; }
        public string ModelVarient { get; set; }
        public string ModelColour { get; set; }
        public int? ManufactureYear { get; set; }
        public bool Pdi { get; set; }
        public string InvoiceNumber { get; set; }
        public string TransporterName { get; set; }
        public string TruckRegistrationNumber { get; set; }
        public string DealerCode { get; set; }
        public string VehicleStatus { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? CreatedOrUpdatedBy { get; set; }
        public decimal? DiscountAsPerHonda { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public decimal? Igst { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? Cess { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? Ugst { get; set; }
        public decimal? TradeDiscount { get; set; }
        public string TrackStatus { get; set; }
    }
}
