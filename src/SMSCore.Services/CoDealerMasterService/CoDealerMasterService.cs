using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.CoDealerMasterService
{
    public class CoDealerMasterService : ICoDealerMasterService
    {
        private readonly IRepository<SubDealerMaster> _subDealerRepo;

        public CoDealerMasterService(IRepository<SubDealerMaster> subDealerRepo)
        {
            _subDealerRepo = subDealerRepo;
        }

        public async Task InsertAsync(SubDealerMaster entity)
        {
            await _subDealerRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(SubDealerMaster entity)
        {
            await _subDealerRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(SubDealerMaster entity)
        {
            await _subDealerRepo.RemoveAsync(entity);
        }

        public async Task<IList<SubDealerMaster>> GetAllCoDealerListAsync(string email = null, string mobileNo = null)
        {
            var query = await _subDealerRepo.TableNoTracking.ToListAsync();

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.EmailId == email).ToList();

            if (!string.IsNullOrEmpty(mobileNo))
                query = query.Where(x => x.ContactNo1 == Convert.ToDecimal(mobileNo)).ToList();

            query = query.OrderByDescending(x => x.Id).ToList();

            return query;
        }

        public async Task<SubDealerMaster> GetCoDealerByAnyAsync(string term = null)
        {
            return await _subDealerRepo.TableNoTracking
                 .Where(x => x.EmailId == term || x.ContactNo1.ToString() == term || x.ContactNo2.ToString() == term).FirstOrDefaultAsync();
        }
        public async Task<SubDealerMaster> GetAllCoDealerByIdAsync(int id)
        {
            return await _subDealerRepo.TableNoTracking.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
