using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.AccountsService
{
    public class AccountService: IAccountService
    {
        #region Fields
        private readonly IRepository<UserMaster> _userMasterRepo;
        #endregion

        #region ctor
        public AccountService(IRepository<UserMaster> userMasterRepo)
        {
            _userMasterRepo = userMasterRepo;
        }

        #endregion

        #region Methods
        public async Task<UserMaster> ValidateUserLoginAsync(string userName, string password)
        {
            var qry= await _userMasterRepo.TableNoTracking
                        .Where(x => x.UserName == userName 
                         && x.Password == password).SingleOrDefaultAsync();
            return qry;
        
        }
        #endregion
    }
}
