using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CCCodePoint.Models;
using CCCodePoint;

namespace CCCodePoint
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var verb = args[0].ToLower();
                common.messageLog(true, true, true, "CCCodePoint has initialised at " + DateTime.Now.ToLongTimeString());
                switch (verb)
                {
                    case "load":
                        loadCodePoint.processLoad();       // Request is to load CodePoint
                        break;
                    case "update":
                        updateCodePoint.processUpdate();    // Request is to Update Postcodes from CodePoint
                        break;
                    default:
                        common.messageLog(false, false, true, "Invalid input. Type CCCodePoint for help...");
                        break;
                }
               
            }
            else
            {
                common.messageLog(false, false,true, "Incorrect Useage. Correct Syntax is CCCodePoint <verb> where <verb> is one of:");
                common.messageLog(false, false,true, "   a) Load - to load CodePoint Tables from CSV files held in the same directory as this one.");
                common.messageLog(false, false,true, "   b) Update - to update Community Counts PostCode tables with the CodePoint data");
            }
            Console.Read();
        }
        
    }
}
