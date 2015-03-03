using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITCV.Models.Interfaces;
using ITCV.Models.Entities;

namespace ITCV.Models.Views
{
    [Serializable]
    public class Education
    {
        public int EducationId { get; set; }
        
        [Required]
        public virtual University University { get; set; }
        
        public virtual Faculty Faculty { get; set; }

        [Display(Name = "Дата начала образования")]
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        [Column(TypeName = "date")]
        public DateTime EduBegin { get; set; }

        [Display(Name = "Дата конца образования")]
        [Required(ErrorMessage="Поле не должно быть пустым!")]
        [Column(TypeName = "date")]
        public DateTime EduEnd { get; set; }

        [Display(Name = "Полученная квалификация")]
        [Required(ErrorMessage = "Поле не должно быть пустым!"), MaxLength(100)]
        [StringLength(100, ErrorMessage="Недопустимая длина ввода!")]
        public string Qualification { get; set; }

        [Display(Name = "Полученная специальность")]
        [Required(ErrorMessage = "Поле не должно быть пустым!"), MaxLength(70)]
        [StringLength(70, ErrorMessage="Недопустимая длина ввода!")]
        public string Specialty { get; set; }
        
        public virtual CV RelatedCV { get; set; }

    }
}
