using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAutoFramework.Helpers
{
    public class LogHelper
    {
        //log file
        private static string _logFileName = string.Format("{0:yyyymmddhhmmss}",DateTime.Now);
        private static StreamWriter streamW = null;

        //create a file wich can store the log information
        public static void CreateLogFile()
        {
            string dir = @"E:\SeleniumProjekti\SeleniumFrameWorkTestProject\SeleniumFrameWorkTestProject\EAAutoFramework";
            if (Directory.Exists(dir))
            {
                streamW = File.AppendText(dir + _logFileName + ".log");
            }
            else
            {
                Directory.CreateDirectory(dir);
                streamW = File.AppendText(dir + _logFileName + ".log");
            }
        }

        //create a method wich can write the text in the log file
        public static void Write(string logMessage)
        {
            streamW.Write("{0}{1}",DateTime.Now.ToLongDateString(),DateTime.Now.ToLongDateString());
            streamW.WriteLine("         {0}", logMessage);
            streamW.Flush();







    }
}
