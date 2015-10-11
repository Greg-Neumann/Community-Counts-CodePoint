using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCCodePoint.Models;
using System.Data.Entity;

namespace CCCodePoint
{
    class updateCodePoint
    {
        public static CodePointData processCounty(cppostcode cprec, int idCodePointDate, CodePoint db, CommunityCounts db2)
        {
            //
            // ensures that County Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the County, and it's name, on the CC tables.
            //
            string cname;
            int idCountyCode;
            var countyRec = db2.counties.Where(a => a.CountyCode == cprec.CPPostCodeCC);
            if (!countyRec.Any())
            {
                // need to add county code to CC database from CodePoint data (cp*)
                var countyData = db.cpcounties.Find(cprec.CPPostCodeCC);
                db2.counties.Add(new county
                {
                    CountyCode = cprec.CPPostCodeCC,
                    CountyName = countyData.CPCountyName,
                    idCPDate = idCodePointDate
                });
                db2.SaveChanges();
                idCountyCode = db2.counties.Where(a => a.CountyName == countyData.CPCountyName).First().idCountyListCode;
                cname = countyData.CPCountyName;
            }
            else    // county code does exist on CC County table
            {
                string cpCountyName = db.cpcounties.Find(cprec.CPPostCodeCC).CPCountyName;   // get county name - it may have changed
                var county = countyRec.First();
                idCountyCode = county.idCountyListCode;
                var countyNameChanged = (cpCountyName != county.CountyName);                 // flag if county name changed for same id
                if (countyNameChanged)
                {
                    county.CountyName = cpCountyName;
                    county.idCPDate = idCodePointDate;
                    db2.Entry(county).State = EntityState.Modified;                  // update county name change
                }
                cname = cpCountyName;
            }

            return new CodePointData { id = idCountyCode, name = cname, code=cprec.CPPostCodeCC };
        }
        public static CodePointData processDistrict(cppostcode cprec, int idCodePointDate, CodePoint db, CommunityCounts db2)
        {
            //
            // ensures that District Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the District, and it's name, on the CC tables.
            //
            string dname;
            int idDistrictCode;
            var districtRec = db2.districts.Where(a => a.DistrictCode == cprec.CPPostCodeDC);    
            if (!districtRec.Any())
            {
                // need to add district code to CC database from CodePoint data (cp*)
                var districtData=db.cpdistricts.Find(cprec.CPPostCodeDC);
                db2.districts.Add(new district { 
                    DistrictCode=cprec.CPPostCodeDC,
                    Description=districtData.CPDistrictName,
                    idCPDate=idCodePointDate
                });
                db2.SaveChanges();
                idDistrictCode=db2.districts.Where(a=>a.Description==districtData.CPDistrictName).First().idDistrictCode;
                dname = districtData.CPDistrictName;
            }
            else    // district code does exist on CC district table
            {
                string cpDistrictName = db.cpdistricts.Find(cprec.CPPostCodeDC).CPDistrictName;   // get district name - it may have changed
                var district = districtRec.First();
                idDistrictCode = district.idDistrictCode;
                var districtNameChanged = (cpDistrictName != district.Description);                 // flag if district name changed for same id
                if(districtNameChanged)
                {
                    district.Description = cpDistrictName;
                    district.idCPDate = idCodePointDate;
                    db2.Entry(district).State = EntityState.Modified;                  // update district name change
                }
                dname = cpDistrictName;
            }

            return new CodePointData {id=idDistrictCode, name=dname, code=cprec.CPPostCodeDC };
        }
        public static CodePointData processWard(cppostcode cprec, int idCodePointDate, CodePoint db, CommunityCounts db2)
        {
            //
            // ensures that Ward Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the Ward code, and it's name, on the CC tables.
            //
            string wname;
            int idWardCode;
            var wardRec = db2.wards.Where(a => a.WardCode == cprec.CPPostCodeWC);
            if (!wardRec.Any())
            {
                // need to add ward code to CC database from CodePoint data (cp*)
                var wardData = db.cpdistrictwards.Find(cprec.CPPostCodeWC);
                db2.wards.Add(new ward
                {
                    WardCode = cprec.CPPostCodeWC,
                    Description = wardData.CPDistrictWardName,
                    idCPDate = idCodePointDate
                });
                db2.SaveChanges();
                idWardCode = db2.wards.Where(a => a.Description == wardData.CPDistrictWardName).First().idWardCode;
                wname = wardData.CPDistrictWardName;
            }
            else    // ward code does exist on CC district table
            {
                string cpWardName = db.cpdistrictwards.Find(cprec.CPPostCodeWC).CPDistrictWardName ;   // get ward name - it may have changed
                var ward = wardRec.First();
                idWardCode = ward.idWardCode;
                var wardNameChanged = (cpWardName != ward.Description);                 // flag if ward name changed for same id
                if (wardNameChanged)
                {
                    ward.Description = cpWardName;
                    ward.idCPDate = idCodePointDate;
                    db2.Entry(ward).State = EntityState.Modified;                  // update ward name change
                }
                wname = cpWardName;
            }

            return new CodePointData { id = idWardCode, name = wname, code=cprec.CPPostCodeWC };
        }
        public static CodePointData processSHA(cppostcode cprec, int idCodePointDate, CodePoint db, CommunityCounts db2)
        {
            //
            // ensures that SHA Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the Ward code, and it's name, on the CC tables.
            //
            string sname;
            int idSHACode;
            var nhsshaREC = db2.nhsshas.Where(a => a.NHSSHACode == cprec.CPPostCodeLH);
            if (!nhsshaREC.Any())
            {
                // need to add SHA code to CC database from CodePoint data (cp*)
                var shaData = db.cpnhsshas.Find(cprec.CPPostCodeLH);
                db2.nhsshas.Add(new nhssha
                {
                    NHSSHACode = cprec.CPPostCodeLH,
                    NHSSHAName = shaData.CPNHSSHAName,
                    idCPDate = idCodePointDate
                });
                db2.SaveChanges();
                idSHACode = db2.nhsshas.Where(a => a.NHSSHAName == shaData.CPNHSSHAName).First().idNHSSHACode;
                sname = shaData.CPNHSSHAName;
            }
            else    // SHA code does exist on CC district table
            {
                string cpshaName = db.cpnhsshas.Find(cprec.CPPostCodeLH).CPNHSSHAName;   // get SHA name - it may have changed
                var nhssha = nhsshaREC.First();
                idSHACode = nhssha.idNHSSHACode;
                var shaNameChanged = (cpshaName != nhssha.NHSSHAName);                 // flag if SHA name changed for same id
                if (shaNameChanged)
                {
                    nhssha.NHSSHAName = cpshaName;
                    nhssha.idCPDate = idCodePointDate;
                    db2.Entry(nhssha).State = EntityState.Modified;                  // update sha name change
                }
                sname = cpshaName;
            }

            return new CodePointData { id = idSHACode, name = sname, code=cprec.CPPostCodeLH };
        }
        public static CodePointData processPANSHA(cppostcode cprec, int idCodePointDate, CodePoint db, CommunityCounts db2)
        {
            //
            // ensures that PanSHA Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the Ward code, and it's name, on the CC tables.
            //
            string pname;
            int idPANSHACode;
            var pannhsREC = db2.nhspanshas.Where(a => a.NHSPanSHACode == cprec.CPPostCodeRH);
            if (!pannhsREC.Any())
            {
                // need to add PANSHA code to CC database from CodePoint data (cp*)
                var panshaData = db.cpnhspanshas.Find(cprec.CPPostCodeRH);
                db2.nhspanshas.Add(new nhspansha
                {
                    NHSPanSHACode = cprec.CPPostCodeRH,
                    NHSPanSHAName = panshaData.CPNHSPanSHAName,
                    idCPDate = idCodePointDate
                });
                db2.SaveChanges();
                idPANSHACode = db2.nhspanshas.Where(a => a.NHSPanSHAName == panshaData.CPNHSPanSHAName).First().idNHSPanSHACode;
                pname = panshaData.CPNHSPanSHAName;
            }
            else    // PANSHA code does exist on CC district table
            {
                string cppanshaName = db.cpnhspanshas.Find(cprec.CPPostCodeRH).CPNHSPanSHAName;   // get PANSHA name - it may have changed
                var pannhssha = pannhsREC.First();
                idPANSHACode = pannhssha.idNHSPanSHACode;
                var panshaNameChanged = (cppanshaName != pannhssha.NHSPanSHAName);                 // flag if PANSHA name changed for same id
                if (panshaNameChanged)
                {
                    pannhssha.NHSPanSHAName = cppanshaName;
                    pannhssha.idCPDate = idCodePointDate;
                    db2.Entry(pannhssha).State = EntityState.Modified;                  // update sha name change
                }
                pname = cppanshaName;
            }

            return new CodePointData { id = idPANSHACode, name = pname, code=cprec.CPPostCodeRH};

        }
        public static void processUpdate(string CPDate, string pcode, string dbname, Boolean silentLogging) // will be null if all postcodes to be updated
        {
            common.messageLog(true, false, true, common.pver+"Update processing will mark all data with CodePoint Date " + CPDate);
            if (pcode!=null)
            {
                common.messageLog(true, false, true,common.pver+ "CodePoint will resume processing from (including) postcode " + pcode);
            }
            else
            {
                pcode = "";
            }
            CodePoint db = new CodePoint();              // open the CodePoint database; 
            CommunityCounts db2 = new CommunityCounts(dbname); // open the Community Counts database
            int CPDateid;
            //
            // process the Code-Point date for this client database
            //
            var codePointDates = db2.cpdates.Where(a => a.CPDate1 == CPDate);
            if (!codePointDates.Any())
            {
                //
                // Add CodePoint date to table and get it's id
                //
                db2.cpdates.Add(new cpdate
                    {
                        CPDate1=CPDate
                    });
                db2.SaveChanges();
                var codePointDate = db2.cpdates.Where(a => a.CPDate1 == CPDate).First();     // must be there this time
                CPDateid = codePointDate.idCPDate;                                          // Get the id for this CodePoint Date.
            }
            else
            {
                CPDateid = codePointDates.First().idCPDate;                                 // Get the id for this CodePoint Date.
            }
            var postCodesToDo = db.cppostcodes.Where(a=>a.CPPostCode1.CompareTo(pcode)>=0);
            CodePointData newCounty = new CodePointData {code="", id=-1, name=""};
            CodePointData newDistrict = new CodePointData {code="", id=-1, name=""};
            CodePointData newWard = new CodePointData {code="", id=-1, name=""};
            CodePointData newSHA = new CodePointData {code="", id=-1, name=""};
            CodePointData newPANSHA = new CodePointData {code="", id=-1, name=""};
              
            int ctrPostCodesAdded = 0, ctrCountyCodeChanged = 0, ctrDistrictCodeChanged = 0, ctrWardCodeChanged = 0, ctrShaCodeChanged = 0, ctrPanShaCodechanged = 0;
            int ctrPostCodesVerified = 0;
            string lastCC="", lastDC="", lastWC="", lastLH="", lastRH = "";
            postCodesToDo = postCodesToDo.OrderBy(a => a.CPPostCode1); // always process list in postcode alphabetical order
            var counter = 1;
            foreach (var pcodeBeingProcessed in postCodesToDo.ToList())
            {
                //
                // Ensure related data exists on related tables inc. ensuring CC descriptions updated to match any changes from CP data.
                //
                if (pcodeBeingProcessed.CPPostCodeCC != lastCC)
                {
                    newCounty = processCounty(pcodeBeingProcessed, CPDateid,db, db2);    // ensure county code present in CC tables
                    lastCC = pcodeBeingProcessed.CPPostCodeCC;
                }
                if (pcodeBeingProcessed.CPPostCodeDC != lastDC)
                { 
                    newDistrict = processDistrict(pcodeBeingProcessed, CPDateid, db, db2);// ensure district code present in CC tables
                    lastDC = pcodeBeingProcessed.CPPostCodeDC;
                }
                if (pcodeBeingProcessed.CPPostCodeWC != lastWC)
                { 
                    newWard = processWard(pcodeBeingProcessed, CPDateid, db, db2);// ensure ward code present in CC tables
                    lastWC = pcodeBeingProcessed.CPPostCodeWC;
                }
                if (pcodeBeingProcessed.CPPostCodeLH != lastLH)
                {
                    newSHA = processSHA(pcodeBeingProcessed, CPDateid,db, db2);// ensure sha code present in CC tables
                    lastLH = pcodeBeingProcessed.CPPostCodeLH;
                }
                if (pcodeBeingProcessed.CPPostCodeRH != lastRH)
                {
                    newPANSHA = processPANSHA(pcodeBeingProcessed, CPDateid, db, db2);// ensure PANsha code present in CC tables
                    lastRH = pcodeBeingProcessed.CPPostCodeRH;
                }
                //
                //
                // Retrieve the current postcode settings
                //
                var existingPostcodes = db2.postcodes.Where(a=>a.PostCode1==pcodeBeingProcessed.CPPostCode1);  
                if (existingPostcodes.Any()) // post code already exists in CC postcode database
                {
                    var existingPostCode = existingPostcodes.First();
                    
                    if (!existingPostCode.county.CountyCode.Equals(newCounty.code,StringComparison.Ordinal))   // county codes do not match
                    {
                        if (!silentLogging)
                        {
                            common.messageLog(true, false, true, common.pver+"Changed County Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.county.CountyCode + " to " + newCounty.code);
                        }
                        existingPostCode.idCountyCode = newCounty.id;
                        ctrCountyCodeChanged++;
                    }
                    if (!existingPostCode.district.DistrictCode.Equals(newDistrict.code,StringComparison.Ordinal))   // District names ids do not match
                    {
                        if (!silentLogging)
                        {
                            common.messageLog(true, false, true,common.pver+ "Changed District Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.district.DistrictCode + " to " + newDistrict.code);
                        }
                        existingPostCode.idDistrictCode = newDistrict.id;
                        ctrDistrictCodeChanged++;
                    }
                    if (!existingPostCode.ward.WardCode.Equals(newWard.code, StringComparison.Ordinal))   // Ward names ids do not match
                    {
                        if (!silentLogging)
                        {
                            common.messageLog(true, false, true,common.pver+ "Changed Ward Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.ward.WardCode + " to " + newWard.code);
                        }
                        existingPostCode.idWardCode = newWard.id;
                        ctrWardCodeChanged++;
                    }
                    if (!existingPostCode.nhssha.NHSSHACode.Equals(newSHA.code, StringComparison.Ordinal))   // SHA names ids do not match
                    {
                        if (!silentLogging)
                        {
                            common.messageLog(true, false, true,common.pver+ "Cchanged NHS SHA Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.nhssha.NHSSHACode + " to " + newSHA.code);
                        }
                        existingPostCode.idNHSHACode = newSHA.id;
                        ctrShaCodeChanged++;
                    }
                    if (!existingPostCode.nhspansha.NHSPanSHACode.Equals(newPANSHA.code, StringComparison.Ordinal))   // PAN SHA names ids do not match
                    {
                        if (!silentLogging)
                        {
                            common.messageLog(true, false, true, common.pver+"Changed NHS Pan SHA Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.nhspansha.NHSPanSHACode + " to " + newPANSHA.code);
                        }
                        existingPostCode.idNHSRegHACode = newPANSHA.id;
                        ctrPanShaCodechanged++;
                    }
                    //
                    // mark postcode as having been updated (checked) to current Code-Point id (date)
                    //
                    existingPostCode.idCPDate = CPDateid;
                    db2.Entry(existingPostCode).State = EntityState.Modified;
                    ctrPostCodesVerified++;
                }
                else                                // post code does NOT already exist in postcode database
                {
                    db2.postcodes.Add(new postcode { 
                        PostCode1=pcodeBeingProcessed.CPPostCode1,
                        idWardCode=newWard.id,
                        idDistrictCode=newDistrict.id,
                        idCountyCode=newCounty.id,
                        idNHSHACode=newSHA.id,
                        idNHSRegHACode=newPANSHA.id,
                        idCPDate=CPDateid
                    });
                    ctrPostCodesAdded++;
                   
                }
                counter++;
                if ((counter -1) % 100 == 0)
                {
                    db2.SaveChanges();                                                                               // commit postcode addition or changes
                    common.messageLog(false,false,false,pcodeBeingProcessed.CPPostCode1+",");
                }
            }
            //
            // done all postcodes in the CodePoint dataset
            //
            common.messageLog(true, false, true, common.pver+"CodePoint has updated all Postcodes on Community Counts from CodePoint  with marker " + CPDate+" at "+DateTime.Now.ToLongTimeString());
            common.messageLog(true, false, true,common.pver+ "Number of Postcodes verified  :" + ctrPostCodesVerified);
            common.messageLog(true, false, true,common.pver+ "Number of Postcodes added     :" + ctrPostCodesAdded);
            common.messageLog(true, false, true,common.pver+ "Number of Postcodes where County changed : " + ctrCountyCodeChanged);
            common.messageLog(true, false, true, common.pver+"Number of PostCodes where District changed : " + ctrDistrictCodeChanged);
            common.messageLog(true, false, true,common.pver+ "Number of PostCodes where Ward changed : " + ctrWardCodeChanged);
            common.messageLog(true, false, true, common.pver+"Number of PostCodes where NHS SHA changed : " + ctrShaCodeChanged);
            common.messageLog(true, false, true, common.pver+"Number of PostCodes where NHS Pan SHA changed : " + ctrPanShaCodechanged);
        }
    }
}
