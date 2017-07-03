using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EAAutoFramework.Base;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using EAAutoFramework.Config;
using EAAutoFramework.Helpers;
using EAEmployeeTest.Pages;

namespace EAEmployeeTest
{
    [TestClass]
    public class UnitTest2 : HookInitialize
    {

        [TestMethod]
        public void TestMethod1()
        {
            
            string fileName = Environment.CurrentDirectory.ToString() + "\\Data\\Login.xlsx";
            ExcelHelper.PopulateInCollection(fileName);
            
            CurrentPage = GetInstance<LoginPage>();
            CurrentPage.As<LoginPage>().ClickLoginLink();
            CurrentPage.As<LoginPage>().CheckIfLoginExist();
            CurrentPage.As<LoginPage>().Login(ExcelHelper.ReadData(1, "UserName"), ExcelHelper.ReadData(1, ""));

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
            
            CurrentPage = GetInstance<LoginPage>();
            CurrentPage.As<LoginPage>().ClickLoginLink();
            CurrentPage.As<LoginPage>().Login(ExcelHelper.ReadData(1, "UserName"), ExcelHelper.ReadData(1, ""));

            CurrentPage = CurrentPage.As<LoginPage>().ClickEmployeeList();
            var table = CurrentPage.As<EmployeePage>().GetEmployeeList();
            HtmlTableHelper.ReadTable(table);
            HtmlTableHelper.PerformActionOnCell("5", "Name", "Ramesh", "Edit");

        }

        public void Login()
        {
            LoginPage li = new LoginPage();
            li.ClickLoginLink();
            li.Login("admin", "password");

        }
    }
}
