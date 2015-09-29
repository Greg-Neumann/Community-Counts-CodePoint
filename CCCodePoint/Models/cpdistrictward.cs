namespace CCCodePoint.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.cpdistrictward")]
    public partial class cpdistrictward
    {
        [Key]
        [Column(TypeName = "char")]
        [StringLength(9)]
        public string CPDistrictWardCode { get; set; }

        [Required]
        [StringLength(70)]
        public string CPDistrictWardName { get; set; }
    }
}
