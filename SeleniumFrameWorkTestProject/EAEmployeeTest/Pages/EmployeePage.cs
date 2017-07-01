using EAAutoFramework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAEmployeeTest.Pages
{
    public class EmployeePage: BasePage
    {

       // public EmployeePage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.Name, Using = "searchTerm")]
        IWebElement txtSearch { get; set; }

        [FindsBy(How = How.LinkText, Using = "Create New")]
        IWebElement lnkCreateNew { get; set; }

        [FindsBy(How = How.ClassName, Using = "table")]
        IWebElement tblEmployeeList { get; set; }


        public void ClickCreateNew()
        {
            lnkCreateNew.Click();
        }

        public IWebElement GetEmployeeList()
        {
            return tblEmployeeList;
        }

    }
}
