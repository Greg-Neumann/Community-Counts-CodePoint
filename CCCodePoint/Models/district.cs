namespace CCCodePoint.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.district")]
    public partial class district
    {
        public district()
        {
            wards = new HashSet<ward>();
        }

        [Key]
        public int idDistrictCode { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(9)]
        public string DistrictCode { get; set; }

        [Required]
        [StringLength(45)]
        public string Description { get; set; }

        public int idCountyListCode { get; set; }

        public virtual countylist countylist { get; set; }

        public virtual ICollection<ward> wards { get; set; }
    }
}
