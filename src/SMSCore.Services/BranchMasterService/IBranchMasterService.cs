using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.BranchMasterService
{
    public interface IBranchMasterService
    {
        Task DeleteAsync(BranchMaster entity);
        Task<BranchMaster> GetBranchByIdAsync(int Id);
        Task<IList<BranchMaster>> GetBranchListAsync();
        Task InsertAsync(BranchMaster entity);
        Task UpdateAsync(BranchMaster entity);
    }
}