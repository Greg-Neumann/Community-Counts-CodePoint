namespace CCCodePoint
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.cpnhspansha")]
    public partial class cpnhspansha
    {
        public cpnhspansha()
        {
            cppostcodes = new HashSet<cppostcode>();
        }

        [Key]
        [Column(TypeName = "char")]
        [StringLength(9)]
        public string CPNHSPanSHACode { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(70)]
        public string CPNHSPanSHAName { get; set; }

        public virtual ICollection<cppostcode> cppostcodes { get; set; }
    }
}