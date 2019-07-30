using challenge_samsung.Consts;
using challenge_samsung.Services;
using challenge_samsung.Services.Impl;
using challenge_samsung.Utils;
using System;
using System.Linq;

namespace challenge_samsung
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalStorage = new GlobalStorage();
            IFileService fileService = new FileService(globalStorage);
            IAllocationService allocationService = new AllocationService(globalStorage);
            IPromotionService promotionService = new PromotionService(globalStorage);
            IBalanceService balanceService = new BalanceService(globalStorage);
            IConsoleService consoleService = new ConsoleService();

            var option = string.Empty;

            while (option != Commands.exit)
            {
                var optionSplit = GetConsoleParameters();

                option = optionSplit[0];
                try
                {
                    switch (option)
                    {
                        case Commands.allocate:
                            globalStorage.Teams = allocationService.Allocate(globalStorage.Teams, globalStorage.EmployeesFromFile);
                            consoleService.ShowTeamsAndEmployees(globalStorage.Teams);
                            break;
                        case Commands.balance:
                            globalStorage.Teams = balanceService.BalanceTeams(globalStorage.Teams);
                            consoleService.ShowTeamsAndEmployees(globalStorage.Teams);
                            break;
                        case Commands.load:
                            globalStorage.Teams = fileService.LoadFileTeam(optionSplit.ElementAtOrDefault(1));
                            globalStorage.EmployeesFromFile = fileService.LoadFileEmployee(optionSplit.ElementAtOrDefault(2));
                            Console.WriteLine(Messages.MSG002);
                            break;
                        case Commands.promote:
                            promotionService.Promote(int.Parse(optionSplit[1]));
                            break;
                        case Commands.showTeams:
                            consoleService.ShowTeamsAndEmployeesDetail(globalStorage.Teams);
                            break;
                        case Commands.exit:
                            break;
                        default:
                            Console.WriteLine(Messages.MSG001);
                            break;
                    }
                }
                catch (BusinessException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static string[] GetConsoleParameters()
        {
            return Console.ReadLine().ToLower().Split();
        }
    }
}
