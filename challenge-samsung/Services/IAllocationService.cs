using challenge_samsung.Models;
using System.Collections.Generic;

namespace challenge_samsung.Services
{
    public interface IAllocationService
    {
        /// <summary>
        /// Will allocate all employees in Team. The team won't be balanced.
        /// </summary>
        List<Team> Allocate(List<Team> teams, List<Employee> employees);
    }
}
