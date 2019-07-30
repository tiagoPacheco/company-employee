using challenge_samsung.Models;
using System.Collections.Generic;

namespace challenge_samsung.Services.Impl
{
    public interface IConsoleService
    {

        /// <summary>
        /// Show teams with their employees
        /// </summary>
        /// <param name="teams"></param>
        void ShowTeamsAndEmployees(List<Team> teams);

        /// <summary>
        /// Show initial information
        /// </summary>
        /// <param name="teams"></param>
        void ShowInitialInformation();

        /// <summary>
        /// Show teams with their employees (detail)
        /// </summary>
        /// <param name="teams"></param>
        void ShowTeamsAndEmployeesDetail(List<Team> teams);
    }
}
