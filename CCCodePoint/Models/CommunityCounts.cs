namespace CCCodePoint.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CommunityCounts : DbContext
    {
        public CommunityCounts()
            : base()
        {
        }
        public CommunityCounts (string dbname) : base(GetConnectionString(dbname)) { }
        public static string GetConnectionString(string dbname)
        {
            switch (dbname)
            {
                case "ccbhlc":
                    return "name=ccbhlc";
                case "cccrow":
                    return "name=cccrow";
                case "ccsydn":
                    return "name=ccsydn";
                case "ccmaster":
                    return "name=ccmaster";
                default :
                    throw new ArgumentOutOfRangeException("logon domain name not recognised");

            }
        }

        public virtual DbSet<county> counties { get; set; }
        public virtual DbSet<cpdate> cpdates { get; set; }
        public virtual DbSet<district> districts { get; set; }
        public virtual DbSet<nhspansha> nhspanshas { get; set; }
        public virtual DbSet<nhssha> nhsshas { get; set; }
        public virtual DbSet<postcode> postcodes { get; set; }
        public virtual DbSet<ward> wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<county>()
                .Property(e => e.CountyCode)
                .IsUnicode(false);

            modelBuilder.Entity<county>()
                .Property(e => e.CountyName)
                .IsUnicode(false);

            modelBuilder.Entity<county>()
                .HasMany(e => e.postcodes)
                .WithRequired(e => e.county)
                .HasForeignKey(e => e.idCountyCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpdate>()
                .Property(e => e.CPDate1)
                .IsUnicode(false);

            modelBuilder.Entity<cpdate>()
                .HasMany(e => e.counties)
                .WithRequired(e => e.cpdate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpdate>()
                .HasMany(e => e.districts)
                .WithRequired(e => e.cpdate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpdate>()
                .HasMany(e => e.nhspanshas)
                .WithRequired(e => e.cpdate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpdate>()
                .HasMany(e => e.nhsshas)
                .WithRequired(e => e.cpdate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpdate>()
                .HasMany(e => e.wards)
                .WithRequired(e => e.cpdate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<district>()
                .Property(e => e.DistrictCode)
                .IsUnicode(false);

            modelBuilder.Entity<district>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<district>()
                .HasMany(e => e.postcodes)
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
