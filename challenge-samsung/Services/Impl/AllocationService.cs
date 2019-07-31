using challenge_samsung.Models;
using challenge_samsung.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace challenge_samsung.Services.Impl
{
    public class AllocationService : IAllocationService
    {
        private readonly GlobalStorage _globalStorage;

        public AllocationService(GlobalStorage globalStorage)
        {
            _globalStorage = globalStorage;
        }

        public List<Team> Allocate(List<Team> teams, List<Employee> employees)
        {

            ValidateIfTeamExists(teams);
            ValidateIfEmployeesExists(employees);

            var qtyTeams = teams.Count;
            employees = employees.OrderByDescending(o => o.Level).ToList();
            teams = teams.OrderByDescending(o => o.MinMaturity).ToList();
           
            while (employees.Count != 0)
            {
                while (teams.Any(t => t.CurrentMaturity < t.MinMaturity) 
                        && employees.Count != 0)
                {
                    for (int teamIndex = 0; teamIndex < qtyTeams; teamIndex++)
                    {
                        var team = teams[teamIndex];

                        if (employees.Count == 0) break;

                        if (team.CurrentMaturity >= team.MinMaturity) continue;

                        AllocateInTeam(employees, team);
                    }
                }

                if (employees.Count >= 0)
                {
                    for (int teamIndex = 0; teamIndex < qtyTeams; teamIndex++)
                    {
                        if (employees.Count == 0) break;

                        var team = teams[teamIndex];

                        AllocateInTeam(employees, team);
                    }
                }
            }

            return teams.OrderBy(t => t.Name).ToList();
        }

        private void AllocateInTeam(List<Employee> employees, Team team)
        {
            var employeeIndex = 0;

            var employee = employees[employeeIndex];
            employee.IsInAnyTeam = true;
            team.Employees.Add(employee);
            employees.RemoveAt(employeeIndex);
        }

        private void ValidateIfTeamExists(List<Team> teams)
        {
            if(teams == null || !teams.Any())
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
