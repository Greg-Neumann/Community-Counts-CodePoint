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
        
        static Boolean validateDatabase(string dbname)
        {
            //
            // validate the passed database name to the allowed value - see class CommunityCounts for dynamic string
            //
            switch (dbname)
            {
                case "ccbhlc": return true;
                case "cccrow": return true;
                case "ccsydn": return true;
                default:
                    common.messageLog(true,false,true,common.pver+"CodePoint database name >" + dbname + "< invalid");
                    return false;
            }
        }
        static void Main(string[] args)
        {
            Boolean inputValid = true;
            string pcode = null, CPDate = null, dbname = null;
            if (args.Length >= 1)
            {
                var verb = args[0].ToLower();
                common.messageLog(true, true, true,common.pver+ "Initialised at " + DateTime.Now.ToLongTimeString());
                switch (verb)
                {
                    case "load":
                        loadCodePoint.processLoad();       // Request is to load CodePoint
                        break;
                    case "update":
                    case "update-s":
                        if (args.Length<=2)
                        {
                            common.messageLog(true, false, true,common.pver+ "CodePoint requires a minimum of 2 parameters to perform >update<" +
                                "\n" + "e.g. Codepoint update <databasename> CodePointdate (in format CCYT-MM)");
                            inputValid = false;
                        }
                        //
                        // get Code-Point effective date from command line
                        //
                        if (args.Length==3)
                        {
                            dbname = args[1].ToLower();
                            inputValid=validateDatabase(dbname);
                            CPDate = args[2].ToUpper();
                            if ((CPDate.Length!=7) || (CPDate.Substring(4,1)!="-"))
                            {
                                common.messageLog(true, false, true,common.pver+ "Error - " + CPDate + " is an invalid format CodePoint date");
                                inputValid = false;
                            }
                        }
                        //
                        // full start (all postcodes?) or resume processing (stated postcode)?
                        //
                        if (args.Length == 4)
                        {
                            pcode = args[3].ToUpper();
                            if (pcode.Length > 4)
                            {
                                pcode = pcode.Substring(0, pcode.Length - 3) + " " + pcode.Substring(pcode.Length - 3, 3); // make standard format postcode with space
                                CodePoint db = new CodePoint();     // open the database;
                                if (db.cppostcodes.Find(pcode) == null)
                                {
                                    inputValid = false;
                                    common.messageLog(true, false, true, common.pver+"Error - input postcode " + pcode + " not present on CodePoint table");
                                    common.messageLog(true, false, true, common.pver+"Cannot resume update from this postcode");
                                }
                                else
                                {
                                    common.messageLog(true, false, true, common.pver+"Execution will resume update from postcode " + pcode);
                                }
                                dbname = args[1].ToLower();
                                inputValid = validateDatabase(dbname);
                                CPDate = args[2].ToUpper();
                                if ((CPDate.Length != 7) || (CPDate.Substring(4, 1) != "-"))
                                {
                                    common.messageLog(true, false, true, common.pver+"Eerror - " + CPDate + " is an invalid format CodePoint date");
                                    inputValid = false;
                                }
                                db.Dispose();
                            }
                            else
                            {
                                inputValid = false;
                                common.messageLog(true, false, true, common.pver+"Error - input postcode " + pcode + " not valid");
                                common.messageLog(true, false, true, common.pver+"Cannot resume update from this postcode");
                            }
                        }
                        if (inputValid)
                        {
                            Boolean silentLogging = (verb=="update-s");
                            updateCodePoint.processUpdate(CPDate, pcode, dbname, silentLogging);   // update postcodes from pcode with CPDate effective date (format "CCYY-MM"
                        }
                        break;
                    default:
                        common.messageLog(false, false, true,common.pver+ "Invalid input. Type CCCodePoint for help...");
                        break;
                }
               
            }
            else
            {
                common.messageLog(false, false,true, common.pver+"Incorrect Useage. Correct Syntax is CCCodePoint <verb> where <verb> is one of:");
                common.messageLog(false, false,true, common.pver+"   a) Load - to load CodePoint Tables from CSV files held in the same directory as this one.");
                common.messageLog(false, false,true, common.pver+"   b) Update - to update Community Counts PostCode tables with the CodePoint data");
            }
            Console.Read();
        }
        
    }
}
