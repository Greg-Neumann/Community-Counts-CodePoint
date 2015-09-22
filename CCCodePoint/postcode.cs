namespace CCCodePoint
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.postcode")]
    public partial class postcode
    {
        [Key]
        public int idPostCode { get; set; }

        [Column("PostCode", TypeName = "char")]
        [Required]
        [StringLength(8)]
        public string PostCode1 { get; set; }

        public int idWardCode { get; set; }

        public int idNHSHACode { get; set; }

        public int idNHSRegHACode { get; set; }

        public virtual nhspansha nhspansha { get; set; }

        public virtual nhssha nhssha { get; set; }

        public virtual ward ward { get; set; }
    }
}
