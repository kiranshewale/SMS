using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace SMSCore.Services.PriceListMasterService
{
    public interface IPriceListMasterService
    {
        Task DeleteAsync(PriceListMaster entity);
        Task<IList<PriceListMaster>> GetAllPriceListAsync();
        Task<PriceListMaster> GetModelPricebyIdAsync(int id);
        Task<PriceListMaster> GetModelPriceByModelCodeAsync(string modelCode, string varientName, string varientType);
        DataTable GetPriceListDataFromExcelFile(Stream stream);
        Task InsertAsync(PriceListMaster entity);
        Task UpdateAsync(PriceListMaster entity);
    }
}
