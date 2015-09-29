using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCCodePoint.Models;

namespace CCCodePoint
{
    class updateCodePoint
    {
        public static void processUpdate(string CPDate, string pcode) // will be null if all postcodes to be updated
        {
            common.messageLog(true, false, true, "CodePoint Update processing will mark all data with CodePoint Date " + CPDate);
            if (pcode!=null)
            {
                common.messageLog(true, false, true, "CodePoint will resume processing from (including) postcode " + pcode);
            }
            CodePoint db = new CodePoint();     // open the CodePoint database; 
            var postCodesToDo = db.cppostcodes.Where(a=>a.CPPostCode1.CompareTo(pcode)>=0);
            postCodesToDo = postCodesToDo.OrderBy(a => a.CPPostCode1); // always process list in postcode alphabetical order
            foreach (var pcodeBeingProcesses in postCodesToDo)
            {
                //
                // Has this postcode's County value changed?
                //
               
            }
        }
    }
}
