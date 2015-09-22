namespace CCCodePoint
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ccmaster.cpnhssha")]
    public partial class cpnhssha
    {
        public cpnhssha()
        {
            cppostcodes = new HashSet<cppostcode>();
        }

        [Key]
        [Column(TypeName = "char")]
        [StringLength(9)]
        public string CPNHSSHACode { get; set; }

        [Required]
        [StringLength(45)]
        public string CPNHSSHAName { get; set; }

        public virtual ICollection<cppostcode> cppostcodes { get; set; }
    }
}