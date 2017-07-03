using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace EAAutoFramework.Config
{
    public class ConfigReader
    {

        //public static string InitializeTest()
        //{
        //     return ConfigurationManager.AppSettings["AUT"].ToString();
        //}
        public static void SetFrameworkSettings()
        {
            XPathItem aut;
            XPathItem testType;
            XPathItem buildName;
            XPathItem isLog;
            XPathItem isReport;
            XPathItem logPath;

            string strFileNAme = Environment.CurrentDirectory.ToString() + "\\Config\\GlobalConfig.xml";
            FileStream stream = new FileStream(strFileNAme, FileMode.Open);
            XPathDocument document = new XPathDocument(stream);
            XPathNavigator navigator = document.CreateNavigator();

            aut = navigator.SelectSingleNode("EAAutoFramework/RunSettings/AUT");
            buildName = navigator.SelectSingleNode("EAAutoFramework/RunSettings/BuildName");
            testType = navigator.SelectSingleNode("EAAutoFramework/RunSettings/TestType");
            isLog = navigator.SelectSingleNode("EAAutoFramework/RunSettings/IsLog");
            isReport = navigator.SelectSingleNode("EAAutoFramework/RunSettings/IsReport");
            logPath = navigator.SelectSingleNode("EAAutoFramework/RunSettings/LogPath");
            
            Settings.AUT = aut.ToString();
            Settings.TestType = testType.ToString();
            Settings.BuildName = buildName.ToString();
            Settings.IsLog = isLog.ToString();
            Settings.IsReporting = isReport.ToString();
            Settings.LogPath = logPath.ToString();


        }



    }
}
