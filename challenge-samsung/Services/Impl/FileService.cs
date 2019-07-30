using System;
using System.Collections.Generic;
using System.IO;
using challenge_samsung.Models;
using challenge_samsung.Utils;
using System.Linq;

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

        private Employee ReadFileEmployee(List<string> employee)
        {
            return new Employee()
            {
                Name = employee[0],
                Level = int.Parse(employee[1]),
                BirthYear = int.Parse(employee[2]),
                AdmissionYear = int.Parse(employee[3]),
                LastProgressionYear = int.Parse(employee.ElementAtOrDefault(2) != null && employee[4] != "" ? employee[4] : employee[3])
            };
        }

        public List<Team> LoadFileTeam(string file)
        {
            return ReadFile(file, ReadFileTeam);
        }

        private Team ReadFileTeam(List<string> team)
        {
            return new Team()
            {
                Name = team[0],
                MinMaturity = int.Parse(team[1])
            };
        }

        private List<Type> ReadFile<Type>(string fileName, Func<List<string>, Type> readFileDelegate)
        {
            List<Type> data = new List<Type>();

            CheckIfFileNameExist(fileName);

            var path = Directory.GetCurrentDirectory() + "\\" + fileName.Replace("\"", "");
            CheckIfFileExist(fileName, path);

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string headerLine = sr.ReadLine();
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    data.Add(readFileDelegate(s.Split(',').ToList()));
                }
            }

            return data;
        }

        private void CheckIfFileExist(string fileName, string path)
        {
            if (!File.Exists(path))
            {
                throw new BusinessException(String.Format(Messages.MSG004, fileName, path));
            }
        }

        private void CheckIfFileNameExist(string fileName)
        {
            if (fileName == null)
            {
                throw new BusinessException(Messages.MSG003);
            }
        }
    }
}
