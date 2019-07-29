using System.Collections.Generic;
using challenge_samsung.Models;
using System.Linq;

namespace challenge_samsung.Services.Impl
{
    public class BalanceService : IBalanceService
    {
        private readonly GlobalStorage _globalStorage;

        public BalanceService(GlobalStorage globalStorage)
        {
            _globalStorage = globalStorage;
        }

        public List<Team> BalanceTeams()
        {
            

            return new List<Team>();
        }
    }
}
