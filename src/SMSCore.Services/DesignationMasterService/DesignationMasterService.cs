using SMSCore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace SMSCore.Services.DesignationMasterService
{
    public class DesignationMasterService: IDesignationMasterService
    {
        private readonly IRepository<DesignationMaster> _designationRepo;

        public DesignationMasterService(IRepository<DesignationMaster> designationRepo)
        {
            _designationRepo = designationRepo;
        }

        public async Task InsertAsync(DesignationMaster entity)
        {
            await _designationRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(DesignationMaster entity)
        {
            await _designationRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(DesignationMaster entity)
        {
            await _designationRepo.RemoveAsync(entity);
        }

        public DesignationMaster GetDesignationbyIdAsync(int id)
        {
            return _designationRepo.Table.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<IList<DesignationMaster>> GetAllDesignationsListAsync()
        {
            return await _designationRepo.TableNoTracking.ToListAsync();
        }
    }
}
