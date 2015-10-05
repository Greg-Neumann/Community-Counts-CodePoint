using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCCodePoint.Models;

namespace CCCodePoint
{
    class loadCodePoint
    {
        const string CPCounty = "CPCounty.csv";             // name of Code Point County data file
        const string CPDistrict = "CPDistrict.csv";         // name of Code Point District data file
        const string CPDistrictWard = "CPDistrictWard.csv"; // name of Code Point DistrictWard data file
        const string CPNHSPanSHA = "CPNHSPanSHA.csv";       // name of Code Point NHS Pan Strategic Health Authority data file
        const string CPNHSSHA = "CPNHSSHA.csv";             // name of Code Point NHS Pan Strategic Health Authority data file
        const string CPPostCode = "CPPostCode";             // name of Code Point PostCode data directory
        //
        static List<cpcounty_table> cpcountytable = new List<cpcounty_table>();                     // all reference table mem stored for performance
        static List<cpdistrict_table> cpdistricttable = new List<cpdistrict_table>();               // all reference table mem stored for performance
        static List<cpdistrictward_table> cpdistrictwardtable = new List<cpdistrictward_table>();   // all reference table mem stored for performance
        static List<cpnhspansha_table> cpnhspanshatable = new List<cpnhspansha_table>();            // all reference table mem stored for performance
        static List<cpnhssha_table> cpnhsshatable = new List<cpnhssha_table>();                     // all reference table mem stored for performance
        //
        static Boolean fileNotExists(string fullFileName)
        {
            if (!File.Exists(fullFileName))
            {
                common.messageLog(true,false,true,common.pver+"Codepoint file not found in local directory - " + fullFileName);
                return true;
            }
            return false;
        }
        static Boolean directoryNotExists(string fullFileName)
        {
            if (!Directory.Exists(fullFileName))
            {
                common.messageLog(true,false,true,common.pver+"Codepoint directory not found in local directory - " + fullFileName);
                return true;
            }
            return false;
        }
        static Boolean fileNotLoaded (string directoryName, string CPFileName, Boolean firstForFileType)
        {
            common.messageLog(false,false,true,common.pver+"Processing " + CPFileName);
            var inputRec = File.ReadAllLines(directoryName + CPFileName);
            string[] cols;
            int lineNumber = 1;
           
            CodePoint db = new CodePoint();     // open the database;
            //
            // ensure that database table for the corresponding CodePoint file is empty;
            //
            switch (CPFileName)
            {
                case (CPCounty):
                    db.Database.ExecuteSqlCommand("Delete from cppostcode;");
                    db.Database.ExecuteSqlCommand("Delete from cpcounty;");
                    break;
                case (CPDistrict):
                    db.Database.ExecuteSqlCommand("Delete from cpdistrict;");
                    break;
                case (CPDistrictWard):
                    db.Database.ExecuteSqlCommand("Delete from cpdistrictward;");
                    break;
                case (CPNHSPanSHA):
                    db.Database.ExecuteSqlCommand("Delete from cpnhspansha;");
                    break;
                case (CPNHSSHA):
                    db.Database.ExecuteSqlCommand("Delete from cpnhssha;");
                    break;
                case (CPPostCode):  // unknown files default to being a Postcode as they are several files names within one directory
                default:            
                    if (firstForFileType)
                    {
                        db.Database.ExecuteSqlCommand("Delete from cppostcode;"); // only do this for first postcode prefix type.
                        addCCPostCodes(db);
                    }
                    break;
            }
            db.SaveChanges();
            foreach (var r in inputRec)
            {
                cols = r.Split(',');   // split each line into columns from csv file.
                switch (CPFileName)    // verify input format correct before loading
                {
                    case (CPCounty) :
                    case (CPDistrict) :
                    case (CPDistrictWard):
                    case (CPNHSPanSHA) :
                    case (CPNHSSHA) :
                        if (cols.Length!=2)
                        {
                            common.messageLog(true,false,true,common.pver+"CodePoint input file format error. " + CPFileName + " should have 2 columns separated by a single comma.");
                            common.messageLog(true,false,true,common.pver+"The offending line was at line number " + lineNumber + ". It's content now follows...");
                            common.messageLog(true,false,true,common.pver+r);
                            return true;
                        }
                        break;
                    case (CPPostCode):
                    default:
                        if (cols.Length != 10)
                        {
                            common.messageLog(true,false,true,common.pver+"CodePoint input file format error. " + CPFileName + " should have 10 columns separated by single commas.");
                            common.messageLog(true,false,true,common.pver+"The offending line was at line number " + lineNumber + ". It's content now follows...");
                            common.messageLog(true,false,true,common.pver+r);
                            return true;
                        }
                        for (var i = 0; i < cols.Length;i++ )
                        {
                            cols[i] = cols[i].Trim('"');      // remove any double quotes found from the input data
                        }
                        break;
                }
                switch (CPFileName) // Data record is valid - load it into CodePoint Table.
                {
                    case CPCounty:
                        db.cpcounties.Add(new cpcounty { CPCountyName = cols[0], CPCountyCode = cols[1] });
                        cpcountytable.Add(new cpcounty_table { CPCountyName = cols[0], CPCountyCode = cols[1] });
                        break;
                    case CPDistrict:
                        db.cpdistricts.Add(new cpdistrict { CPDistrictName = cols[0], CPDistrictCode = cols[1] });
                        cpdistricttable.Add(new cpdistrict_table { CPDistrictName = cols[0], CPDistrictCode = cols[1] });
                        break;
                    case CPDistrictWard:
                        db.cpdistrictwards.Add(new cpdistrictward { CPDistrictWardName = cols[0], CPDistrictWardCode = cols[1] });
                        cpdistrictwardtable.Add(new cpdistrictward_table { CPDistrictWardName=cols[0], CPDistrictWardCode=cols[1] });
                        break;
                    case CPNHSPanSHA:
                        db.cpnhspanshas.Add(new cpnhspansha { CPNHSPanSHACode = cols[0], CPNHSPanSHAName = cols[1] });
                        cpnhspanshatable.Add(new cpnhspansha_table { CPNHSPanSHACode = cols[0], CPNHSPanSHAName = cols[1] });
                        break;
                    case CPNHSSHA:
                        db.cpnhsshas.Add(new cpnhssha { CPNHSSHACode = cols[0],  CPNHSSHAName = cols[1] });
                        cpnhsshatable.Add(new cpnhssha_table {CPNHSSHACode = cols[0], CPNHSSHAName=cols[1] });
                        break;
                    case CPPostCode:    // multiple csv input files for Postcode
                    default:
                        if (cols[5]=="") // frig absent CPPostCodeRH 
                        { cols[5]="*";}
                        if (cols[7]=="") // frig absent CPPostCodeCC
                        { cols[7] = "*"; }
                        cols[0] = cols[0].Replace(" ",String.Empty); // remove all spaces out of postcode
                        //
                        // check R.I. links present
                        //
                        if (cpcountytable.Find(a=>a.CPCountyCode==cols[7])==null)
                        {
                            common.messageLog(true,false,true,common.pver+CPCounty+" R.I. link not present for "+ cols[7] + " and PostCode " + cols[0]);
                        }
                        if (cpdistricttable.Find(a=>a.CPDistrictCode==cols[8])==null)
                        {
                            common.messageLog(true,false,true,common.pver+CPDistrict+" R.I. link not present for "+ cols[8]+ " and PostCode " + cols[0]);
                        }
                        if (cpdistrictwardtable.Find(a=>a.CPDistrictWardCode==cols[9])==null)
                        {
                            common.messageLog(true, false, true, common.pver+ CPDistrictWard + " R.I. link not present for " + cols[9] + " and PostCode " + cols[0]);
                        }
                        if (cpnhspanshatable.Find(a=>a.CPNHSPanSHACode==cols[5])==null)
                        {
                            common.messageLog(true, false, true, common.pver + CPNHSPanSHA + " R.I. link not present for " + cols[5] + " and PostCode " + cols[0]);
                        }
                        if (cpnhsshatable.Find(a=>a.CPNHSSHACode==cols[6])==null)
                        {
                            common.messageLog(true, false, true, common.pver + CPNHSSHA + " R.I. link not present for " + cols[6] + " and PostCode " + cols[0]);
                        }
                        // insert space at 4th from right if none present
                        string p = cols[0];
                        p = cols[0].Substring(0, cols[0].Length - 3) + " " + cols[0].Substring(cols[0].Length - 3, 3);
                        db.cppostcodes.Add(new cppostcode
                        {
                            
                            CPPostCode1 = p, 
                             CPPostCodePQ = Convert.ToInt32(cols[1]),
                              CPPostCodeEA = Convert.ToInt32(cols[2]),
                              CPPostCodeNO = Convert.ToInt32(cols[3]),
                              CPPostCodeCY = cols[4],
                              CPPostCodeRH = cols[5],
                              CPPostCodeLH = cols[6],
                              CPPostCodeCC = cols[7],
                              CPPostCodeDC = cols[8],
                              CPPostCodeWC = cols[9]
                        });
                        break;
                }
                lineNumber++;
                if ((lineNumber-1) % 1000==0)       // commit every 1000 records.
                {
                    common.messageLog(false,false,false,"..");
                    db.SaveChanges();
                }
            }
            db.SaveChanges();       //commit all work for this input table.
            common.messageLog(true,false,true,"\nCodePoint has successfully loaded " + (lineNumber-1).ToString() + " lines from " + CPFileName);
            return false; // good exit
        }
        static int addCCPostCodes(CodePoint db)
        {
            //
            // module responsible for the inserting of Community Counts Special Postcodes into the CodePoint tables.
            // 
            // CC1 1CC is for Homeless - no home anywhere at all
            // CC1 2CC is for No Fixed Abode - has a home, but it is not fixed geographically (e.g. Caravan, Barge)
            //
            db.cpcounties.Add(new cpcounty 
            {
                CPCountyCode="UK",
                CPCountyName="All of UK"
            });
            db.cpdistricts.Add(new cpdistrict 
            {
                CPDistrictCode="CC1",
                CPDistrictName="Not Geographical"
            });
            db.cpdistrictwards.Add(new cpdistrictward 
            {
                CPDistrictWardCode = "CC1 1CC",
                CPDistrictWardName = "Homeless"
            });
            db.cpdistrictwards.Add(new cpdistrictward
            {
                CPDistrictWardCode = "CC1 2CC",
                CPDistrictWardName = "No Fixed Abode"
            });
            db.cpnhsshas.Add(new cpnhssha 
            {
                CPNHSSHACode="CC1 1CC",
                CPNHSSHAName="Homeless"
            });
            db.cpnhsshas.Add(new cpnhssha
            {
                CPNHSSHACode = "CC1 2CC",
                CPNHSSHAName = "No Fixed Abode"
            });
            db.cpnhspanshas.Add(new cpnhspansha 
            {
                CPNHSPanSHACode = "CC1",
                CPNHSPanSHAName = "Not Geographical"
            });
            db.SaveChanges();
            db.cppostcodes.Add(new cppostcode 
            {
                CPPostCode1 = "CC1 1CC",
                CPPostCodeCC = "*",
                CPPostCodeCY = "UK",
                CPPostCodeDC = "CC1",
                CPPostCodeWC = "CC1 1CC",
                CPPostCodeRH = "CC1",
                CPPostCodeLH = "CC1 1CC",
                CPPostCodeEA=0,
                CPPostCodeNO=0,
                CPPostCodePQ=0
            });
            db.cppostcodes.Add(new cppostcode
            {
                CPPostCode1 = "CC1 2CC",
                CPPostCodeCC = "*",
                CPPostCodeCY = "UK",
                CPPostCodeDC = "CC1",
                CPPostCodeWC = "CC1 2CC",
                CPPostCodeRH = "CC1",
                CPPostCodeLH = "CC1 2CC",
                CPPostCodeEA = 0,
                CPPostCodeNO = 0,
                CPPostCodePQ = 0
            });
            return 0;
        }
        static int loadInputFiles()
        {
            //
            // check for the presence of the required csv input files. Note the PostCode file is a directory
            //
            
            Boolean invalid = false;
            string fileName;
            string[] cols;
            Boolean firstPostCodePrefix = true;
            string directoryName = Directory.GetCurrentDirectory() + "\\";   // current working directory of the app.
            //
            invalid = invalid || fileNotExists(directoryName + CPCounty);
            invalid = invalid || fileNotExists(directoryName + CPDistrict);
            invalid = invalid || fileNotExists(directoryName + CPDistrictWard);
            invalid = invalid || fileNotExists(directoryName + CPNHSSHA);
            invalid = invalid || fileNotExists(directoryName + CPNHSPanSHA);
            invalid = invalid || directoryNotExists(directoryName + CPPostCode); 
            if (invalid)
            {
                common.messageLog(true,false,true,common.pver+"Please ensure all 5 CodePoint csv files and the CPPostCode directory are present before attempting to load CodePoint tables.");
                return -1;
            }
            
            invalid = invalid || fileNotLoaded(directoryName, CPCounty,true);
            invalid = invalid || fileNotLoaded(directoryName, CPDistrict,true);
            invalid = invalid || fileNotLoaded(directoryName, CPDistrictWard,true);
            invalid = invalid || fileNotLoaded(directoryName, CPNHSSHA,true);
            invalid = invalid || fileNotLoaded(directoryName, CPNHSPanSHA,true);
            //
            // Code Point dataset may have omitted County Codes and/or Pan SHA Codes (NHS) - allow for null values by using single *;
            //
            CodePoint db = new CodePoint();
            db.cpcounties.Add(new cpcounty { CPCountyCode = "*", CPCountyName = "(None)" });
            cpcountytable.Add(new cpcounty_table { CPCountyCode = "*", CPCountyName = "(None)", });
            db.cpnhspanshas.Add(new cpnhspansha { CPNHSPanSHACode = "*", CPNHSPanSHAName = "(None)" });
            cpnhspanshatable.Add(new cpnhspansha_table { CPNHSPanSHACode = "*", CPNHSPanSHAName = "(None)" });
            db.SaveChanges();
            db.Dispose();
            //
            // Postcodes are multiple csv files in sub-directory of the working directory.
            //  
            foreach (var p in Directory.GetFiles(directoryName+CPPostCode))
            {
                cols = p.Split('\\');
                fileName = cols[cols.Length - 2] + "\\" + cols[cols.Length - 1];
                if (!cols[cols.Length-1].StartsWith("."))  // ignore hidden files (starting with a '.')
                {
                    invalid = invalid || fileNotLoaded(directoryName, fileName, firstPostCodePrefix);
                    firstPostCodePrefix = false;
                    }
            }
            common.messageLog(true, false, true,common.pver+ "CodePoint has loaded all input data at " + DateTime.Now.ToLongTimeString());
            if (invalid)
            {return -1;}    // issues during run   
            else 
            {return 0;}     // successful run
        }   
        internal static void processLoad()
        {
            //
            // Control module to load all Code Point data tables.
            //
            if (loadInputFiles() >= 0)
            {
                // data loaded okay...
            }
        }
    
    }
    
}
