using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.CoDealerMasterService
{
    public interface ICoDealerMasterService
    {
        Task DeleteAsync(SubDealerMaster entity);
        Task<SubDealerMaster> GetAllCoDealerByIdAsync(int id);
        Task<IList<SubDealerMaster>> GetAllCoDealerListAsync(string email = null, string mobileNo = null);
        Task<SubDealerMaster> GetCoDealerByAnyAsync(string term = null);
        Task InsertAsync(SubDealerMaster entity);
        Task UpdateAsync(SubDealerMaster entity);
    }
}