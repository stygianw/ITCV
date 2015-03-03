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
    public class Job : ITransient<JobEntity>
    {
        [ScaffoldColumn(false)]
        public int JobId { get; set; }
        
        [Display(Name="Компания-работодатель")]
        [Required(ErrorMessage="Поле не должно быть пустым!"), MaxLength(50)]
        [StringLength(50,ErrorMessage="Недопустимая длина ввода!")]
        public virtual string Employer { get; set; }

        [Display(Name="Дата начала работы")]
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        [Column(TypeName = "date")]
        public DateTime DateOfBeginning { get; set; }

        [Display(Name = "Дата конца работы")]
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        [Column(TypeName = "date")]
        public DateTime DateOfEnding { get; set; }

        [Display(Name="Должность")]
        [Required(ErrorMessage = "Поле не должно быть пустым!"), MaxLength(50)]
        [StringLength(50,ErrorMessage="Недопустимая длина ввода!")]
        public string Position { get; set; }

        [Display(Name = "Должностные обязанности")]
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        [DataType(DataType.MultilineText)]
        public string Responsibility { get; set; }

        [Display(Name = "Личные достижения")]
        [DataType(DataType.MultilineText)]
        public string PersonalAchievements { get; set; }

        public virtual CV AssociatedCv { get; set; }

        public JobEntity Transit()
        {
            throw new NotImplementedException();
        }
    }
}
