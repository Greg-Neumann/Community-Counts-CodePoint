namespace CCCodePoint
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.ward")]
    public partial class ward
    {
        public ward()
        {
            postcodes = new HashSet<postcode>();
        }

        [Key]
        public int idWardCode { get; set; }

        public int idDistrictCode { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string WardCode { get; set; }

        [Required]
        [StringLength(70)]
        public string Description { get; set; }

        public virtual district district { get; set; }

        public virtual ICollection<postcode> postcodes { get; set; }
    }
}
