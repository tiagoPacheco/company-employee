using System.Collections.Generic;
using challenge_samsung.Models;
using System.Linq;
using challenge_samsung.Utils;

namespace challenge_samsung.Services.Impl
{
    public class BalanceService : IBalanceService
    {
        private readonly GlobalStorage _globalStorage;

        public BalanceService(GlobalStorage globalStorage)
        {
            _globalStorage = globalStorage;
        }

        public List<Team> BalanceTeams(List<Team> teams)
        {
            ValidateIfTeamExists(teams);
            ValidateIfEmployeesExists(_globalStorage.EmployeesInTeam);

            var teamsOrded = teams.OrderByDescending(o => o.MinMaturity).ToList();
            var employees = _globalStorage.EmployeesInTeam.OrderByDescending(o => o.Level).ToList();
            employees.ForEach(e => e.IsInAnyTeam = false);

            List<Team> newTeams = AllocateEmployeesAtLeastTeamMaturity(teamsOrded, employees);
            BalanceTeamsAndAllocateMissingEmployees(ref teams, ref employees);

            return teams.OrderBy(t => t.Name).ToList();
        }

        private static List<Team> AllocateEmployeesAtLeastTeamMaturity(List<Team> teams, List<Employee> employees)
        {
            var newTeams = new List<Team>();

            for (int teamIndex = 0; teamIndex < teams.Count; teamIndex++)
            {
                var team = teams[teamIndex];
                team.Employees = new List<Employee>();

                var employeesAbleCurrentTeam = employees.Where(e => !e.IsInAnyTeam && e.Level <= team.MinMaturity).ToList();

                for (int employeeIndex = 0; employeeIndex < employeesAbleCurrentTeam.Count; employeeIndex++)
                {
                    var employee = employeesAbleCurrentTeam[employeeIndex];

                    if (team.CurrentMaturity >= team.MinMaturity)
                        break;
                    else if (employee.Level <= team.MissingMaturity)
                    {
                        employee.IsInAnyTeam = true;
                        team.Employees.Add(employee);
                        employees.Remove(employee);
                    }
                }

                newTeams.Add(team);
            }

            return newTeams;
        }

        private static void BalanceTeamsAndAllocateMissingEmployees(
            ref List<Team> teams, ref List<Employee> candidateEmployees)
        {

            var tempTeams = new List<Team>();

            for (int i = 0; i < teams.Count; i++)
            {
                tempTeams.Add(new Team());
            }

            candidateEmployees = candidateEmployees.OrderByDescending(e => e.Level).ToList();

            candidateEmployees.ForEach(ce =>
            {
                var team = tempTeams.OrderBy(t => t.CurrentMaturity).First();

                team.Employees.Add(ce);
            });


            var mainTeams = teams.OrderBy(t => t.ExtraMaturity).ToList();
            tempTeams = tempTeams.OrderByDescending(t => t.CurrentMaturity).ToList();

            for (int i = 0; i < mainTeams.Count; i++)
            {
                var mainTeam = mainTeams[i];
                var tempTeam = tempTeams[i];

                mainTeam.Employees.AddRange(tempTeam.Employees);
            }




            //while (candidateEmployees.Count != 0)
            //{
            //    teams = teams.OrderBy(t => t.ExtraMaturity).ToList();

            //    for (int teamIndex = 0; teamIndex < teams.Count; teamIndex++)
            //    {
            //        var team = teams[teamIndex];
            //        candidateEmployees = candidateEmployees.OrderByDescending(e => e.Level).ToList();

            //        for (int i = 0; i < candidateEmployees.Count; i++)
            //        {
            //            var candidateEmployee = candidateEmployees[i];

            //            var allocatedEmployee = team.Employees.FirstOrDefault(e => e.Level < candidateEmployee.Level);

            //            if (allocatedEmployee != null)
            //            {
            //                allocatedEmployee.IsInAnyTeam = false;
            //                team.Employees.Remove(allocatedEmployee);
            //                candidateEmployees.Add(allocatedEmployee);
            //            }

            //            candidateEmployee.IsInAnyTeam = true;
            //            team.Employees.Add(candidateEmployee);
            //            candidateEmployees.Remove(candidateEmployee);
            //            break;

            //        }
            //    }
            //}
        }

        //private static void BalanceTeamsAndAllocateMissingEmployees(
        //    ref List<Team> teams, ref List<Employee> candidateEmployees)
        //{
        //    while (candidateEmployees.Count != 0)
        //    {
        //        teams = teams.OrderBy(t => t.ExtraMaturity).ToList();

        //        for (int teamIndex = 0; teamIndex < teams.Count; teamIndex++)
        //        {
        //            var team = teams[teamIndex];
        //            candidateEmployees = candidateEmployees.OrderByDescending(e => e.Level).ToList();

        //            for (int i = 0; i < candidateEmployees.Count; i++)
        //            {
        //                var candidateEmployee = candidateEmployees[i];

        //                var allocatedEmployee = team.Employees.FirstOrDefault(e => e.Level < candidateEmployee.Level);

        //                if (allocatedEmployee != null)
        //                {
        //                    allocatedEmployee.IsInAnyTeam = false;
        //                    team.Employees.Remove(allocatedEmployee);
        //                    candidateEmployees.Add(allocatedEmployee);
        //                }

        //                candidateEmployee.IsInAnyTeam = true;
        //                team.Employees.Add(candidateEmployee);
        //                candidateEmployees.Remove(candidateEmployee);
        //                break;

        //            }
        //        }
        //    }
        //}

        private void ValidateIfTeamExists(List<Team> teams)
        {
            if (teams == null || !teams.Any())
            {
                throw new BusinessException(Messages.MSG006);
            }
        }

        private void ValidateIfEmployeesExists(List<Employee> employees)
        {
            if (employees == null || !employees.Any())
            {
                throw new BusinessException(Messages.MSG007);
            }
        }
    }
}
