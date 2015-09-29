namespace CCCodePoint.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.cppostcode")]
    public partial class cppostcode
    {
        [Key]
        [Column("CPPostCode", TypeName = "char")]
        [StringLength(8)]
        public string CPPostCode1 { get; set; }

        public int CPPostCodePQ { get; set; }

        public int CPPostCodeEA { get; set; }

        public int CPPostCodeNO { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(9)]
        public string CPPostCodeCY { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(9)]
        public string CPPostCodeRH { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(9)]
        public string CPPostCodeLH { get; set; }

        [Column(TypeName = "char")]
        [StringLength(9)]
        public string CPPostCodeCC { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(9)]
        public string CPPostCodeDC { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(9)]
        public string CPPostCodeWC { get; set; }
    }
}
