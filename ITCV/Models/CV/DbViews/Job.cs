using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ITCV.Models.Entities
{
    [Serializable]
    public class JobEntity
    {
        public int JobId { get; set; }
        
        public virtual string Employer { get; set; }

        public DateTime DateOfBeginning { get; set; }

        public DateTime DateOfEnding { get; set; }

        public string Position { get; set; }

        public string Responsibility { get; set; }

        public string PersonalAchievements { get; set; }

        public virtual CVEntity AssociatedCv { get; set; }
    }

    class JobTableConfig : EntityTypeConfiguration<JobEntity>
    {
        public JobTableConfig()
        {
            HasKey(m => m.JobId);
            Property(m => m.Employer).HasMaxLength(100).IsRequired();
            Property(m => m.DateOfBeginning).HasColumnType("date").IsRequired();
            Property(m => m.DateOfEnding).HasColumnType("date").IsRequired();
            Property(m => m.Position).HasMaxLength(100).IsRequired();
            Property(m => m.Responsibility).HasMaxLength(500);
            HasRequired(m => m.AssociatedCv).WithMany(m => m.Jobs);
        }
    }
}
