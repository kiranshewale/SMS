using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.ModelMasterService
{
    public interface IModelMasterService
    {
        Task AddVarientMappingAsync(ModelVarientMapping entity);
        Task DeleteAsync(ModelMaster entity);
        Task<IList<ModelMaster>> GetAllModelsListAsync();
        ModelMaster GetModelbyIdAsync(int id);
        ModelMaster GetModelbyModelCodeAsync(string modelCode);
        ModelMaster GetModelbyModelNameAsync(string modelName);
        Task<IList<ModelVarientMapping>> GetModelVarientMappingByModelId(int modelId);
        Task<List<VarientMaster>> GetModelVarientsByModelId(int modelId);
        Task InsertAsync(ModelMaster entity);

        //MOdel varient mappings
        Task RemoveVarientMappingAsync(ModelVarientMapping entity);
        Task RemoveVarientMappingByModelAsync(IList<ModelVarientMapping> varients);
        Task UpdateAsync(ModelMaster entity);
        Task UpdateVarientMappingAsync(ModelVarientMapping entity);
    }
}