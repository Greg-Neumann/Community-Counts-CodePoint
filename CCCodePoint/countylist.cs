namespace CCCodePoint
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.countylist")]
    public partial class countylist
    {
        public countylist()
        {
            districts = new HashSet<district>();
        }

        [Key]
        public int idCountyListCode { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(9)]
        public string CountyCode { get; set; }

        [Required]
        [StringLength(45)]
        public string CountyName { get; set; }

        public virtual ICollection<district> districts { get; set; }
    }
}
