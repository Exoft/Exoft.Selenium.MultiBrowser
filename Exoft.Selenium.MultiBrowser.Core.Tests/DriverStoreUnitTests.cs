using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Exoft.Selenium.MultiBrowser.Core.Tests
{
    [TestClass]
    public class DriverStoreUnitTests
    {
        [TestMethod]
        public void DriverStore_ShouldAttachFunction_OnAttachActionCalled_WithCorrectParameters()
        {
            // arrange
            var drivers = new List<IWebDriver>();
            var driverStore = new DriverStore(drivers);
            Func<IWebDriver, IWebDriver> action = driver => driver;

            // act
            driverStore.AttachAction(action);

            // assert
            Assert.AreEqual(driverStore.DriverAction.Method, action.Method);
        }
    }
}
