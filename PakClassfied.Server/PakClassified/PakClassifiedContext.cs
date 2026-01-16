using Microsoft.EntityFrameworkCore;
using PakClassified.Entities.Location;
using PakClassified.Entities.PakClassified;
using PakClassified.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified
{
    public class PakClassifiedContext : DbContext
    {
        #region Location
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityArea> CityAreas { get; set; }

        #endregion

        #region Advertisement
        public DbSet<AdvertisementCategory> AdvertisementCategories { get; set; }
        public DbSet<AdvertisementSubCategory> AdvertisementSubCategories { get; set; }
        public DbSet<AdvertisementStatus> AdvertisementStatuses { get; set; }
        public DbSet<AdvertisementType> AdvertisementTypes { get; set; }
        public DbSet<AdvertisementTag> AdvertisementTags { get; set; }
        public DbSet<AdvertisementImage> AdvertisementImages { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }

        #endregion

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=JALEEL\\SQLEXPRESS;Database=PakClassified;User Id=jaleel;Password=jalil7860;TrustServerCertificate=true;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PakClassifiedContext).Assembly);
        }
    }
}
