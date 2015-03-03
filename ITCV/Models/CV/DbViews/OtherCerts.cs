using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ITCV.Models.Entities
{
    public class OtherCertsEntity
    {
        public int OtherCertsId { get; set; }

        public virtual CVEntity RelatedCV { get; set; }

        public string Issuer { get; set; }

        public DateTime IssueDate { get; set; }

        public string Qualification { get; set; }

        public string OtherInfo { get; set; }
    }

    class OtherCertsTableConfig : EntityTypeConfiguration<OtherCertsEntity>
    {
        public OtherCertsTableConfig()
        {
            HasKey(m => m.OtherCertsId);
            HasRequired(m => m.RelatedCV).WithMany(m => m.OtherCerts);
            Property(m => m.Issuer).HasMaxLength(200).IsRequired();
            Property(m => m.IssueDate).IsRequired().HasColumnType("date");
            Property(m => m.Qualification).IsRequired().HasMaxLength(500);
            Property(m => m.OtherInfo).IsOptional().HasMaxLength(500);
        }
    }
}
