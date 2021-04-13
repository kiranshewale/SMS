using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.PDIVehiclesService
{
    public class PDIVehiclesService : IPDIVehiclesService
    {
        private readonly IRepository<PdiVehiclesDetails> _pdiRepo;

        public PDIVehiclesService(IRepository<PdiVehiclesDetails> pdiRepo)
        {
            _pdiRepo = pdiRepo;
        }

        public async Task InsertAsync(PdiVehiclesDetails entity)
        {
            await _pdiRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(PdiVehiclesDetails entity)
        {
            await _pdiRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(PdiVehiclesDetails entity)
        {
            await _pdiRepo.RemoveAsync(entity);
        }

        public async Task<PdiVehiclesDetails> GetByIdAsync(int Id)
        {
            return await _pdiRepo.TableNoTracking.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IList<PdiVehiclesDetails>> GetPdiVehiclesListAsync()
        {
            return await _pdiRepo.TableNoTracking.ToListAsync();
        }
    }
}
