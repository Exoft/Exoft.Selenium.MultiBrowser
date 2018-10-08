using System.Threading.Tasks;
using Exoft.Selenium.MultiBrowser.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exoft.Selenium.MultiBrowser.Tests
{
    [TestClass]
    public class UnitTestExample
    {
        private readonly DriverRunner _driverRunner = new DriverRunner(ScenarioType.All);

        [TestMethod]
        public async Task TestMethodExample()
        {
            _driverRunner.AttachBrowserAction((driver) =>
            {
                driver.Navigate().GoToUrl("https://google.com");
                return driver;
            });

            _driverRunner.AttachBrowserAction((driver) =>
            {
                driver.Quit();
                return driver;
            });

            await _driverRunner.ExecuteAndClose();
        }
    }
}
