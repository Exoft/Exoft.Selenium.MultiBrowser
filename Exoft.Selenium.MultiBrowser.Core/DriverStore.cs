using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Exoft.Selenium.MultiBrowser.Core
{
    public class DriverStore
    {
        public IEnumerable<IWebDriver> Drivers { get; }
        public Func<IWebDriver, IWebDriver> DriverAction { get; private set; } = driver => driver;

        public DriverStore(IEnumerable<IWebDriver> drivers)
        {
            Drivers = drivers;
        }

        public void AttachAction(Func<IWebDriver, IWebDriver> action)
        {
            DriverAction += action;
        }

        public void QuitAllDrivers()
        {
            foreach (var webDriver in Drivers)
            {
                webDriver.Quit();
            }
        }
    }
}
