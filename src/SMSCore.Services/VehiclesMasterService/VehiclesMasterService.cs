using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SMSCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.VehiclesMasterService
{
    public class VehiclesMasterService : IVehiclesMasterService
    {
        private readonly IRepository<VehiclesMaster> _vehiclesMasterRepo;
        private readonly IRepository<VehicleAllotmentDetails> _vehiclesAllotementRepo;

        public VehiclesMasterService(IRepository<VehiclesMaster> vehiclesMasterRepo,
            IRepository<VehicleAllotmentDetails> vehiclesAllotementRepo)
        {
            _vehiclesMasterRepo = vehiclesMasterRepo;
            _vehiclesAllotementRepo = vehiclesAllotementRepo;
        }

        public async Task InsertAsync(VehiclesMaster entity)
        {
            await _vehiclesMasterRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(VehiclesMaster entity)
        {
            await _vehiclesMasterRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(VehiclesMaster entity)
        {
            await _vehiclesMasterRepo.RemoveAsync(entity);
        }

        public async Task<VehiclesMaster> GetVehicleDetailsByChassisnoAsync(string vinNumber)
        {
            return await _vehiclesMasterRepo.TableNoTracking.Where(x => x.Vinnumber == vinNumber).FirstOrDefaultAsync();
        }

        public async Task<VehiclesMaster> GetByIdAsync(int Id)
        {
            return await _vehiclesMasterRepo.TableNoTracking.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IList<VehiclesMaster>> GetAllVehicleDetailsAsync()
        {
            return await _vehiclesMasterRepo.TableNoTracking.ToListAsync();
        }
        public async Task<IList<VehiclesMaster>> GetVehicleListByModelVarientColurAsync(int modelId, int varientId, int colourId,int numOfVehicles=1)
        {
            return await _vehiclesMasterRepo.TableNoTracking
                .Where(x => x.ModelId == modelId && x.VarientId == varientId
                && x.ColourId == colourId && x.VehicleStatus==VehicleStatusEnum.InStock.ToString() && x.TrackStatus==VehicleStatusEnum.Avail.ToString()).OrderByDescending(c=>c.AgingDays).Take(numOfVehicles).ToListAsync();
        }

        public DataTable GetVehiclesDataFromExcelFile(Stream stream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new Exception("No worksheet found");


                DataTable table = new DataTable();
                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    table.Columns.Add(firstRowCell.Text);
                }

                for (var rowNumber = 2; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
                {
                    var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];
                    var newRow = table.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                    table.Rows.Add(newRow);

                }
                return table;



            }
        }


        public async Task CalculateAgingDaysTaskAsync()
        {
            var query = await _vehiclesMasterRepo.Table.Where(x => x.VehicleStatus == "InStock").ToListAsync();
            foreach (var item in query)
            {
                item.AgingDays = (DateTime.UtcNow.Date - Convert.ToDateTime(item.UnloadDate).Date).Days;

                await _vehiclesMasterRepo.UpdateAsync(item);
            }

        }


        //calculate stock
        public async Task<int> GetVehiclesStockAsync()
        {
            return await _vehiclesMasterRepo.TableNoTracking.Where(x => x.VehicleStatus == "InStock").CountAsync();
        }

        public async Task<int> GetAllotedVehiclesStockAsync()
        {
            return await( from veh in _vehiclesMasterRepo.TableNoTracking
                          join allot in _vehiclesAllotementRepo.TableNoTracking
                          on veh.Id equals allot.VehicleId
                          where veh.VehicleStatus == VehicleStatusEnum.InStock.ToString()
                          && (veh.TrackStatus==VehicleStatusEnum.Alloted.ToString() || veh.TrackStatus==VehicleStatusEnum.PartiallyAlloted.ToString()) && !allot.IsVehicleOutFromGodown
                          select veh).CountAsync();
        }

        public async Task<int> GetAvaillableVehiclesStockAsync()
        {
            return await _vehiclesMasterRepo.TableNoTracking.Where(x=>x.TrackStatus==VehicleStatusEnum.Avail.ToString()).CountAsync();
        }
    }
}
