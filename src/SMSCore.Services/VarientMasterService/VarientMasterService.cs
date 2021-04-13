using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.VarientMasterService
{
    public class VarientMasterService : IVarientMasterService
    {
        #region fields
        private readonly IRepository<VarientMaster> _varientMasterRepo;
        private readonly IRepository<ModelVarientColourMapping> _varientColourMapRepo;
        private readonly IRepository<ColourMaster> _colourRepo;
        #endregion

        #region ctor
        public VarientMasterService(IRepository<VarientMaster> varientMasterRepo,
            IRepository<ModelVarientColourMapping> varientColourMapRepo,
            IRepository<ColourMaster> colourRepo)
        {
            _varientMasterRepo = varientMasterRepo;
            _varientColourMapRepo = varientColourMapRepo;
            _colourRepo = colourRepo;
        }
        #endregion

        #region Methods
        public async Task InsertAsync(VarientMaster entity)
        {
            await _varientMasterRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(VarientMaster entity)
        {
            await _varientMasterRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(VarientMaster entity)
        {
            await _varientMasterRepo.RemoveAsync(entity);
        }

        public VarientMaster GetVarientByIdAsync(int id)
        {
            return _varientMasterRepo.Table.Where(x => x.Id == id).FirstOrDefault();
        }

        public VarientMaster GetVarientByNameAsync(string varName)
        {
            return _varientMasterRepo.Table.Where(x => x.VerientName == varName).FirstOrDefault();
        }

        public async Task<IList<VarientMaster>> GetAllVarientsListAsync()
        {
            return await _varientMasterRepo.TableNoTracking.ToListAsync();
        }

        #endregion

        #region Verient colour mapping
        public async Task AddMappingAsync(ModelVarientColourMapping entity)
        {
            await _varientColourMapRepo.AddAsync(entity);
        }

        public async Task UpdateMappingAsync(ModelVarientColourMapping entity)
        {
            await _varientColourMapRepo.UpdateAsync(entity);
        }

        public async Task RemoveMappingAsync(ModelVarientColourMapping entity)
        {
            await _varientColourMapRepo.RemoveAsync(entity);
        }

        public async Task<List<ColourMaster>> GetVarientColoursByVarienAsync(int varientId,string varientType)
        {
            var qry = await (from colour in _colourRepo.Table
                             join varient in _varientColourMapRepo.Table
                             on colour.Id equals varient.ColourId
                             where varient.VarientId == varientId &&
                             colour.ColourType.ToUpper()==varientType.ToUpper()
                             select colour).ToListAsync();
            return qry;
        }
        #endregion
    }
}
