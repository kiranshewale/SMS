using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.BranchMasterService
{
    public class BranchMasterService : IBranchMasterService
    {
        private readonly IRepository<BranchMaster> _branchMasterRepo;

        public BranchMasterService(IRepository<BranchMaster> branchMasterRepo)
        {
            _branchMasterRepo = branchMasterRepo;
        }

        public async Task InsertAsync(BranchMaster entity)
        {
            await _branchMasterRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(BranchMaster entity)
        {
            await _branchMasterRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(BranchMaster entity)
        {
            await _branchMasterRepo.RemoveAsync(entity);
        }

        public async Task<BranchMaster> GetBranchByIdAsync(int Id)
        {
            return await _branchMasterRepo.TableNoTracking.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IList<BranchMaster>> GetBranchListAsync()
        {
            return await _branchMasterRepo.TableNoTracking.ToListAsync();
        }
    }
}
