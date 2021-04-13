using SMSCore.Data.Entities;
using System.Threading.Tasks;

namespace SMSCore.Services.AccountsService
{
    public interface IAccountService
    {
        Task<UserMaster> ValidateUserLoginAsync(string userName,string password);
    }
}
