using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace UpDown.Models
{
    public class Logger
    {
        public static void LogPayments(string txt)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Logs/";
            
            string directoryPath = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;

            if (!Directory.Exists(path+directoryPath))
            {
                Directory.CreateDirectory(path + directoryPath);
            }

            path = path + directoryPath + "/LogPayments.txt";
            StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII);
            sw.WriteLine();
            sw.WriteLine("****************************************************************************************************************");
            string line = "*###" + DateTime.Now + " " + txt;
            sw.WriteLine(line);
            sw.WriteLine(txt);
            sw.WriteLine("****************************************************************************************************************");
            sw.WriteLine();
            sw.Close();
        }


        public static void ErrorLog(string funcName, string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Content/logs/LogPayments.txt";
            StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII);
            string line = "###" + funcName + " date= " + DateTime.Now;
            sw.WriteLine("****************************************************************************************************************");
            sw.WriteLine(line);
            line = message;
            sw.WriteLine();
            sw.WriteLine(line);
            sw.WriteLine("****************************************************************************************************************");
            sw.WriteLine();
            sw.Close();
        }
    }



}