using System.Collections.Generic;
using System.Threading.Tasks;
using Exoft.Selenium.MultiBrowser.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Exoft.Selenium.MultiBrowser.Tests
{
    [TestClass]
    public class UnitTestExample
    {
        private readonly DriverRunner _driverRunner = new DriverRunner(() =>
        {
            var chromeDriver = new ChromeDriver(
                @"D:\Projects\Exoft.Selenium.MultiBrowser\Exoft.Selenium.MultiBrowser.Core\drivers\");

            var firefoxDriver = new FirefoxDriver(
                @"D:\Projects\Exoft.Selenium.MultiBrowser\Exoft.Selenium.MultiBrowser.Core\drivers\");

            var drivers = new List<IWebDriver>()
            {
                chromeDriver,
                firefoxDriver
            };

            return drivers;
        });

        [TestMethod]
        public async Task TestMethodExample()
        {
            using (_driverRunner)
            {
                _driverRunner.AttachBrowserAction((driver) =>
                {
                    driver.Navigate().GoToUrl("https://www.google.com/");
                    return driver;
                });

                _driverRunner.AttachBrowserAction((driver) =>
                {
                    Assert.AreEqual(driver.Url, "https://www.google.com/");
                    driver.Quit();
                    return driver;
                });

                await _driverRunner.Execute();
            }
        }
    }
}
