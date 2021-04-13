using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.QuotationManagerService
{
    public interface IQuotationManagerService
    {
        Task DeleteAsync(QuotationDetails entity);
        Task<QuotationDetails> GetAllQuotationByIdAsync(int id);
        Task<IList<QuotationDetails>> GetAllQuotationsListAsync();
        Task InsertAsync(QuotationDetails entity);
        Task UpdateAsync(QuotationDetails entity);
    }
}