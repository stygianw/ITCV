using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ITCV.Models.Entities
{
    public class UniversityEntity
    {
        public UniversityEntity()
        {
            EduGiven = new HashSet<EducationEntity>();
            Faculties = new HashSet<FacultyEntity>();
        }

        public int UniversityId { get; set; }

        public string UniversityName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
        
        public virtual ICollection<EducationEntity> EduGiven { get; set; }
        
        public virtual ICollection<FacultyEntity> Faculties { get; set; }

        public bool IsHighSchool { get; set; }

        public bool IsApproved { get; set; }

    }

    class UniversityTableConfig : EntityTypeConfiguration<UniversityEntity>
    {
        public UniversityTableConfig()
        {
            HasKey(m => m.UniversityId);
            Property(m => m.UniversityName).HasMaxLength(400).IsRequired();
            Property(m => m.City).HasMaxLength(50);
            Property(m => m.Country).HasMaxLength(50);
            HasMany(m => m.EduGiven).WithRequired(m => m.University);
            HasMany(m => m.Faculties).WithOptional(m => m.RelatedUniversity);
        }
    }
}
