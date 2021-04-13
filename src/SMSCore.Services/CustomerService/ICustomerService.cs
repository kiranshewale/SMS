using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.CustomerService
{
    public interface ICustomerService
    {
        Task DeleteAsync(CustomerDetailsTable entity);
        Task<CustomerDetailsTable> GetAllCustomerByIdAsync(int id);
        Task<IList<CustomerDetailsTable>> GetAllCustomerListAsync(string email = null, string mobileNo = null, string panNo = null, string aadharNo = null);
        Task<CustomerDetailsTable> GetCustomerByAnyAsync(string term = null);
        Task InsertAsync(CustomerDetailsTable entity);
        Task UpdateAsync(CustomerDetailsTable entity);
    }
}