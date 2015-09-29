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
        [Key]
        [Column(TypeName = "char")]
        [StringLength(9)]
        public string CPDistrictCode { get; set; }

        [Required]
        [StringLength(45)]
        public string CPDistrictName { get; set; }
    }
}
