namespace CCCodePoint.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.cpdistrict")]
    public partial class cpdistrict
    {
        public cpdistrict()
        {
            cppostcodes = new HashSet<cppostcode>();
        }

        [Key]
        [Column(TypeName = "char")]
        [StringLength(9)]
        public string CPDistrictCode { get; set; }

        [Required]
        [StringLength(45)]
        public string CPDistrictName { get; set; }

        public virtual ICollection<cppostcode> cppostcodes { get; set; }
    }
    public class cpdistrict_table
    {
        public cpdistrict_table() { }
        public string CPDistrictCode { get; set; }
        public string CPDistrictName { get; set; }
    }
}
