using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class UserMaster
    {
        public UserMaster()
        {
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }
        public Guid? UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int DesignationId { get; set; }
        public int? UnderDesId { get; set; }
        public decimal? ContactNumber { get; set; }
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
