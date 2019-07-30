using System;
using System.Collections.Generic;
using System.IO;
using challenge_samsung;
using challenge_samsung.Models;
using challenge_samsung.Services;
using challenge_samsung.Services.Impl;
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
    }
}
