using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SMSCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.PriceListMasterService
{
    public class PriceListMasterService: IPriceListMasterService
    {
        private readonly IRepository<PriceListMaster> _pricListRepo;

        public PriceListMasterService(IRepository<PriceListMaster> pricListRepo)
        {
            _pricListRepo = pricListRepo;
        }

        public async Task InsertAsync(PriceListMaster entity)
        {
            await _pricListRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(PriceListMaster entity)
        {
            await _pricListRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(PriceListMaster entity)
        {
            await _pricListRepo.RemoveAsync(entity);
        }

        public async Task<PriceListMaster> GetModelPricebyIdAsync(int id)
        {
            return await _pricListRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PriceListMaster> GetModelPriceByModelCodeAsync(string modelCode,string varientName, string varientType)
        {
            return await _pricListRepo.Table.Where(x => x.ModelCode == modelCode && x.ModelVarient==varientName && x.VarientType== varientType).FirstOrDefaultAsync();
        }

        public async Task<IList<PriceListMaster>> GetAllPriceListAsync()
        {
            return await _pricListRepo.TableNoTracking.ToListAsync();
        }

        public DataTable GetPriceListDataFromExcelFile(Stream stream)
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
    }
    
}
