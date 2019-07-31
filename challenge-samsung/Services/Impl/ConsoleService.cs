using challenge_samsung.Consts;
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

        public void ShowTeamsAndEmployees(List<Team> teams)
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

        public void ShowTeamsAndEmployeesDetail(List<Team> teams)
        {
            for (int i = 0; i < teams.Count; i++)
            {
                var team = teams[i];
                Console.WriteLine($"{team.Name} - Min. Maturity {team.MinMaturity} - Current Maturity {team.CurrentMaturity}");
                for (int j = 0; j < team.Employees.Count; j++)
                {
                    var employee = team.Employees[j];
                    Console.WriteLine(
                        string.Format(Messages.MSG005, employee.Name, employee.Level, employee.BirthYear, employee.AdmissionYear, employee.LastProgressionYear));
                }

                Console.WriteLine();
            }
        }

        public void ShowInitialInformation()
        {
            int maxAsterisk = 40;
            string title = "Company Employees Balancing";
            string commands = "Enter one of the above commands";

            showAsterisk(2, maxAsterisk, "*", true);
            showAsterisk(1, (maxAsterisk - title.Length) / 2, "*", false);
            Console.Write(title);
            showAsterisk(1, 1 + (maxAsterisk - title.Length) / 2, "*", true);
            showAsterisk(2, maxAsterisk, "*", true);

            showAsterisk(1, (maxAsterisk - commands.Length) / 2, "*", false);
            Console.Write(commands);
            showAsterisk(1, 1 + (maxAsterisk - commands.Length) / 2, "*", true);

            Console.WriteLine(Commands.load);
            Console.WriteLine(Commands.allocate);
            Console.WriteLine(Commands.promote);
            Console.WriteLine(Commands.balance);
            Console.WriteLine(Commands.exit);
            showAsterisk(1, maxAsterisk, "*", true);

        }


        private void showAsterisk(int rows, int asterisk, string charater, bool jumpLine)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < asterisk; j++)
                {
                    Console.Write(charater);

                }
                if (jumpLine)
                    Console.WriteLine();
            }
        }
    }
}
