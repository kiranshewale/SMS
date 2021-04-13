using SMSCore.ViewModels.BaseViewModels;
using System;

namespace SMSCore.Models
{
    public class VehiclesMasterModel:BaseViewModel
    {
        public DateTime? HcilinvoiceDate { get; set; }
        public DateTime? UnloadDate { get; set; }
        public int AgingDays { get; set; }
        public string Vinnumber { get; set; }
        public string EngineNumber { get; set; }
        public string KeyNumber { get; set; }
        public string ModelName { get; set; }
        public string ModelVarient { get; set; }
        public string ModelColour { get; set; }
        public int? ManufactureYear { get; set; }
        public string InvoiceNumber { get; set; }
        public long? TransporterName { get; set; }
        public string TruckRegistrationNumber { get; set; }
    }
}
