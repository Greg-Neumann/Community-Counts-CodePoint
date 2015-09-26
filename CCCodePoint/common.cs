using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCCodePoint.Models;
using CCCodePoint;

namespace CCCodePoint
{
    class common
    {
        public static void messageLog(Boolean SystemLog, Boolean init, Boolean cr, String msg)
        {
            //
            // Generic logging routine - writing msg to console and, if SystemLog is true, to the log file
            //
            const string logFileName = "CCCodePoint";   // always created in current working directory
            const string logFileExtn = ".txt";
            //
            string logFile = logFileName + logFileExtn;
            string directoryName, fullFileName;
            directoryName=Directory.GetCurrentDirectory();
            fullFileName = directoryName + "\\" + logFile;
            if (init)   // initialise the system log
            {
                if (File.Exists(fullFileName))
                {
                    File.Move(fullFileName, directoryName + "\\" + logFileName + "-"+DateTime.Now.Ticks.ToString() + logFileExtn); // rename previous log to timestamp version
                }
            }
            if (cr)
            {
                Console.WriteLine(msg);
            }
            else
            {
                Console.Write(msg);
            }
            if (SystemLog)
            {
                using (StreamWriter sw =File.AppendText(fullFileName))
                {
                    if (cr)
                    {
                        sw.WriteLine(msg);
                    }
                    else
                    {
                        sw.WriteLine(msg);
                    }
                }
            }
        }
    }
}
