﻿using challenge_samsung.Models;
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

        public void Allocate()
        {
            var qtyTeams = _globalStorage.Teams.Count;
            var employees = _globalStorage.EmployeesFromFile.OrderByDescending(o => o.Level).ToList();
            _globalStorage.Teams = _globalStorage.Teams.OrderByDescending(o => o.MinMaturity).ToList();
           
            while (employees.Count != 0)
            {
                while (_globalStorage.Teams.Any(t => t.CurrentMaturity < t.MinMaturity) 
                        && employees.Count != 0)
                {
                    for (int teamIndex = 0; teamIndex < qtyTeams; teamIndex++)
                    {
                        var team = _globalStorage.Teams[teamIndex];

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

                        var team = _globalStorage.Teams[teamIndex];

                        AllocateInTeam(employees, team);
                    }
                }
            }

            ShowTeams();
        }

        private void ShowTeams()
        {
            for (int i = 0; i < _globalStorage.Teams.Count; i++)
            {
                var team = _globalStorage.Teams[i];
                Console.WriteLine($"{team.Name} - Min. Maturity {team.MinMaturity} - Current Maturity {team.CurrentMaturity}");
                for (int j = 0; j < team.Employees.Count; j++)
                {
                    var employee = team.Employees[j];
                    Console.WriteLine($"{employee.Name} - {employee.Level}");
                }

                Console.WriteLine();
            }
        }

        private void AllocateInTeam(List<Employee> employees, Team team)
        {
            var employeeIndex = 0;

            var employee = employees[employeeIndex];
            team.Employees.Add(employee);
            team.CurrentMaturity += employee.Level;
            employees.RemoveAt(employeeIndex);
        }
    }
}