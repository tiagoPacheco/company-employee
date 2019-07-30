using challenge_samsung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace challenge_samsung.Services.Impl
{
    public class ConsoleService : IConsoleService
    {

        public ConsoleService()
        {
        }

        public void ShowClassAndEmployees(List<Team> teams)
        {
            for (int i = 0; i < teams.Count; i++)
            {
                var team = teams[i];
                Console.WriteLine($"{team.Name} - Min. Maturity {team.MinMaturity} - Current Maturity {team.CurrentMaturity}");
                for (int j = 0; j < team.Employees.Count; j++)
                {
                    var employee = team.Employees[j];
                    Console.WriteLine($"{employee.Name} - {employee.Level}");
                }

                Console.WriteLine();
            }
        }

        public void ShowInitialInformation()
        {
            throw new NotImplementedException();
        }
    }
}
