namespace CCCodePoint.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.cpnhspansha")]
    public partial class cpnhspansha
    {
        [Key]
        [Column(TypeName = "char")]
        [StringLength(9)]
        public string CPNHSPanSHACode { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(70)]
        public string CPNHSPanSHAName { get; set; }
    }
}
