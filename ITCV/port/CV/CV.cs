using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace TPN.NewModel.CV
{
    public class CV
    {
        public CV()
        {
            Education = new HashSet<Education>();
            Jobs = new HashSet<Job>();
            OtherCerts = new HashSet<OtherCerts>();
        }
        
        [Key]
        
        public int CVId { get; set; }

          
        public virtual CVUser User { get; set; }
        
        [Column(TypeName="date")]
        public virtual DateTime DateAdded { get; set; }
        
        [Display(Name="Ваша цель")]
        [Required(ErrorMessage="Поле не должно быть пустым!")]
        public string Purpose { get; set; }
        
        [Required]
        public string SkillsDesc { get; set; }

        [Display(Name="Другая информация")]
        public string OtherInfo { get; set; }

        public virtual ICollection<Education> Education { get; set; }
        
        public virtual ICollection<Job> Jobs{ get; set; }
        
        public virtual ICollection<OtherCerts> OtherCerts { get; set; }

        //[Required]
        
        //public CBSUser CBSUser { get; set; }

        
        
    }

    
}
