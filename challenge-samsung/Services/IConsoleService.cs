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
        void ShowClassAndEmployees(List<Team> teams);

        void ShowInitialInformation();
    }
}
