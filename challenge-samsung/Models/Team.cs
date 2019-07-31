using System.Collections.Generic;
using System.Linq;

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

        public List<Employee> Employees { get; set; }

        private int _currenteMaturity;
        public int CurrentMaturity
        {
            get
            {
                return this.Employees.Sum(e => e.Level);
            }
            set
            {
                _currenteMaturity = value;
            }
        }

        private int _missingMaturity;
        public int MissingMaturity
        {
            get
            {
                var missingMaturity = MinMaturity - CurrentMaturity;

                return missingMaturity;
            }
            set
            {
                _missingMaturity = value;
            }
        }

        private int _extraMaturity;
        public int ExtraMaturity
        {
            get
            {
                return CurrentMaturity - MinMaturity;
            }
            set
            {
                _extraMaturity = value;
            }
        }
    }
}
