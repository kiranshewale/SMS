using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.ColourMasterService
{
    public interface IColourMasterService
    {
        Task DeleteAsync(ColourMaster entity);
        Task<IList<ColourMaster>> GetAllColoursListAsync();
        ColourMaster GetColourByIdAsync(int id);
        ColourMaster GetColourByNameAsync(string colourName);
        Task InsertAsync(ColourMaster entity);
        Task UpdateAsync(ColourMaster entity);
    }
}