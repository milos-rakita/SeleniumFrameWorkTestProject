using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using EAEmployeeTest.Pages;
using EAAutoFramework.Base;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using EAAutoFramework.Helpers;
using EAAutoFramework.Config;

namespace EAEmployeeTest
{
    [TestClass]
    public class UnitTest1 :Base
    {

        //string url = ConfigReader.InitializeTest();
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
        
        [TestMethod]
        public void TestMethod1()
        {
            //DriverContext.Driver = new FirefoxDriver();
            //DriverContext.Driver.Navigate().GoToUrl(url);
            ConfigReader.SetFrameworkSettings();

            LogHelper.CreateLogFile();

            string fileName = Environment.CurrentDirectory.ToString() + "\\Data\\Login.xlsx";
            LogHelper.Write("Opened the Data file for Login!!!");
            ExcelHelper.PopulateInCollection(fileName);
            
            OpenBrowser(BrowsrType.Chrome);
            DriverContext.Browser.GoToUrl(Settings.AUT);


            CurrentPage = GetInstance<LoginPage>();
            CurrentPage.As<LoginPage>().ClickLoginLink();
            CurrentPage.As<LoginPage>().CheckIfLoginExist();
            CurrentPage.As<LoginPage>().Login(ExcelHelper.ReadData(1,"UserName"), ExcelHelper.ReadData(1, ""));

            CurrentPage = CurrentPage.As<LoginPage>().ClickEmployeeList();
            CurrentPage.As<EmployeePage>().ClickCreateNew();
        }
        [TestMethod]
        public void TableOperation()
        {
            LogHelper.CreateLogFile();

            string fileName = Environment.CurrentDirectory.ToString() + "\\Data\\Login.xlsx";
            LogHelper.Write("Opened the Data file for Login!!!");
            ExcelHelper.PopulateInCollection(fileName);

            OpenBrowser(BrowsrType.Chrome);
            DriverContext.Browser.GoToUrl(Settings.AUT);

            CurrentPage = GetInstance<LoginPage>();
            CurrentPage.As<LoginPage>().ClickLoginLink();
            CurrentPage.As<LoginPage>().Login(ExcelHelper.ReadData(1, "UserName"), ExcelHelper.ReadData(1, ""));

            CurrentPage = CurrentPage.As<LoginPage>().ClickEmployeeList();
            var table = CurrentPage.As<EmployeePage>().GetEmployeeList();
            HtmlTableHelper.ReadTable(table);
            HtmlTableHelper.PerformActionOnCell("5","Name","Ramesh","Edit");

        }

        public void Login()
        {
            LoginPage li = new LoginPage();
            li.ClickLoginLink();
            li.Login("admin", "password");
            
        }
    }
}
