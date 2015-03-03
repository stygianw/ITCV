using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITCV.Models.Interfaces;
using ITCV.Models.Entities;

namespace ITCV.Models.Views
{
    [Serializable]
    public class CVUser : ITransient<CVUserEntity>
    {
               
        public int CVUserID { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        [StringLength(20, ErrorMessage = "Недопустимая длина ввода!")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!"), Display(Name = "Фамилия")]
        [StringLength(20, ErrorMessage = "Недопустимая длина ввода!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!"), Display(Name = "Дата рождения")]
        [Column(TypeName = "date")]

        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(50, ErrorMessage = "Недопустимая длина ввода!")]
        public string Address { get; set; }

        [Phone, Required(ErrorMessage = "Поле не должно быть пустым!"), Display(Name = "Телефон")]
        [StringLength(20, ErrorMessage = "Недопустимая длина ввода!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!"), EmailAddress,  Display(Name = "Эл.почта")]
        [StringLength(40, ErrorMessage = "Недопустимая длина ввода!")]
        public string Email { get; set; }

        [Required]
        public virtual CV UserCV { get; set; }



        public CVUserEntity Transit()
        {
            throw new NotImplementedException();
        }
    }

    class CVUserTableConfig : EntityTypeConfiguration<CVUser>
    {
        public CVUserTableConfig()
        {
            HasKey(m => m.CVUserID);
            Property(m => m.Name).HasMaxLength(20).IsRequired();
            Property(m => m.Surname).HasMaxLength(20).IsRequired();
            Property(m => m.DateOfBirth).HasColumnType("date").IsRequired();
            Property(m => m.Address).HasMaxLength(100).IsRequired();
            

        }
    }
}
