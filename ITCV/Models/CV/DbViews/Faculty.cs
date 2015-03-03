using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace ITCV.Models.Entities
{
    public class FacultyEntity
    {
        [Key]
        public int FacultyId { get; set; }

        public string FacultyName { get; set; }
        
        public virtual UniversityEntity RelatedUniversity { get; set; }

        public bool IsApproved { get; set; }
    }

    class FacultyTableConfig : EntityTypeConfiguration<FacultyEntity>
    {
        public FacultyTableConfig()
        {
            //HasKey(m => m.FacultyId);
            Property(m => m.FacultyName).HasMaxLength(200).IsRequired();
            HasOptional(m => m.RelatedUniversity).WithMany(m => m.Faculties);
        }
    }



    
}
