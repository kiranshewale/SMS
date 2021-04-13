using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMSCore.Data.Entities
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
