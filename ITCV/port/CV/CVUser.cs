using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPN.NewModel.CV
{
    public class CVUser
    {
        [Key]
        public int UserLoginId { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!"), MaxLength(20)]
        [StringLength(20, ErrorMessage = "Недопустимая длина ввода!")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!"), MaxLength(20), Display(Name = "Фамилия")]
        [StringLength(20, ErrorMessage = "Недопустимая длина ввода!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!"), Display(Name = "Дата рождения")]
        [Column(TypeName = "date")]

        public DateTime DateOfBirth { get; set; }

        [MaxLength(50), Display(Name = "Адрес")]
        [StringLength(50, ErrorMessage = "Недопустимая длина ввода!")]
        public string Address { get; set; }

        [Phone, Required(ErrorMessage = "Поле не должно быть пустым!"), MaxLength(20), Display(Name = "Телефон")]
        [StringLength(20, ErrorMessage = "Недопустимая длина ввода!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!"), EmailAddress, MaxLength(40), Display(Name = "Эл.почта")]
        [StringLength(40, ErrorMessage = "Недопустимая длина ввода!")]
        public string Email { get; set; }

        [Required]
        public virtual CV UserCV { get; set; }


    }
}
