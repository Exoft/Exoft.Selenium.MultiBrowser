using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Exoft.Selenium.MultiBrowser.Core
{
    public class DriverRunner: IDisposable
    {
        private readonly DriverStore _browserStore;

        public DriverRunner(IEnumerable<IWebDriver> drivers)
        {
            _browserStore = new DriverStore(drivers);
        }

        public DriverRunner(Func<IEnumerable<IWebDriver>> initFunc) : this(initFunc())
        {
        }

        public void AttachBrowserAction(Func<IWebDriver, IWebDriver> action)
        {
            _browserStore.AttachAction(action);
        }

        public async Task Execute()
        {
            var tasks = new List<Task>();

            foreach (var webDriver in _browserStore.Drivers)
            {
                tasks.Add(Task.Factory.StartNew(() => _browserStore.DriverAction(webDriver)));
            }

            await Task.WhenAll(tasks);
        }

        public void Dispose()
        {
            _browserStore.QuitAllDrivers();
        }
    }
}
