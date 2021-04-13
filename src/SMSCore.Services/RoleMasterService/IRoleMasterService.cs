using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.RoleMasterService
{
    public interface IRoleMasterService
    {
        Task DaleteAsync(RoleMaster entity);
        Task<IList<RoleMaster>> GetAllRolesListAsync();
        Task<RoleMaster> GetRoleByIdAsync(int id);
        Task InsertAsync(RoleMaster entity);
        Task UpdateAsync(RoleMaster entity);
    }
}
