using EAAutoFramework.Config;
using EAAutoFramework.Helpers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAutoFramework.Base
{
    public abstract class TestInitializeHook : Base
    {
        public readonly BrowsrType Browser;

        public TestInitializeHook(BrowsrType browType)
        {
            Browser = browType;
        }

        public void InitialSettings()
        {
            //set all the settings for framework
            ConfigReader.SetFrameworkSettings();

            //set Log
            LogHelper.CreateLogFile();

            //openBrowser
            OpenBrowser(Browser);

            LogHelper.Write("Initialized framework");
        }

        private void OpenBrowser(BrowsrType bt)
        {
            switch (bt)
            {
                case BrowsrType.InternetExplorer:
                    DriverContext.Driver = new InternetExplorerDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);

                    break;
                case BrowsrType.Firefox:
                    DriverContext.Driver = new FirefoxDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);

                    break;
                case BrowsrType.Chrome:
                    DriverContext.Driver = new ChromeDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);

                    break;
                default:
                    DriverContext.Driver = new ChromeDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);

                    break;
            }
        }

        public virtual void NavigateSite()
        {
            DriverContext.Browser.GoToUrl(Settings.AUT);
        }

    }
}
