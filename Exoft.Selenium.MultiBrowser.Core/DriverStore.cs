using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Exoft.Selenium.MultiBrowser.Core
{
    public class DriverStore: IDisposable
    {
        public List<IWebDriver> Drivers { get; }
        public Func<IWebDriver, IWebDriver> DriverAction { get; private set; } = driver => driver;

        public DriverStore(List<IWebDriver> drivers)
        {
            Drivers = drivers;
        }

        public void AttachAction(Func<IWebDriver, IWebDriver> action)
        {
            DriverAction += action;
        }

        public void Dispose()
        {
            Drivers.ForEach(driver =>
            {
                driver.Quit();
            });
        }
    }
}
