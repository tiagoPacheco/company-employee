using challenge_samsung.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace challenge_samsung.Services.Impl
{
    public class PromotionService : IPromotionService
    {
        private readonly GlobalStorage _globalStorage;

        public PromotionService(GlobalStorage globalStorage)
        {
            _globalStorage = globalStorage;
        }

        public void Promote(int qtyEmployeesToPromote)
        {
            var employeesToPromote = new List<Promotion>();

            for (int employeeIndex = 0; employeeIndex < _globalStorage.EmployeesInTeam.Count; employeeIndex++)
            {
                var employee = _globalStorage.EmployeesInTeam[employeeIndex];

                var score = GetScore(employee);

                if (score == null) continue;

                employeesToPromote.Add(new Promotion { Index = employeeIndex, Score = score.Value });
            }

            PromoteEmployees(qtyEmployeesToPromote, employeesToPromote);
        }

        private void PromoteEmployees(int qtyEmployeesToPromote, List<Promotion> employeesToPromote)
        {
            employeesToPromote = employeesToPromote
                                            .OrderByDescending(e => e.Score).Take(qtyEmployeesToPromote).ToList();

            for (int i = 0; i < employeesToPromote.Count; i++)
            {
                var employeeToPromote = employeesToPromote[i];
                var employee = _globalStorage.EmployeesInTeam[employeeToPromote.Index];

                employee.LastProgressionYear = _globalStorage.CurrentYear;
                employee.Level += 1;

                Console.WriteLine($"{employee.Name} From: {employee.Level - 1} - To: {employee.Level}");
            }

            if (employeesToPromote.Count > 0)
            {
                _globalStorage.CurrentYear += 1;
            }
        }

        private int? GetScore(Employee employee)
        {
            int companyTime = _globalStorage.CurrentYear - employee.AdmissionYear;
            int timeWithoutProgression = _globalStorage.CurrentYear - employee.LastProgressionYear;
            int age = _globalStorage.CurrentYear - employee.BirthYear;
            int score = 0;

            if (employee.Level > 4)
            {
                return null;
            }

            if (companyTime > 0)
            {
                score = companyTime * 2;
            }
            else
            {
                return null;
            }

            if ((employee.Level == 4 && timeWithoutProgression > 1) || employee.Level < 4)
            {
                score += timeWithoutProgression * 3;
            }
            else
            {
                return null;
            }

            score += age / 5;

            return score;
        }
    }
}
