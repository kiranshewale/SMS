using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.ColourMasterService
{
    public class ColourMasterService : IColourMasterService
    {
        private readonly IRepository<ColourMaster> _colourMasterRepo;

        public ColourMasterService(IRepository<ColourMaster> colourMasterRepo)
        {
            _colourMasterRepo = colourMasterRepo;
        }

        public async Task InsertAsync(ColourMaster entity)
        {
            await _colourMasterRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(ColourMaster entity)
        {
            await _colourMasterRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(ColourMaster entity)
        {
            await _colourMasterRepo.RemoveAsync(entity);
        }

        public ColourMaster GetColourByIdAsync(int id)
        {
            return _colourMasterRepo.Table.Where(x => x.Id == id).FirstOrDefault();
        }

        public ColourMaster GetColourByNameAsync(string colourName)
        {
            return _colourMasterRepo.Table.Where(x => x.ColourName == colourName).FirstOrDefault();
        }

        public async Task<IList<ColourMaster>> GetAllColoursListAsync()
        {
            return await _colourMasterRepo.TableNoTracking.ToListAsync();
        }
    }
}
