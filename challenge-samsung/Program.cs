using challenge_samsung.Consts;
using challenge_samsung.Services;
using challenge_samsung.Services.Impl;
using System;

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
                switch (option)
                {
                    case Commands.allocate:
                        allocationService.Allocate();
                        consoleService.ShowClassAndEmployees(globalStorage.Teams);
                        break;
                    case Commands.balance:
                        globalStorage.Teams = balanceService.BalanceTeams();
                        consoleService.ShowClassAndEmployees(globalStorage.Teams);
                        break;
                    case Commands.load:
                        globalStorage.Teams = fileService.LoadFileTeam(optionSplit[1]);
                        globalStorage.EmployeesFromFile = fileService.LoadFileEmployee(optionSplit[2]);
                        Console.WriteLine(Messages.MSG002);
                        break;
                    case Commands.promote:
                        promotionService.Promote(int.Parse(optionSplit[1]));
                        break;
                    case Commands.exit:
                        break;
                    default:
                        Console.WriteLine(Messages.MSG001);
                        break;
                }
            }
        }

        private static string[] GetConsoleParameters()
        {
            return Console.ReadLine().ToLower().Split();
        }
    }
}
