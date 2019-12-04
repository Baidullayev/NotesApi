using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Gala.Models
{
    public partial class projectzeroContext : DbContext
    {
        public projectzeroContext()
        {
        }

        public projectzeroContext(DbContextOptions<projectzeroContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<Persons2> Persons2 { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("User Id=user2;Password=user2;Host=localhost;Database=projectzero;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Districts>(entity =>
            {
                entity.ToTable("districts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<Persons2>(entity =>
            {
                entity.HasKey(e => e.Iin)
                    .HasName("PRIMARY");

                entity.ToTable("persons2");

                entity.HasIndex(e => e.BirthDate)
                    .HasName("birthdate");

                entity.HasIndex(e => e.DistrictCode)
                    .HasName("fk_districts_idx");

                entity.HasIndex(e => e.Firstname)
                    .HasName("firstname");

                entity.HasIndex(e => e.Patronymic)
                    .HasName("patronymic");

                entity.HasIndex(e => e.RegionCode)
                    .HasName("fk_region_idx");

                entity.HasIndex(e => e.Surname)
                    .HasName("surname");

                entity.HasIndex(e => new { e.Surname, e.Firstname })
                    .HasName("firstname_surname");

                entity.HasIndex(e => new { e.Surname, e.Firstname, e.Patronymic })
                    .HasName("firtsname_surname_patronymic");

                entity.Property(e => e.Iin)
                    .HasColumnName("iin")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.DeathDate)
                    .HasColumnName("death_date")
                    .HasColumnType("date");

                entity.Property(e => e.DistrictCode)
                    .HasColumnName("district_code")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.LbgBeginDate)
                    .HasColumnName("lbg_begin_date")
                    .HasColumnType("date");

                entity.Property(e => e.LbgNumber)
                    .HasColumnName("lbg_number")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LbgOrganCode).HasColumnName("lbg_organ_code");

                entity.Property(e => e.LbgTypeCode).HasColumnName("lbg_type_code");

                entity.Property(e => e.PasspBeginDate)
                    .HasColumnName("passp_begin_date")
                    .HasColumnType("date");

                entity.Property(e => e.PasspNumber)
                    .HasColumnName("passp_number")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PasspOrganCode).HasColumnName("passp_organ_code");

                entity.Property(e => e.PasspTypeCode).HasColumnName("passp_type_code");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.PersonStatusCode).HasColumnName("person_status_code");

                entity.Property(e => e.RegAddressBuilding)
                    .HasColumnName("reg_address_building")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RegAddressCity)
                    .HasColumnName("reg_address_city")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RegAddressCorpus)
                    .HasColumnName("reg_address_corpus")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RegAddressFlat)
                    .HasColumnName("reg_address_flat")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RegAddressStreet)
                    .HasColumnName("reg_address_street")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RegionCode)
                    .HasColumnName("region_code")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.UdlBeginDate)
                    .HasColumnName("udl_begin_date")
                    .HasColumnType("date");

                entity.Property(e => e.UdlNumber)
                    .HasColumnName("udl_number")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UdlOrganCode).HasColumnName("udl_organ_code");

                entity.Property(e => e.UdlTypeCode).HasColumnName("udl_type_code");

                entity.Property(e => e.VnzhBeginDate)
                    .HasColumnName("vnzh_begin_date")
                    .HasColumnType("date");

                entity.Property(e => e.VnzhNumber)
                    .HasColumnName("vnzh_number")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.VnzhOrganCode).HasColumnName("vnzh_organ_code");

                entity.Property(e => e.VnzhTypeCode).HasColumnName("vnzh_type_code");

                entity.HasOne(d => d.DistrictCodeNavigation)
                    .WithMany(p => p.Persons2)
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_districts");

                entity.HasOne(d => d.RegionCodeNavigation)
                    .WithMany(p => p.Persons2)
                    .HasForeignKey(d => d.RegionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_region");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.ToTable("regions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(256)");
            });
        }
    }
}
