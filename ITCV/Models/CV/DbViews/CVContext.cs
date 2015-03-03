using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ITCV.Models.Entities;

namespace ITCV.Models.Entities
{
    public class CVContext : DbContext
    {
        public CVContext() : base (@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\StygianW\Documents\Visual Studio 2013\Projects\ITCV\ITCV\App_Data\CVData1.mdf;Integrated Security=True")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CVTableConfig());
            modelBuilder.Configurations.Add(new CVUserTableConfig());
            modelBuilder.Configurations.Add(new EducationTableConfig());
//            modelBuilder.Configurations.Add(new EducationTableConfig());
            modelBuilder.Configurations.Add(new JobTableConfig());
            modelBuilder.Configurations.Add(new OtherCertsTableConfig());
            modelBuilder.Configurations.Add(new UniversityTableConfig());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CVEntity> CVs { get; set; }
        public DbSet<CVUserEntity> CVUsers { get; set; }
        public DbSet<EducationEntity> Educations { get; set; }
        public DbSet<FacultyEntity> Faculties { get; set; }
        public DbSet<JobEntity> Jobs{ get; set; }
        public DbSet<OtherCertsEntity> OtherCerts { get; set; }
        public DbSet<UniversityEntity> Universities { get; set; }
    }
}