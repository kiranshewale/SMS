using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.VehicleAllotmentService
{
    public interface IVehicleAllotmentService
    {
        Task DeleteAsync(VehicleAllotmentDetails entity);
        Task<IList<VehicleAllotmentDetails>> GetAllAllotedVehiclesAsync();
        Task<VehicleAllotmentDetails> GetByIdAsync(int Id);
        Task InsertAsync(VehicleAllotmentDetails entity);
        Task UpdateAsync(VehicleAllotmentDetails entity);
    }
}