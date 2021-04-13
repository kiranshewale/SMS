using Microsoft.EntityFrameworkCore;
using SMSCore.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSCore.Services.StateListService
{
    public class StateListService : IStateListService
    {
        private readonly IRepository<StateListMaster> _stateListRepo;

        public StateListService(IRepository<StateListMaster> stateListRepo)
        {
            _stateListRepo = stateListRepo;
        }

        public async Task<IList<StateListMaster>> GetStateList()
        {
            return await _stateListRepo.TableNoTracking.ToListAsync();
        }

        public async Task<StateListMaster> GetStateById(int stateId)
        {
            return await _stateListRepo.TableNoTracking.Where(x=>x.Id==stateId).FirstOrDefaultAsync();
        }
    }
}
