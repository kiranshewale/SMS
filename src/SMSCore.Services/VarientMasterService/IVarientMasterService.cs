using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.VarientMasterService
{
    public interface IVarientMasterService
    {
        Task DeleteAsync(VarientMaster entity);
        Task<IList<VarientMaster>> GetAllVarientsListAsync();
        VarientMaster GetVarientByIdAsync(int id);
        Task InsertAsync(VarientMaster entity);
        Task UpdateAsync(VarientMaster entity);


        Task AddMappingAsync(ModelVarientColourMapping entity);
        Task UpdateMappingAsync(ModelVarientColourMapping entity);
        Task RemoveMappingAsync(ModelVarientColourMapping entity);
        Task<List<ColourMaster>> GetVarientColoursByVarienAsync(int varientId, string varientType);
        VarientMaster GetVarientByNameAsync(string varName);
    }
}