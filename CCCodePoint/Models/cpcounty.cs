namespace CCCodePoint.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.cpcounty")]
    public partial class cpcounty
    {
        public cpcounty()
        {
            cppostcodes = new HashSet<cppostcode>();
        }

        [Key]
        [Column(TypeName = "char")]
        [StringLength(9)]
        public string CPCountyCode { get; set; }

        [Required]
        [StringLength(45)]
        public string CPCountyName { get; set; }

        public virtual ICollection<cppostcode> cppostcodes { get; set; }
    }
    
}
