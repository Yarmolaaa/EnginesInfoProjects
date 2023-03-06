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
        public string Models { get; set; }

        [Display(Name = "Рік випуску")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Рік випуску\'")]
        [Range(1895, 2022, ErrorMessage = "Рік випуску "
            + "повинен бути в межах від 1895 до 2022")]
        public int? Year { get; set; }



        public static explicit operator EngineEditingModel(Engine obj)
        {
            return new EngineEditingModel()
            {
                Id = obj.Id,
                Models = obj.model.name,
                Year = obj.year,
                
            };
        }
    }
}