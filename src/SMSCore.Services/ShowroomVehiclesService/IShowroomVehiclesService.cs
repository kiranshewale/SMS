using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.ShowroomVehiclesService
{
    public interface IShowroomVehiclesService
    {
        Task DeleteAsync(ShowroomVehiclesDetails entity);
        Task<int> GetShowroomVehiclesStockAsync();
        Task<ShowroomVehiclesDetails> GetByIdAsync(int Id);
        Task<IList<ShowroomVehiclesDetails>> GetShowroomVehiclesListAsync();
        Task InsertAsync(ShowroomVehiclesDetails entity);
        Task UpdateAsync(ShowroomVehiclesDetails entity);
    }
}