using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.UserMasterService
{
    public interface IUserMasterService
    {
        Task DelateAsync(UserMaster entity);
        Task<IList<UserMaster>> GetAllUsers();
        Task<IList<UserMaster>> GetSMandTLList();
        Task<UserMaster> GetUserByIdAsync(int id);
        Task InsertAsync(UserMaster entity);
        Task UpdateAsync(UserMaster entity);
    }
}
