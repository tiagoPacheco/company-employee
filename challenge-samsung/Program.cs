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

            var option = string.Empty;

            while (option != Commands.exit)
            {
                var optionSplit = Console.ReadLine().ToLower().Split();

                option = optionSplit[0];
                switch (option)
                {
                    case Commands.allocate:
                        allocationService.Allocate();
                        break;
                    case Commands.balance:
                        Console.WriteLine(Commands.balance);
                        break;
                    case Commands.load:
                        fileService.LoadFileTeam(optionSplit[1]);
                        fileService.LoadFileEmployee(optionSplit[2]);
                        Console.WriteLine("loaded");
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
    }
}
