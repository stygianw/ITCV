using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TPN.NewModel.CV
{
    public class University
    {
        public University()
        {
            EduGiven = new HashSet<Education>();
            Faculties = new HashSet<Faculty>();
        }

        [ScaffoldColumn(false)]
        public int UniversityId { get; set; }

        [Display(Name = "Название учебного заведения")]
        [Required(ErrorMessage="Поле не должно быть пустым!"), MaxLength(100)]
        [StringLength(100,ErrorMessage="Недопустимая длина ввода!")]
        public string UniversityName { get; set; }

        [Display(Name = "Страна")]
        [Required(ErrorMessage = "Поле не должно быть пустым!"), MaxLength(20)]
        [StringLength(20, ErrorMessage = "Недопустимая длина ввода!")]
        public string Country { get; set; }

        [Display(Name = "Город")]
        [Required(ErrorMessage = "Поле не должно быть пустым!"), MaxLength(20)]
        [StringLength(20, ErrorMessage = "Недопустимая длина ввода!")]
        public string City { get; set; }
        
        public virtual ICollection<Education> EduGiven { get; set; }
        
        public virtual ICollection<Faculty> Faculties { get; set; }

        [Display(Name = "Отметьте, если учебное заведение является ВУЗом")]
        [Required]
        public bool IsHighSchool { get; set; }

        [ScaffoldColumn(false)]
        public bool IsApproved { get; set; }

    }
}
