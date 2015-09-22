namespace CCCodePoint
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CodePoint : DbContext
    {
        public CodePoint()
            : base("name=CodePoint")
        {
        }

        public virtual DbSet<countylist> countylists { get; set; }
        public virtual DbSet<cpcounty> cpcounties { get; set; }
        public virtual DbSet<cpdistrict> cpdistricts { get; set; }
        public virtual DbSet<cpdistrictward> cpdistrictwards { get; set; }
        public virtual DbSet<cpnhspansha> cpnhspanshas { get; set; }
        public virtual DbSet<cpnhssha> cpnhsshas { get; set; }
        public virtual DbSet<cppostcode> cppostcodes { get; set; }
        public virtual DbSet<district> districts { get; set; }
        public virtual DbSet<nhspansha> nhspanshas { get; set; }
        public virtual DbSet<nhssha> nhsshas { get; set; }
        public virtual DbSet<postcode> postcodes { get; set; }
        public virtual DbSet<ward> wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<countylist>()
                .Property(e => e.CountyCode)
                .IsUnicode(false);

            modelBuilder.Entity<countylist>()
                .Property(e => e.CountyName)
                .IsUnicode(false);

            modelBuilder.Entity<countylist>()
                .HasMany(e => e.districts)
                .WithRequired(e => e.countylist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpcounty>()
                .Property(e => e.CPCountyCode)
                .IsUnicode(false);

            modelBuilder.Entity<cpcounty>()
                .Property(e => e.CPCountyName)
                .IsUnicode(false);

            modelBuilder.Entity<cpcounty>()
                .HasMany(e => e.cppostcodes)
                .WithRequired(e => e.cpcounty)
                .HasForeignKey(e => e.CPPostCodeCC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpdistrict>()
                .Property(e => e.CPDistrictCode)
                .IsUnicode(false);

            modelBuilder.Entity<cpdistrict>()
                .Property(e => e.CPDistrictName)
                .IsUnicode(false);

            modelBuilder.Entity<cpdistrict>()
                .HasMany(e => e.cppostcodes)
                .WithRequired(e => e.cpdistrict)
                .HasForeignKey(e => e.CPPostCodeDC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpdistrictward>()
                .Property(e => e.CPDistrictWardCode)
                .IsUnicode(false);

            modelBuilder.Entity<cpdistrictward>()
                .Property(e => e.CPDistrictWardName)
                .IsUnicode(false);

            modelBuilder.Entity<cpdistrictward>()
                .HasMany(e => e.cppostcodes)
                .WithRequired(e => e.cpdistrictward)
                .HasForeignKey(e => e.CPPostCodeWC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpnhspansha>()
                .Property(e => e.CPNHSPanSHACode)
                .IsUnicode(false);

            modelBuilder.Entity<cpnhspansha>()
                .Property(e => e.CPNHSPanSHAName)
                .IsUnicode(false);

            modelBuilder.Entity<cpnhspansha>()
                .HasMany(e => e.cppostcodes)
                .WithRequired(e => e.cpnhspansha)
                .HasForeignKey(e => e.CPPostCodeRH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpnhssha>()
                .Property(e => e.CPNHSSHACode)
                .IsUnicode(false);

            modelBuilder.Entity<cpnhssha>()
                .Property(e => e.CPNHSSHAName)
                .IsUnicode(false);

            modelBuilder.Entity<cpnhssha>()
                .HasMany(e => e.cppostcodes)
                .WithRequired(e => e.cpnhssha)
                .HasForeignKey(e => e.CPPostCodeLH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cppostcode>()
                .Property(e => e.CPPostCode1)
                .IsUnicode(false);

            modelBuilder.Entity<cppostcode>()
                .Property(e => e.CPPostCodeCY)
                .IsUnicode(false);

            modelBuilder.Entity<cppostcode>()
                .Property(e => e.CPPostCodeRH)
                .IsUnicode(false);

            modelBuilder.Entity<cppostcode>()
                .Property(e => e.CPPostCodeLH)
                .IsUnicode(false);

            modelBuilder.Entity<cppostcode>()
                .Property(e => e.CPPostCodeCC)
                .IsUnicode(false);

            modelBuilder.Entity<cppostcode>()
                .Property(e => e.CPPostCodeDC)
                .IsUnicode(false);

            modelBuilder.Entity<cppostcode>()
                .Property(e => e.CPPostCodeWC)
                .IsUnicode(false);

            modelBuilder.Entity<district>()
                .Property(e => e.DistrictCode)
                .IsUnicode(false);

            modelBuilder.Entity<district>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<district>()
                .HasMany(e => e.wards)
                .WithRequired(e => e.district)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<nhspansha>()
                .Property(e => e.NHSPanSHACode)
                .IsUnicode(false);

            modelBuilder.Entity<nhspansha>()
                .Property(e => e.NHSPanSHAName)
                .IsUnicode(false);

            modelBuilder.Entity<nhspansha>()
                .HasMany(e => e.postcodes)
                .WithRequired(e => e.nhspansha)
                .HasForeignKey(e => e.idNHSRegHACode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<nhssha>()
                .Property(e => e.NHSSHACode)
                .IsUnicode(false);

            modelBuilder.Entity<nhssha>()
                .Property(e => e.NHSSHAName)
                .IsUnicode(false);

            modelBuilder.Entity<nhssha>()
                .HasMany(e => e.postcodes)
                .WithRequired(e => e.nhssha)
                .HasForeignKey(e => e.idNHSHACode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<postcode>()
                .Property(e => e.PostCode1)
                .IsUnicode(false);

            modelBuilder.Entity<ward>()
                .Property(e => e.WardCode)
                .IsUnicode(false);

            modelBuilder.Entity<ward>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ward>()
                .HasMany(e => e.postcodes)
                .WithRequired(e => e.ward)
                .WillCascadeOnDelete(false);
        }
    }
}
