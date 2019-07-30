using challenge_samsung.Models;
using System.Collections.Generic;

namespace challenge_samsung.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Reads the Team file and store on global class
        /// </summary>
        /// <param name="file">Team file</param>
        List<Team> LoadFileTeam(string file);

        /// <summary>
        /// Reads the Employee file and store on global class
        /// </summary>
        /// <param name="file">Employees file</param>
        List<Employee> LoadFileEmployee(string file);
    }
}
