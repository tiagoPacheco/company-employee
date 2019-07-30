using System;
using System.Collections.Generic;
using System.IO;
using challenge_samsung;
using challenge_samsung.Models;
using challenge_samsung.Services;
using challenge_samsung.Services.Impl;
using challenge_samsung.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace challenge_samsung_test
{
    [TestClass]
    public class FileServiceUnitTest
    {
        IFileService _fileService;
        GlobalStorage _globalStorage;

        private void InitializeServices()
        {
            var globalStorage = new GlobalStorage();
            _fileService = new FileService(globalStorage);
        }

        [TestMethod]
        public void No_File_Team_Test()
        {
            InitializeServices();

            var ex = Assert.ThrowsException<BusinessException>(() => _fileService.LoadFileTeam(null));
            Assert.AreEqual(Messages.MSG003, ex.Message);

        }

        [TestMethod]
        public void No_File_Employee_Test()
        {
            InitializeServices();

            var ex = Assert.ThrowsException<BusinessException>(() => _fileService.LoadFileEmployee(null));
            Assert.AreEqual(Messages.MSG003, ex.Message);
        }

        [TestMethod]
        public void File_Not_Exist_Team_Test()
        {
            InitializeServices();
            var fileName = "abc";
            var ex = Assert.ThrowsException<BusinessException>(() => _fileService.LoadFileTeam(fileName));
            Assert.AreEqual(string.Format(Messages.MSG004, fileName, Directory.GetCurrentDirectory() + "\\" + fileName.Replace("\"", "")), ex.Message);
        }

        [TestMethod]
        public void File_Not_Exist_Employee_Test()
        {
            InitializeServices();
            var fileName = "abc";
            var ex = Assert.ThrowsException<BusinessException>(() => _fileService.LoadFileEmployee(fileName));
            Assert.AreEqual(string.Format(Messages.MSG004, fileName, Directory.GetCurrentDirectory() + "\\" + fileName.Replace("\"", "")), ex.Message);
        }
    }
}
