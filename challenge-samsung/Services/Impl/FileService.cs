using System;
using System.IO;
using challenge_samsung.Models;

namespace challenge_samsung.Services.Impl
{
    public class FileService : IFileService
    {
        private readonly GlobalStorage _globalStorage;

        public FileService(GlobalStorage globalStorage)
        {
            _globalStorage = globalStorage;
        }

        public void LoadFileEmployee(string file)
        {
            ReadFile(file, ReadFileEmployee);
        }

        private void ReadFileEmployee(string[] employee)
        {
            _globalStorage.EmployeesFromFile.Add(new Employee()
            {
                Name = employee[0],
                Level = int.Parse(employee[1]),
                BirthYear = int.Parse(employee[2]),
                AdmissionYear = int.Parse(employee[3]),
                LastProgressionYear = int.Parse(employee[4])
            });
        }

        public void LoadFileTeam(string file)
        {
            ReadFile(file, ReadFileTeam);
        }

        private void ReadFileTeam(string[] team)
        {
            _globalStorage.Teams.Add(new Team()
            {
                Name = team[0],
                MinMaturity = int.Parse(team[1])
            });
        }

        private void ReadFile(string fileName, Action<string[]> readFileDelegate)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\" + fileName.Replace("\"", ""));

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string headerLine = sr.ReadLine();
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    readFileDelegate(s.Split(','));
                }
            }
        }
    }
}
