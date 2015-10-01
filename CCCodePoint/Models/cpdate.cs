namespace CCCodePoint.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.cpdate")]
    public partial class cpdate
    {
        public cpdate()
        {
            countylists = new HashSet<county>();
            districts = new HashSet<district>();
            nhspanshas = new HashSet<nhspansha>();
            nhsshas = new HashSet<nhssha>();
            wards = new HashSet<ward>();
        }

        [Key]
        public int idCPDate { get; set; }

        [Column("CPDate", TypeName = "char")]
        [Required]
        [StringLength(7)]
        public string CPDate1 { get; set; }

        public virtual ICollection<county> countylists { get; set; }

        public virtual ICollection<district> districts { get; set; }

        public virtual ICollection<nhspansha> nhspanshas { get; set; }

        public virtual ICollection<nhssha> nhsshas { get; set; }

        public virtual ICollection<ward> wards { get; set; }
    }
}
