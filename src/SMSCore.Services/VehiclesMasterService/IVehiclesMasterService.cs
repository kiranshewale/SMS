using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace SMSCore.Services.VehiclesMasterService
{
    public interface IVehiclesMasterService
    {
        Task DeleteAsync(VehiclesMaster entity);
        Task<IList<VehiclesMaster>> GetAllVehicleDetailsAsync();
        DataTable GetVehiclesDataFromExcelFile(Stream stream);
        Task<VehiclesMaster> GetVehicleDetailsByChassisnoAsync(string vinNumber);
        Task InsertAsync(VehiclesMaster entity);
        Task UpdateAsync(VehiclesMaster entity);
        Task CalculateAgingDaysTaskAsync();
        Task<IList<VehiclesMaster>> GetVehicleListByModelVarientColurAsync(int modelId, int varientId, int colourId, int numOfVehicles = 1);
        Task<VehiclesMaster> GetByIdAsync(int Id);
        Task<int> GetAllotedVehiclesStockAsync();
        Task<int> GetAvaillableVehiclesStockAsync();
        Task<int> GetVehiclesStockAsync();
    }
}
