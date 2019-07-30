using challenge_samsung.Models;
using System.Collections.Generic;

namespace challenge_samsung.Services
{
    public interface IBalanceService
    {
        List<Team> BalanceTeams(List<Team> team);
    }
}
