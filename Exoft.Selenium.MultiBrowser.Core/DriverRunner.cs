using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Exoft.Selenium.MultiBrowser.Core
{
    public class DriverRunner
    {
        private readonly DriverStore _browserStore;

        public DriverRunner(ScenarioType scenarioType)
        {
            switch (scenarioType)
            {
                case ScenarioType.All:
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

                        _browserStore = new DriverStore(drivers);
                        break;
                    }
            }
        }

        public void AttachBrowserAction(Func<IWebDriver, IWebDriver> action)
        {
            _browserStore.AttachAction(action);
        }

        public async Task ExecuteAndClose()
        {
            var tasks = new List<Task>();

            _browserStore.Drivers.ForEach(driver => tasks.Add(Task.Factory.StartNew(() => _browserStore.DriverAction(driver))));

            await Task.WhenAll(tasks);
        }
    }
}
