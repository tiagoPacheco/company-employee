using System.Collections.Generic;

namespace challenge_samsung.Models
{
    public class Team
    {

        public Team()
        {
            Employees = new List<Employee>();
            CurrentMaturity = 0;
        }

        public string Name { get; set; }

        public int MinMaturity { get; set; }

        public int CurrentMaturity { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
