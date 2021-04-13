using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.UserMasterService
{
   public class UserMasterService: IUserMasterService
    {
        private readonly IRepository<UserMaster> _userMasterRepo;
        private readonly IRepository<DesignationMaster> _designationRepo;

        public UserMasterService(IRepository<UserMaster> userMasterRepo,
            IRepository<DesignationMaster> designationRepo)
        {
            _userMasterRepo = userMasterRepo;
            _designationRepo = designationRepo;
        }

        public async Task InsertAsync(UserMaster entity)
        {
            await _userMasterRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(UserMaster entity)
        {
            await _userMasterRepo.UpdateAsync(entity);
        }

        public async Task DelateAsync(UserMaster entity)
        {
            await _userMasterRepo.RemoveAsync(entity);
        }
        public async Task<IList<UserMaster>> GetAllUsers()
        {
            return await _userMasterRepo.TableNoTracking.ToListAsync();
        }

        public async Task<IList<UserMaster>> GetSMandTLList()
        {
            return await (from user in _userMasterRepo.Table
                          join des in _designationRepo.Table
                          on user.DesignationId equals des.Id
                          where des.Designation.ToLower()=="sales managaer" || des.Designation.ToLower()=="team leader"
                          select user).ToListAsync();
        }
        public async Task<UserMaster> GetUserByIdAsync(int id)
        {
            return await _userMasterRepo.TableNoTracking.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
