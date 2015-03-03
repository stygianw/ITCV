using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ITCV.Models.Entities
{
    [Serializable]
    public class EducationEntity
    {
        public int EducationId { get; set; }
        
        public virtual UniversityEntity University { get; set; }
        
        public virtual FacultyEntity Faculty { get; set; }

        public DateTime EduBegin { get; set; }

        public DateTime EduEnd { get; set; }

        public string Qualification { get; set; }

        public string Specialty { get; set; }
        
        public virtual CVEntity RelatedCV { get; set; }

    }

    class EducationTableConfig : EntityTypeConfiguration<EducationEntity>
    {
        public EducationTableConfig()
        {
            HasKey(m => m.EducationId);
            HasRequired(m => m.University).WithMany(m => m.EduGiven);
            HasRequired(m => m.Faculty);
            Property(m => m.EduBegin).HasColumnType("date").IsRequired();
            Property(m => m.EduEnd).HasColumnType("date").IsRequired();
            Property(m => m.Qualification).HasMaxLength(200);
            Property(m => m.Specialty).HasMaxLength(400).IsRequired();
            HasRequired(m => m.RelatedCV).WithMany(m => m.Education);
        }
    }
}
