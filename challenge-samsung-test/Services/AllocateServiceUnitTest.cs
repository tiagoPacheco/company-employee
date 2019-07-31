using System;
using System.Collections.Generic;
using System.IO;
using challenge_samsung;
using challenge_samsung.Models;
using challenge_samsung.Services;
using challenge_samsung.Services.Impl;
using challenge_samsung.Utils;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace challenge_samsung_test
{
    [TestClass]
    public class AllocateServiceUnitTest
    {
        IAllocationService _allocationService;
        GlobalStorage _globalStorage;

        private void InitializeServices()
        {
            var globalStorage = new GlobalStorage();
            _allocationService = new AllocationService(globalStorage);
        }

        [TestMethod]
        public void Validate_If_Team_Exists()
        {
            InitializeServices();

            List<Employee> employees = Builder<Employee>.CreateListOfSize(7)
               .All()
               .Build() as List<Employee>;

            var ex = Assert.ThrowsException<BusinessException>(() => _allocationService.Allocate(new List<Team>(), employees));
            Assert.AreEqual(Messages.MSG006, ex.Message);
        }

        [TestMethod]
        public void Validate_If_Exists_Exists()
        {
            InitializeServices();

            List<Team> teams = Builder<Team>.CreateListOfSize(3)
                .All()
                .Build() as List<Team>;

            var ex = Assert.ThrowsException<BusinessException>(() => _allocationService.Allocate(teams, new List<Employee>()));
            Assert.AreEqual(Messages.MSG007, ex.Message);
        }
    }
}
