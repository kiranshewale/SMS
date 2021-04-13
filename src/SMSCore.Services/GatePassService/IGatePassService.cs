using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.GatePassService
{
    public interface IGatePassService
    {
        Task DeleteAsync(GatePassDetails entity);
        Task<GatePassDetails> GetByIdAsync(int id);
        Task<IList<GatePassDetails>> GetGatePassVehiclesListAsync(int id);
        Task InsertAsync(GatePassDetails entity);
        Task InsertGatePassVehicleAsync(GatePassVehicles gentity);
        Task UpdateAsync(GatePassDetails entity);
    }
}