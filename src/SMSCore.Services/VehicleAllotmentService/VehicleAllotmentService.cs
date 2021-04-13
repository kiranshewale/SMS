using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.VehicleAllotmentService
{
    public class VehicleAllotmentService : IVehicleAllotmentService
    {
        private readonly IRepository<VehicleAllotmentDetails> _vehicleAllotmentRepo;

        public VehicleAllotmentService(IRepository<VehicleAllotmentDetails> vehicleAllotmentRepo)
        {
            _vehicleAllotmentRepo = vehicleAllotmentRepo;
        }

        public async Task InsertAsync(VehicleAllotmentDetails entity)
        {
            await _vehicleAllotmentRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(VehicleAllotmentDetails entity)
        {
            await _vehicleAllotmentRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(VehicleAllotmentDetails entity)
        {
            await _vehicleAllotmentRepo.RemoveAsync(entity);
        }

        public async Task<VehicleAllotmentDetails> GetByIdAsync(int Id)
        {
            return await _vehicleAllotmentRepo.TableNoTracking.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IList<VehicleAllotmentDetails>> GetAllAllotedVehiclesAsync()
        {
            return await _vehicleAllotmentRepo.TableNoTracking.Where(x=>x.IsAlloted && !x.IsVehicleOutFromGodown).ToListAsync();
        }
    }
}
