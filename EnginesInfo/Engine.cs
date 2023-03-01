using Common.Entities;
using System;


namespace EnginesInfo
{
    [Serializable]
    public class Engine : Entity
    {


        public Model model { get; set; }
        public int? year { get; set; }

        public Engine(Model model,
                    int? year)
        {
            this.model = model;
            this.year = year;
        }
        public Engine(Model model )
            : this(model, null) { }

        public Engine() { }
        public override string ToString()
        {
            return string.Format(
                "{0,7},назва моделі:{1}  рік випуску: {2} ",
                Id, model == null ? "" : model.name, year);
        }
        

    }

}