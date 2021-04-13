using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class PriceListMaster
    {
        public int Id { get; set; }
        public string ModelCode { get; set; }
        public string ModelVarient { get; set; }
        public string VarientType { get; set; }
        public decimal? ExShowroomPrice { get; set; }
        public decimal? FastTag { get; set; }
        public decimal? Tcs { get; set; }
        public decimal? Hainsurance { get; set; }
        public decimal? HanilDepWithKeyProtect { get; set; }
        public decimal? HartiwithEngineProtect { get; set; }
        public decimal? Rtoindividual { get; set; }
        public decimal? AccessoryComboKit { get; set; }
        public decimal? ExtendedWarranty { get; set; }
        public decimal? Rsa { get; set; }
        public decimal? ClayBar { get; set; }
        public decimal? Antirust { get; set; }
        public decimal? CarpetLamination { get; set; }
        public decimal? TotatPrice { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? ByUser { get; set; }
    }
}
