using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.GatePassService
{
    public class GatePassService : IGatePassService
    {
        private readonly IRepository<GatePassDetails> _gatePassRepo;
        private readonly IRepository<GatePassVehicles> _gatePassVehiclesRepo;

        public GatePassService(IRepository<GatePassDetails> gatePassRepo,
            IRepository<GatePassVehicles> gatePassVehiclesRepo)
        {
            _gatePassRepo = gatePassRepo;
            _gatePassVehiclesRepo = gatePassVehiclesRepo;
        }

        public async Task InsertAsync(GatePassDetails entity)
        {
            await _gatePassRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(GatePassDetails entity)
        {
            await _gatePassRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(GatePassDetails entity)
        {
            await _gatePassRepo.RemoveAsync(entity);
        }

        public async Task<GatePassDetails> GetByIdAsync(int id)
        {
            return await _gatePassRepo.TableNoTracking.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<GatePassDetails>> GetGatePassVehiclesListAsync(int id)
        {
            return await _gatePassRepo.TableNoTracking.Where(x => x.Id == id).ToListAsync();
        }


        #region GatePassVehicles
        public async Task InsertGatePassVehicleAsync(GatePassVehicles gentity)
        {
            await _gatePassVehiclesRepo.AddAsync(gentity);
        }
        #endregion
    }
}
