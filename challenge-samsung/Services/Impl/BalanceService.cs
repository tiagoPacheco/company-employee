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
            return BalanceTeamsAndAllocateMissingEmployees(ref teams, employees);
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

        private List<Team> BalanceTeamsAndAllocateMissingEmployees(
            ref List<Team> teams, List<Employee> candidateEmployees)
        {

            var temporaryAllocationTeams = GetEmptyTeamsToAllocate(teams.Count);
            AllocateCandidatesInTemporaryTeams(candidateEmployees, ref temporaryAllocationTeams);

            var mainTeams = teams.OrderBy(t => t.ExtraMaturity).ToList();
            temporaryAllocationTeams = temporaryAllocationTeams.OrderByDescending(t => t.CurrentMaturity).ToList();

            for (int i = 0; i < mainTeams.Count; i++)
            {
                var mainTeam = mainTeams[i];
                var tempTeam = temporaryAllocationTeams[i];

                mainTeam.Employees.AddRange(tempTeam.Employees);
            }

            return mainTeams.OrderBy(mt => mt.Name).ToList();
        }

        private static void AllocateCandidatesInTemporaryTeams(List<Employee> candidateEmployees, ref List<Team> temporaryAllocationTeams)
        {
            var candidateEmployeesSorted = candidateEmployees.OrderByDescending(e => e.Level).ToList();

            for (int i = 0; i < candidateEmployeesSorted.Count; i++)
            {
                var candidateEmployee = candidateEmployeesSorted[i];
                var teamWithMinCurrentMaturity = temporaryAllocationTeams.OrderBy(t => t.CurrentMaturity).First();

                teamWithMinCurrentMaturity.Employees.Add(candidateEmployee);
            }
        }

        private List<Team> GetEmptyTeamsToAllocate(int quantityOfTeams)
        {
            var emptyTeams = new List<Team>();

            for (int i = 0; i < quantityOfTeams; i++)
            {
                emptyTeams.Add(new Team());
            }

            return emptyTeams;
        }

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
