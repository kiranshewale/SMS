using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.ShowroomVehiclesService
{
    public class ShowroomVehiclesService : IShowroomVehiclesService
    {
        private readonly IRepository<ShowroomVehiclesDetails> _showroomVehiclesRepo;
        private readonly IRepository<VehiclesMaster> _vehMAsterRepo;

        public ShowroomVehiclesService(IRepository<ShowroomVehiclesDetails> showroomVehiclesRepo,
            IRepository<VehiclesMaster> vehMAsterRepo)
        {
            _showroomVehiclesRepo = showroomVehiclesRepo;
            _vehMAsterRepo = vehMAsterRepo;
        }

        public async Task InsertAsync(ShowroomVehiclesDetails entity)
        {
            await _showroomVehiclesRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(ShowroomVehiclesDetails entity)
        {
            await _showroomVehiclesRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(ShowroomVehiclesDetails entity)
        {
            await _showroomVehiclesRepo.RemoveAsync(entity);
        }

        public async Task<ShowroomVehiclesDetails> GetByIdAsync(int Id)
        {
            return await _showroomVehiclesRepo.TableNoTracking.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IList<ShowroomVehiclesDetails>> GetShowroomVehiclesListAsync()
        {
            return await _showroomVehiclesRepo.TableNoTracking.ToListAsync();
        }


        //get showroom stock
        public async Task<int> GetShowroomVehiclesStockAsync()
        {
            return await (from shw in _showroomVehiclesRepo.TableNoTracking
                          join veh in _vehMAsterRepo.TableNoTracking
                          on shw.VehicleId equals veh.Id
                          where veh.VehicleStatus == VehicleStatusEnum.InStock.ToString()
                          && (veh.TrackStatus == VehicleStatusEnum.Showroom.ToString()) && !shw.IsDelivered
                          select shw).CountAsync();
        }
    }
}
