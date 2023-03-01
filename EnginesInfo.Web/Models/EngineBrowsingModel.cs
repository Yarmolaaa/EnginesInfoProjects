using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace EnginesInfo.Web.Models
{
    public class EngineBrowsingModel
    {

        public int Id { get; set; }

        [Display(Name = "Назва моделі")]
        public string engineModel { get; set; }

        [Display(Name = "Рік випуску")]
        public int? Year { get; set; }




        public static explicit operator EngineBrowsingModel(Engine obj)
        {
            return new EngineBrowsingModel()
            {
                Id = obj.Id,
                engineModel = obj.model.name,
                Year = obj.year,

            };
        }

    }
}