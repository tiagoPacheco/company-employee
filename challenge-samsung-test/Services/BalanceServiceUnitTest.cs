using System;
using System.Collections.Generic;
using System.IO;
using challenge_samsung;
using challenge_samsung.Models;
using challenge_samsung.Services;
using challenge_samsung.Services.Impl;
using challenge_samsung.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FizzWare.NBuilder;
using Moq;

namespace challenge_samsung_test
{
    [TestClass]
    public class BalanceServiceUnitTest
    {        
        IBalanceService _balanceService;
        GlobalStorage _globalStorage;

        private void InitializeServices()
        {
            var globalStorage = GlobalStorage.Instance;
            _balanceService = new BalanceService(globalStorage);
        }

        [TestMethod]
        public void Validate_If_Team_Exists()
        {
            InitializeServices();

            var ex = Assert.ThrowsException<BusinessException>(() => _balanceService.BalanceTeams(new List<Team>()));
            Assert.AreEqual(Messages.MSG006, ex.Message);
        }

        [TestMethod]
        public void Validate_If_Exists_Exists()
        {
            InitializeServices();

            List<Team> teams = Builder<Team>.CreateListOfSize(3)
                .All()
                .Build() as List<Team>;

            var ex = Assert.ThrowsException<BusinessException>(() => _balanceService.BalanceTeams(teams));
            Assert.AreEqual(Messages.MSG007, ex.Message);
        }
    }
}
