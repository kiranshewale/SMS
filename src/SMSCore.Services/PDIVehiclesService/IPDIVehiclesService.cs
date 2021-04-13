using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.PDIVehiclesService
{
    public interface IPDIVehiclesService
    {
        Task DeleteAsync(PdiVehiclesDetails entity);
        Task<PdiVehiclesDetails> GetByIdAsync(int Id);
        Task<IList<PdiVehiclesDetails>> GetPdiVehiclesListAsync();
        Task InsertAsync(PdiVehiclesDetails entity);
        Task UpdateAsync(PdiVehiclesDetails entity);
    }
}