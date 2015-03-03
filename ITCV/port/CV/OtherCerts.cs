using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPN.NewModel.CV
{
    public class OtherCerts
    {
        [ScaffoldColumn(false)]
        public int OtherCertsId { get; set; }

        public virtual CV RelatedCV { get; set; }

        [Display(Name = "Компания, выдавшая сертификат")]
        [Required, MaxLength(20)]
        [StringLength(20,ErrorMessage="Недопустимая длина ввода!")]
        public string Issuer { get; set; }

        [Display(Name = "Дата выдачи")]
        [Column(TypeName="date")]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Квалификация")]
        [Required, MaxLength(50)]
        [StringLength(50,ErrorMessage="Недопустимая длина ввода!")]
        public string Qualification { get; set; }

        [Display(Name = "Другая информация")]
        [DataType(DataType.MultilineText)]
        public string OtherInfo { get; set; }
    }
}
