using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace ITCV.Models.Entities
{
    
    public class CVEntity
    {
        public CVEntity()
        {
            Education = new HashSet<EducationEntity>();
            Jobs = new HashSet<JobEntity>();
            OtherCerts = new HashSet<OtherCertsEntity>();
        }
        
        public int CVId { get; set; }

          
        public virtual CVUserEntity User { get; set; }
        
        
        public virtual DateTime DateAdded { get; set; }
        
        public string Purpose { get; set; }
        
        public string SkillsDesc { get; set; }

        public string OtherInfo { get; set; }

        public virtual ICollection<EducationEntity> Education { get; set; }
        
        public virtual ICollection<JobEntity> Jobs{ get; set; }
        
        public virtual ICollection<OtherCertsEntity> OtherCerts { get; set; }

        
        
        
    }

    public class CVTableConfig : EntityTypeConfiguration<CVEntity>
    {
        public CVTableConfig()
        {
            HasKey(m => m.CVId);
            Property(m => m.DateAdded).HasColumnType("datetime2");
            Property(m => m.OtherInfo).HasMaxLength(300);
            Property(m => m.Purpose).HasMaxLength(200);
            Property(m => m.SkillsDesc).IsRequired().IsMaxLength();
            HasRequired(m => m.User).WithRequiredDependent(m => m.UserCV);
            HasMany(m => m.Education).WithRequired(m => m.RelatedCV);
            HasMany(m => m.Jobs).WithRequired(m => m.AssociatedCv);
            HasMany(m => m.OtherCerts).WithRequired(m => m.RelatedCV);
        }
    }
    
}
