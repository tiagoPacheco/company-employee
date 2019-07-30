using System;
using System.Collections.Generic;
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

        public List<Employee> LoadFileEmployee(string file)
        {
            return ReadFile(file, ReadFileEmployee);
        }

        private Employee ReadFileEmployee(string[] employee)
        {
            return new Employee()
            {
                Name = employee[0],
                Level = int.Parse(employee[1]),
                BirthYear = int.Parse(employee[2]),
                AdmissionYear = int.Parse(employee[3]),
                LastProgressionYear = int.Parse(employee[4])
            };
        }

        public List<Team> LoadFileTeam(string file)
        {
            return ReadFile(file, ReadFileTeam);
        }

        private Team ReadFileTeam(string[] team)
        {
            return new Team()
            {
                Name = team[0],
                MinMaturity = int.Parse(team[1])
            };
        }

        private List<Type> ReadFile<Type>(string fileName, Func<string[], Type> readFileDelegate)
        {
            List<Type> data = new List<Type>();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\" + fileName.Replace("\"", ""));

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string headerLine = sr.ReadLine();
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    data.Add(readFileDelegate(s.Split(',')));
                }
            }

            return data;
        }
    }
}
