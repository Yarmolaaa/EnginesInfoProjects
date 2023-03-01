using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnginesInfo.Web.Models
{
    public class EngineEditingModel
    {

        public int Id { get; set; }

        [Display(Name = "Назва моделі")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Назва моделі\'")]
        [StringLength(50, MinimumLength = 3,
             ErrorMessage = "Назва моделі двигуна "
             + "повинна містити від 3 до 50 символів")]
        public Model Models { get; set; }

        [Display(Name = "Рік випуску")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Рік випуску\'")]
        [StringLength(4, MinimumLength = 4,
             ErrorMessage = "Рік випуску "
             + "повинна містити 4 символа")]
        public int? Year { get; set; }



        public static explicit operator EngineEditingModel(Engine obj)
        {
            return new EngineEditingModel()
            {
                Id = obj.Id,
                Models = obj.model,
                Year = obj.year,
                
            };
        }
    }
}