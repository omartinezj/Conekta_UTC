using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ConektaUTC.Models
{
    public class Logs
    {
        public void writeLog(string tipo, dynamic data)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/logs/conekta/")  + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine("---------------------------INICIO "+tipo+"--------------------------------------------------");
            sw.WriteLine("---------------------------------------" + data + "--------------------------------------------");
            sw.WriteLine("---------------------------------------" + DateTime.Now.ToString() + "--------------------------------------------");
            sw.WriteLine("------------------------------------------------------------------------------------------------------");
            sw.WriteLine("---------------------------FIN "+tipo+"-----------------------------------------------------");
            sw.Close();
        }
    }
}