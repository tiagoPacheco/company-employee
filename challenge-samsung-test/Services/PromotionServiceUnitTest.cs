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
    public class PromotionServiceUnitTest
    {
        IPromotionService _promotionService;
        GlobalStorage _globalStorage;

        private void InitializeServices()
        {
            var globalStorage = new GlobalStorage();
            _promotionService = new PromotionService(globalStorage);
        }

        [TestMethod]
        public void Validate_Qty_Employees_To_Promote()
        {
            InitializeServices();

            var ex = Assert.ThrowsException<BusinessException>(() => _promotionService.Promote("abc"));
            Assert.AreEqual(Messages.MSG008, ex.Message);
        }

        [TestMethod]
        public void Validate_If_Employees_Exists()
        {
            InitializeServices();

            var ex = Assert.ThrowsException<BusinessException>(() => _promotionService.Promote("2"));
            Assert.AreEqual(Messages.MSG007, ex.Message);
        }
    }
}
