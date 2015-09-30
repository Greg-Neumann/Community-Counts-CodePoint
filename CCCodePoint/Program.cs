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
            Boolean inputValid = true;
            string pcode = null, CPDate=null;
            if (args.Length >= 1)
            {
                var verb = args[0].ToLower();
                common.messageLog(true, true, true, "CCCodePoint has initialised at " + DateTime.Now.ToLongTimeString());
                switch (verb)
                {
                    case "load":
                        loadCodePoint.processLoad();       // Request is to load CodePoint
                        break;
                    case "update":
                        //
                        // get Code-Point effective date from command line
                        //
                        if (args.Length==2)
                        {
                            CPDate = args[1].ToUpper();
                            if ((CPDate.Length!=7) || (CPDate.Substring(4,1)!="-"))
                            {
                                common.messageLog(true, false, true, "CodePoint error - " + CPDate + " is an invalid format CodePoint date");
                                inputValid = false;
                            }
                        }
                        //
                        // full start (all postcodes?) or resume processing (stated postcode)?
                        //
                        if (args.Length == 3)
                        {
                            pcode = args[2].ToUpper();
                            if (pcode.Length > 4)
                            {
                                pcode = pcode.Substring(0, pcode.Length - 3) + " " + pcode.Substring(pcode.Length - 3, 3); // make standard format postcode with space
                                CodePoint db = new CodePoint();     // open the database;
                                if (db.cppostcodes.Find(pcode) == null)
                                {
                                    inputValid = false;
                                    common.messageLog(true, false, true, "CodePoint error - input postcode " + pcode + " not present on CodePoint table");
                                    common.messageLog(true, false, true, "CodePoint cannot resume update from this postcode");
                                }
                                else
                                {
                                    common.messageLog(true, false, true, "CodePoint will resume update from postcode " + pcode);
                                }
                                db.Dispose();
                            }
                            else
                            {
                                inputValid = false;
                                common.messageLog(true, false, true, "CodePoint error - input postcode " + pcode + " not valid");
                                common.messageLog(true, false, true, "CodePoint cannot resume update from this postcode");
                            }
                        }
                        if (inputValid)
                        {
                            updateCodePoint.processUpdate(CPDate, pcode);   // update postcodes from pcode with CPDate effective date (format "CCYY-MM"
                        }
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
