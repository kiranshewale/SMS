using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMSCore.Services.StateListService
{
    public interface IStateListService
    {
        Task<StateListMaster> GetStateById(int stateId);
        Task<IList<StateListMaster>> GetStateList();
    }
}