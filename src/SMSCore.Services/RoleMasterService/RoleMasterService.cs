using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.RoleMasterService
{
    public class RoleMasterService: IRoleMasterService
    {
        private readonly IRepository<RoleMaster> _roleMasterRepo;

        public RoleMasterService(IRepository<RoleMaster> roleMasterRepo)
        {
            _roleMasterRepo = roleMasterRepo;
        }


        public async Task InsertAsync(RoleMaster entity)
        {
            await _roleMasterRepo.AddAsync(entity);
        }


        public async Task UpdateAsync(RoleMaster entity)
        {
            await _roleMasterRepo.UpdateAsync(entity);
        }


        public async Task DaleteAsync(RoleMaster entity)
        {
            await _roleMasterRepo.RemoveAsync(entity);
        }

        public async Task<RoleMaster> GetRoleByIdAsync(int id)
        {
            return await _roleMasterRepo.TableNoTracking.Where(x=>x.Id==id).FirstOrDefaultAsync();
        }
        public async Task<IList<RoleMaster>> GetAllRolesListAsync()
        {
            return await _roleMasterRepo.TableNoTracking.ToListAsync();
        }
    }
}
