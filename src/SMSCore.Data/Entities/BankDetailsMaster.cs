using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class BankDetailsMaster
    {
        public int Id { get; set; }
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string Ifsccode { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
