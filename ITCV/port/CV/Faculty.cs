using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TPN.NewModel.CV
{
    public class Faculty
    {
        [ScaffoldColumn(false)]
        public int FacultyId { get; set; }

        [Display(Name = "Название факультета")]
        [Required(ErrorMessage="Поле не должно быть пустым!"), MaxLength(100)]
        [StringLength(100, ErrorMessage = "Недопустимая длина ввода!")]
        public string FacultyName { get; set; }
        
        public virtual University RelatedUniversity { get; set; }

        [ScaffoldColumn(false)]
        public bool IsApproved { get; set; }
    }

    
}
