﻿using System.Collections.Generic;
using challenge_samsung.Models;
using System.Linq;

namespace challenge_samsung.Services.Impl
{
    public class BalanceService : IBalanceService
    {
        private readonly GlobalStorage _globalStorage;

        public BalanceService(GlobalStorage globalStorage)
        {
            _globalStorage = globalStorage;
        }

        public List<Team> BalanceTeams()
        {

            var teams = _globalStorage.Teams.OrderByDescending(o => o.MinMaturity).ToList();
            var employees = _globalStorage.EmployeesFromFile.OrderByDescending(o => o.Level).ToList();
            employees.ForEach(e => e.IsInAnyTeam = false);

            List<Team> newTeams = AllocateEmployeesAtLeastTeamMaturity(teams, employees);
            BalanceTeamsAndAllocateMissingEmployees(ref teams, ref employees);

            return teams;
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
            ref List<Team> teams, ref List<Employee> employees)
        {
            while (employees.Count != 0)
            {
                teams = teams.OrderBy(t => t.CurrentMaturity - t.MinMaturity).ToList();

                for (int teamIndex = 0; teamIndex < teams.Count; teamIndex++)
                {
                    var team = teams[teamIndex];
                    employees = employees.OrderByDescending(e => e.Level).ToList();

                    for (int i = 0; i < employees.Count; i++)
                    {
                        var employee = employees[i];

                        var employeeToRemove = team.Employees.FirstOrDefault(e => e.Level < employee.Level);

                        if (employeeToRemove != null)
                        {
                            employeeToRemove.IsInAnyTeam = false;
                            team.Employees.Remove(employeeToRemove);
                            employees.Add(employeeToRemove);
                        }
                        else
                        {
                            team.Employees.Add(employee);
                            employees.Remove(employee);
                            break;
                        }

                    }
                }
            }
        }
    }
}
