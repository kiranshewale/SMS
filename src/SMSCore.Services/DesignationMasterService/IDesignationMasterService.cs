using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.DesignationMasterService
{
    public interface IDesignationMasterService
    {
        Task DeleteAsync(DesignationMaster entity);
        Task<IList<DesignationMaster>> GetAllDesignationsListAsync();
        DesignationMaster GetDesignationbyIdAsync(int id);
        Task InsertAsync(DesignationMaster entity);
        Task UpdateAsync(DesignationMaster entity);
    }
}
