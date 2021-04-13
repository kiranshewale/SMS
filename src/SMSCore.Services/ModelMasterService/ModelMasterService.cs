using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.ModelMasterService
{
    public class ModelMasterService : IModelMasterService
    {
        private readonly IRepository<ModelMaster> _modelMasterRepo;
        private readonly IRepository<ModelVarientMapping> _modelVarientMasterRepo;
        private readonly IRepository<VarientMaster> _varientRepo;

        public ModelMasterService(IRepository<ModelMaster> modelMasterRepo,
            IRepository<ModelVarientMapping> modelVarientMasterRepo,
            IRepository<VarientMaster> varientRepo)
        {
            _modelMasterRepo = modelMasterRepo;
            _modelVarientMasterRepo = modelVarientMasterRepo;
            _varientRepo = varientRepo;
        }


        public async Task InsertAsync(ModelMaster entity)
        {
            await _modelMasterRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(ModelMaster entity)
        {
            await _modelMasterRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(ModelMaster entity)
        {
            await _modelMasterRepo.RemoveAsync(entity);
        }

        public ModelMaster GetModelbyIdAsync(int id)
        {
            return _modelMasterRepo.Table.Where(x => x.Id == id).FirstOrDefault();
        }

        public ModelMaster GetModelbyModelCodeAsync(string modelCode)
        {
            return _modelMasterRepo.Table.Where(x => x.ModelCode == modelCode).FirstOrDefault();
        }

        public ModelMaster GetModelbyModelNameAsync(string modelName)
        {
            return _modelMasterRepo.Table.Where(x => x.ModelName == modelName).FirstOrDefault();
        }

        public async Task<IList<ModelMaster>> GetAllModelsListAsync()
        {
            return await _modelMasterRepo.TableNoTracking.ToListAsync();
        }

        #region Model Varient Mapping
        public async Task AddVarientMappingAsync(ModelVarientMapping entity)
        {
            await _modelVarientMasterRepo.AddAsync(entity);
        }

        public async Task UpdateVarientMappingAsync(ModelVarientMapping entity)
        {
            await _modelVarientMasterRepo.UpdateAsync(entity);
        }

        public async Task RemoveVarientMappingAsync(ModelVarientMapping entity)
        {
            await _modelVarientMasterRepo.RemoveAsync(entity);
        }

        public async Task RemoveVarientMappingByModelAsync(IList<ModelVarientMapping> varients)
        {
            foreach (var map in varients)
            {
                await _modelVarientMasterRepo.RemoveAsync(map);
            }
        }

        public async Task<IList<ModelVarientMapping>> GetModelVarientMappingByModelId(int modelId)
        {
            return await _modelVarientMasterRepo.TableNoTracking.Where(x => x.ModelId == modelId).ToListAsync();
        }

        public async Task<List<VarientMaster>> GetModelVarientsByModelId(int modelId)
        {
            var qry = await (from varient in _varientRepo.Table
                             join model in _modelVarientMasterRepo.Table
                             on varient.Id equals model.VarientId
                             where model.ModelId==modelId
                             select varient).ToListAsync();
            return qry;
        }
        #endregion
    }
}
