using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.QuotationManagerService
{
    public class QuotationManagerService : IQuotationManagerService
    {
        private readonly IRepository<QuotationDetails> _quotationRepo;

        public QuotationManagerService(IRepository<QuotationDetails> quotationRepo)
        {
            _quotationRepo = quotationRepo;
        }

        public async Task InsertAsync(QuotationDetails entity)
        {
            await _quotationRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(QuotationDetails entity)
        {
            await _quotationRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(QuotationDetails entity)
        {
            await _quotationRepo.RemoveAsync(entity);
        }

        public async Task<IList<QuotationDetails>> GetAllQuotationsListAsync()
        {
            return await _quotationRepo.TableNoTracking.ToListAsync();
        }

        public async Task<QuotationDetails> GetAllQuotationByIdAsync(int id)
        {
            return await _quotationRepo.TableNoTracking.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
