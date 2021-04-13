using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<CustomerDetailsTable> _customerRepo;

        public CustomerService(IRepository<CustomerDetailsTable> customerRepo)
        {
            _customerRepo = customerRepo;
        }


        public async Task InsertAsync(CustomerDetailsTable entity)
        {
            await _customerRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(CustomerDetailsTable entity)
        {
            await _customerRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(CustomerDetailsTable entity)
        {
            await _customerRepo.RemoveAsync(entity);
        }

        public async Task<IList<CustomerDetailsTable>> GetAllCustomerListAsync(string email = null, string mobileNo = null, string panNo = null, string aadharNo = null)
        {
            var query = await _customerRepo.TableNoTracking.ToListAsync();

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.EmailId == email).ToList();

            if (!string.IsNullOrEmpty(mobileNo))
                query = query.Where(x => x.MobileNo1 == Convert.ToDecimal(mobileNo)).ToList();

            if (!string.IsNullOrEmpty(panNo))
                query = query.Where(x => x.PanNo == panNo).ToList();

            if (!string.IsNullOrEmpty(aadharNo))
                query = query.Where(x => x.AdharNo == aadharNo).ToList();

               query = query.OrderByDescending(x => x.Id).ToList();

            return query;
        }

        public async Task<CustomerDetailsTable> GetCustomerByAnyAsync(string term = null)
        {
           return await _customerRepo.TableNoTracking
                .Where(x=>x.EmailId==term || x.MobileNo1.ToString()== term || x.PanNo==term).FirstOrDefaultAsync();         
        }
        public async Task<CustomerDetailsTable> GetAllCustomerByIdAsync(int id)
        {
            return await _customerRepo.TableNoTracking.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
