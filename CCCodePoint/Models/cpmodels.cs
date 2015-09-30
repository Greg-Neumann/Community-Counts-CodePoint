using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCodePoint.Models
{
    public class cpcounty_table
    {
        public cpcounty_table() { }
        public string CPCountyCode { get; set; }
        public string CPCountyName { get; set; }

    }
    public class cpdistrict_table
    {
        public cpdistrict_table() { }
        public string CPDistrictCode { get; set; }
        public string CPDistrictName { get; set; }
    }
    public class cpdistrictward_table
    {
        public cpdistrictward_table() { }
        public string CPDistrictWardCode { get; set; }
        public string CPDistrictWardName { get; set; }
    }
    public class cpnhspansha_table
    {
        public cpnhspansha_table() { }
        public string CPNHSPanSHACode { get; set; }
        public string CPNHSPanSHAName { get; set; }
    }
    public class cpnhssha_table
    {
        public cpnhssha_table() { }
        public string CPNHSSHACode { get; set; }
        public string CPNHSSHAName { get; set; }
    }
    public class CodePointData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }
}
