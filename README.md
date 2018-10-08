# Exoft.Selenium.MultiBrowser

Wrapper for [Selenium](https://www.seleniumhq.org/) ``` IWebDriver ``` to execute actions on different browsers simultaneously.

### Installation

1. Clone and build solution;
2. In ```Exoft.Selenium.MultiBrowser.Core\bin\Debug```, nuget package will be generated (```Exoft.Selenium.MultiBrowser.Core.*.*.*.nupkg```);
3. Include nuget package (```Exoft.Selenium.MultiBrowser.Core.*.*.*.nupkg```) into your test project.
 
### How to use

In your unit test file, initialize ``` DriverRunner ``` instance with list of ``` IWebDriver ``` instances (Chrome, Firefox, Edge, etc.).

```
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
```

Then, use ``` AttachBrowserAction ``` to attach action, that will be executed on each driver. 
Wrap your code with ``` using ``` block, so, in case of failure, drivers will be properly closed and disposed.

```
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
```

After all driver actions attached, run ``` Execute ``` method on ``` DriverRunner ``` instance to start action execution on initialized drivers.
