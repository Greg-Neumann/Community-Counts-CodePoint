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
        public static CodePointData processCounty(int idCountyCode, cppostcode cprec, int idCodePointDate, CodePoint db)
        {
            //
            // ensures that County Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the County, and it's name, on the CC tables.
            //
            string cname;
            var county = db.countylists.Find(idCountyCode);                                  // existing county data
            if (county == null)
            {
                // need to add county code to CC database from CodePoint data (cp*)
                var countyData = db.cpcounties.Find(cprec.CPPostCodeCC);
                db.countylists.Add(new countylist
                {
                    CountyCode = cprec.CPPostCodeCC,
                    CountyName = countyData.CPCountyName,
                    idCPDate = idCodePointDate
                });
                db.SaveChanges();
                idCountyCode = db.countylists.Where(a => a.CountyName == countyData.CPCountyName).First().idCountyListCode;
                cname = countyData.CPCountyName;
            }
            else    // county code does exist on CC County table
            {
                string cpCountyName = db.cpcounties.Find(cprec.CPPostCodeCC).CPCountyName;   // get county name - it may have changed
                var countyNameChanged = (cpCountyName != county.CountyName);                 // flag if county name changed for same id
                if (countyNameChanged)
                {
                    county.CountyName = cpCountyName;
                    county.idCPDate = idCodePointDate;
                    db.Entry(county).State = EntityState.Modified;                  // update county name change
                }
                cname = cpCountyName;
            }

            return new CodePointData { id = idCountyCode, name = cname, code=cprec.CPPostCodeCC };
        }
        public static CodePointData processDistrict(int idDistrictCode, cppostcode cprec, int idCodePointDate, CodePoint db)
        {
            //
            // ensures that District Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the District, and it's name, on the CC tables.
            //
            string dname;
            var district = db.districts.Find(idDistrictCode);                                  // existing district data
            if (district==null)
            {
                // need to add district code to CC database from CodePoint data (cp*)
                var districtData=db.cpdistricts.Find(cprec.CPPostCodeDC);
                db.districts.Add(new district { 
                    DistrictCode=cprec.CPPostCodeDC,
                    Description=districtData.CPDistrictName,
                    idCPDate=idCodePointDate
                });
                db.SaveChanges();
                idDistrictCode=db.districts.Where(a=>a.Description==districtData.CPDistrictName).First().idDistrictCode;
                dname = districtData.CPDistrictName;
            }
            else    // district code does exist on CC district table
            {
                string cpDistrictName = db.cpdistricts.Find(cprec.CPPostCodeDC).CPDistrictName;   // get district name - it may have changed
                var districtNameChanged = (cpDistrictName != district.Description);                 // flag if district name changed for same id
                if(districtNameChanged)
                {
                    district.Description = cpDistrictName;
                    district.idCPDate = idCodePointDate;
                    db.Entry(district).State = EntityState.Modified;                  // update district name change
                }
                dname = cpDistrictName;
            }

            return new CodePointData {id=idDistrictCode, name=dname, code=cprec.CPPostCodeDC };
        }
        public static CodePointData processWard(int idWardCode, cppostcode cprec, int idCodePointDate, CodePoint db)
        {
            //
            // ensures that Ward Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the Ward code, and it's name, on the CC tables.
            //
            string wname;
            var ward = db.wards.Find(idWardCode);                                  // existing ward data
            if (ward == null)
            {
                // need to add ward code to CC database from CodePoint data (cp*)
                var wardData = db.cpdistrictwards.Find(cprec.CPPostCodeWC);
                db.wards.Add(new ward
                {
                    WardCode = cprec.CPPostCodeWC,
                    Description = wardData.CPDistrictWardName,
                    idCPDate = idCodePointDate
                });
                db.SaveChanges();
                idWardCode = db.wards.Where(a => a.Description == wardData.CPDistrictWardName).First().idWardCode;
                wname = wardData.CPDistrictWardName;
            }
            else    // ward code does exist on CC district table
            {
                string cpWardName = db.cpdistrictwards.Find(cprec.CPPostCodeWC).CPDistrictWardName ;   // get ward name - it may have changed
                var wardNameChanged = (cpWardName != ward.Description);                 // flag if ward name changed for same id
                if (wardNameChanged)
                {
                    ward.Description = cpWardName;
                    ward.idCPDate = idCodePointDate;
                    db.Entry(ward).State = EntityState.Modified;                  // update ward name change
                }
                wname = cpWardName;
            }

            return new CodePointData { id = idWardCode, name = wname, code=cprec.CPPostCodeWC };
        }
        public static CodePointData processSHA(int idSHACode, cppostcode cprec, int idCodePointDate, CodePoint db)
        {
            //
            // ensures that SHA Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the Ward code, and it's name, on the CC tables.
            //
            string sname;
            var nhssha = db.nhsshas.Find(idSHACode);                                  // existing SHA data
            if (nhssha == null)
            {
                // need to add SHA code to CC database from CodePoint data (cp*)
                var shaData = db.cpnhsshas.Find(cprec.CPPostCodeLH);
                db.nhsshas.Add(new nhssha
                {
                    NHSSHACode = cprec.CPPostCodeLH,
                    NHSSHAName = shaData.CPNHSSHAName,
                    idCPDate = idCodePointDate
                });
                db.SaveChanges();
                idSHACode = db.nhsshas.Where(a => a.NHSSHAName == shaData.CPNHSSHAName).First().idNHSSHACode;
                sname = shaData.CPNHSSHAName;
            }
            else    // SHA code does exist on CC district table
            {
                string cpshaName = db.cpnhsshas.Find(cprec.CPPostCodeLH).CPNHSSHAName;   // get SHA name - it may have changed
                var shaNameChanged = (cpshaName != nhssha.NHSSHAName);                 // flag if SHA name changed for same id
                if (shaNameChanged)
                {
                    nhssha.NHSSHAName = cpshaName;
                    nhssha.idCPDate = idCodePointDate;
                    db.Entry(nhssha).State = EntityState.Modified;                  // update sha name change
                }
                sname = cpshaName;
            }

            return new CodePointData { id = idSHACode, name = sname, code=cprec.CPPostCodeLH };
        }
        public static CodePointData processPANSHA(int idPANSHACode, cppostcode cprec, int idCodePointDate, CodePoint db)
        {
            //
            // ensures that PanSHA Code on CC tables exists and is of correct name (allowing for changes from CodePoint data)
            // returns the id of the Ward code, and it's name, on the CC tables.
            //
            string pname;
            var pannhssha = db.nhspanshas.Find(idPANSHACode);                                  // existing PANSHA data
            if (pannhssha == null)
            {
                // need to add PANSHA code to CC database from CodePoint data (cp*)
                var panshaData = db.cpnhspanshas.Find(cprec.CPPostCodeRH);
                db.nhspanshas.Add(new nhspansha
                {
                    NHSPanSHACode = cprec.CPPostCodeRH,
                    NHSPanSHAName = panshaData.CPNHSPanSHAName,
                    idCPDate = idCodePointDate
                });
                db.SaveChanges();
                idPANSHACode = db.nhspanshas.Where(a => a.NHSPanSHAName == panshaData.CPNHSPanSHAName).First().idNHSPanSHACode;
                pname = panshaData.CPNHSPanSHAName;
            }
            else    // PANSHA code does exist on CC district table
            {
                string cppanshaName = db.cpnhspanshas.Find(cprec.CPPostCodeRH).CPNHSPanSHAName;   // get PANSHA name - it may have changed
                var panshaNameChanged = (cppanshaName != pannhssha.NHSPanSHAName);                 // flag if PANSHA name changed for same id
                if (panshaNameChanged)
                {
                    pannhssha.NHSPanSHAName = cppanshaName;
                    pannhssha.idCPDate = idCodePointDate;
                    db.Entry(pannhssha).State = EntityState.Modified;                  // update sha name change
                }
                pname = cppanshaName;
            }

            return new CodePointData { id = idPANSHACode, name = pname, code=cprec.CPPostCodeRH};

        }
        public static void processUpdate(string CPDate, string pcode) // will be null if all postcodes to be updated
        {
            common.messageLog(true, false, true, "CodePoint Update processing will mark all data with CodePoint Date " + CPDate);
            if (pcode!=null)
            {
                common.messageLog(true, false, true, "CodePoint will resume processing from (including) postcode " + pcode);
            }
            else
            {
                pcode = "";
            }
            CodePoint db = new CodePoint();     // open the CodePoint database; 
            int CPDateid;
            //
            // process the Code-Point date
            //
            var codePointDates = db.cpdates.Where(a => a.CPDate1 == CPDate);
            if (!codePointDates.Any())
            {
                //
                // Add CodePoint date to table and get it's id
                //
                db.cpdates.Add(new cpdate
                    {
                        CPDate1=CPDate
                    });
                db.SaveChanges();
                var codePointDate = db.cpdates.Where(a => a.CPDate1 == CPDate).First();     // must be there this time
                CPDateid = codePointDate.idCPDate;                                          // Get the id for this CodePoint Date.
            }
            else
            {
                CPDateid = codePointDates.First().idCPDate;                                 // Get the id for this CodePoint Date.
            }
            var postCodesToDo = db.cppostcodes.Where(a=>a.CPPostCode1.CompareTo(pcode)>=0);
            CodePointData newCounty, newDistrict, newWard, newSHA, newPANSHA;
            postCodesToDo = postCodesToDo.OrderBy(a => a.CPPostCode1); // always process list in postcode alphabetical order
            var counter = 1;
            foreach (var pcodeBeingProcessed in postCodesToDo.ToList())
            {
                //
                // Retrieve the current postcode settings
                //
                var existingPostcodes = db.postcodes.Where(a=>a.PostCode1==pcodeBeingProcessed.CPPostCode1);  
                if (existingPostcodes.Any()) // post code already exists in CC postcode database
                {
                    var existingPostCode = existingPostcodes.First();
                    newCounty = processCounty(existingPostCode.idCountyCode, pcodeBeingProcessed, CPDateid, db);    // ensure county code present in CC tables
                    newDistrict = processDistrict(existingPostCode.idDistrictCode, pcodeBeingProcessed, CPDateid, db);// ensure district code present in CC tables
                    newWard = processWard(existingPostCode.idWardCode, pcodeBeingProcessed, CPDateid, db);// ensure ward code present in CC tables
                    newSHA = processSHA(existingPostCode.idNHSHACode, pcodeBeingProcessed, CPDateid, db);// ensure sha code present in CC tables
                    newPANSHA = processPANSHA(existingPostCode.idNHSRegHACode, pcodeBeingProcessed, CPDateid, db);// ensure PANsha code present in CC tables
                    if (!existingPostCode.countylist.CountyCode.Equals(newCounty.code,StringComparison.Ordinal))   // county codes do not match
                    {
                        common.messageLog(true, false, true, "CodePoint changed County Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.countylist.CountyCode + " to " + newCounty.code);
                        existingPostCode.idCountyCode = newCounty.id;
                        db.Entry(db.postcodes).State = EntityState.Modified;
                    }
                    if (!existingPostCode.district.DistrictCode.Equals(newDistrict.code,StringComparison.Ordinal))   // District names ids do not match
                    {
                        common.messageLog(true, false, true, "CodePoint changed District Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.district.DistrictCode + " to " + newDistrict.code);
                        existingPostCode.idDistrictCode = newDistrict.id;
                        db.Entry(db.postcodes).State = EntityState.Modified;
                    }
                    if (!existingPostCode.ward.WardCode.Equals(newWard.code, StringComparison.Ordinal))   // Ward names ids do not match
                    {
                        common.messageLog(true, false, true, "CodePoint changed Ward Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.ward.WardCode + " to " + newWard.code);
                        existingPostCode.idWardCode = newWard.id;
                        db.Entry(db.postcodes).State = EntityState.Modified;
                    }
                    if (!existingPostCode.nhssha.NHSSHACode.Equals(newSHA.code, StringComparison.Ordinal))   // SHA names ids do not match
                    {
                        common.messageLog(true, false, true, "CodePoint changed NHS SHA Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.nhssha.NHSSHACode + " to " + newSHA.code);
                        existingPostCode.idNHSHACode = newSHA.id;
                        db.Entry(db.postcodes).State = EntityState.Modified;
                    }
                    if (!existingPostCode.nhspansha.NHSPanSHACode.Equals(newPANSHA.code, StringComparison.Ordinal))   // PAN SHA names ids do not match
                    {
                        common.messageLog(true, false, true, "CodePoint changed NHS Pan SHA Code for postcode " + existingPostCode.PostCode1 + " from " + existingPostCode.nhspansha.NHSPanSHACode + " to " + newPANSHA.code);
                        existingPostCode.idNHSRegHACode = newPANSHA.id;
                        db.Entry(db.postcodes).State = EntityState.Modified;
                    }
                }
                else                                // post code does NOT already exist in postcode database
                {
                    var idCountyCode = -1;
                    var countyRec = db.countylists.Where(a=>a.CountyCode==pcodeBeingProcessed.CPPostCodeCC);
                    if (countyRec.Any())            // but county code does - reuse it.
                    {
                        idCountyCode = countyRec.First().idCountyListCode;
                    }
                    newCounty = processCounty(idCountyCode, pcodeBeingProcessed, CPDateid, db);                      // make sure Code Point county exists before created CC postcode row
                    //
                    var idDistrictCode = -1;
                    var districtRec = db.districts.Where(a => a.DistrictCode == pcodeBeingProcessed.CPPostCodeDC);
                    if (districtRec.Any())
                    {
                        idDistrictCode = districtRec.First().idDistrictCode;
                    }
                    newDistrict = processDistrict(idDistrictCode, pcodeBeingProcessed, CPDateid, db);               // ensure district code present in CC tables
                    //
                    var idWardCode = -1;
                    var wardRec = db.wards.Where(a => a.WardCode == pcodeBeingProcessed.CPPostCodeWC);
                    if (wardRec.Any())
                    {
                        idWardCode = wardRec.First().idWardCode;
                    }
                    newWard = processWard(idWardCode, pcodeBeingProcessed, CPDateid, db);                           // ensure ward code present in CC tables
                    //
                    var idSHACode = -1;
                    var shaRec = db.nhsshas.Where(a => a.NHSSHACode == pcodeBeingProcessed.CPPostCodeLH);
                    if (shaRec.Any())
                    {
                        idSHACode = shaRec.First().idNHSSHACode;
                    }
                    newSHA = processSHA(idSHACode, pcodeBeingProcessed, CPDateid, db);                           // ensure SHA code present in CC tables
                    //
                    var idPANSHACode = -1;
                    var panshaRec = db.nhspanshas.Where(a => a.NHSPanSHACode== pcodeBeingProcessed.CPPostCodeRH);
                    if (panshaRec.Any())
                    {
                        idPANSHACode = panshaRec.First().idNHSPanSHACode;
                    }
                    newPANSHA = processPANSHA(idPANSHACode, pcodeBeingProcessed, CPDateid, db);                           // ensure PANSHA code present in CC tables
                    db.postcodes.Add(new postcode { 
                        PostCode1=pcodeBeingProcessed.CPPostCode1,
                        idWardCode=newWard.id,
                        idDistrictCode=newDistrict.id,
                        idCountyCode=newCounty.id,
                        idNHSHACode=newSHA.id,
                        idNHSRegHACode=newPANSHA.id,
                        idCPDate=CPDateid
                    });
                   
                }
                db.SaveChanges();                                                                               // commit postcode addition or changes
                counter++;
                if ((counter -1) % 1000 == 0)
                {
                    common.messageLog(false,false,false,"..");
                }
            }
            //
            // done all psotcodes in the CodePoint dataset
            //
            common.messageLog(true, false, true, "CodePoint has updated all Postcodes on Community Counts from CodePoint  with marker " + CPDate+" at "+DateTime.Now.ToLongTimeString());
        }
    }
}
