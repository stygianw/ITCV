using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;


namespace ITCV.Models.Entities
{
    public class CVUserEntity
    {
               
        public int CVUserID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual CVEntity UserCV { get; set; }


    }

    class CVUserTableConfig : EntityTypeConfiguration<CVUserEntity>
    {
        public CVUserTableConfig()
        {
            HasKey(m => m.CVUserID);
            Property(m => m.Name).HasMaxLength(20).IsRequired();
            Property(m => m.Surname).HasMaxLength(20).IsRequired();
            Property(m => m.DateOfBirth).HasColumnType("date").IsRequired();
            Property(m => m.Address).HasMaxLength(100).IsRequired();
            Property(m => m.Phone).HasMaxLength(20).IsRequired();
            Property(m => m.Email).HasMaxLength(30).IsRequired();
            HasRequired(m => m.UserCV).WithRequiredPrincipal(m => m.User);
            

        }
    }
}
