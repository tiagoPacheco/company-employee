using challenge_samsung.Models;
using System;
using System.Collections.Generic;

namespace challenge_samsung
{
    public class GlobalStorage
    {
        private static readonly Lazy<GlobalStorage> instance = new Lazy<GlobalStorage>(() => new GlobalStorage());

        public static GlobalStorage Instance { get { return instance.Value; } }
        
        private GlobalStorage()
        {
            Teams = new List<Team>();
            EmployeesFromFile = new List<Employee>();
            CurrentYear = DateTime.Now.Year;
        }

        public int CurrentYear { get; set; }

        public List<Team> Teams { get; set; }

        public List<Employee> EmployeesFromFile { get; set; }

        private List<Employee> _employeesInTeam;
        public List<Employee> EmployeesInTeam
        {
            get
            {
                var list = new List<Employee>();

                Teams.ForEach(t => list.AddRange(t.Employees));

                return list;
            }
            set
            {
                _employeesInTeam = value;
            }
        }
    }
}
