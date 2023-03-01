using System;
using Common.Entities;

namespace EnginesInfo
{
    [Serializable]
    public class Model : Entity
    {
        public Engine engine;


        public string name { get; set; }
        public int? cylinders { get; set; }
        public int? objem { get; set; }
        public bool turbo { get; set; }
       

        public Model(string name, int? cylinders, int? objem, bool turbo )
        {
            this.name = name;
            this.cylinders = cylinders;
            this.objem = objem;
            this.turbo = turbo;
        }
        public Model() { }
        public Model(string name, int? objem, bool turbo) 
          : this ( name, null, objem, turbo) { }

        public override string ToString()
        {
            return string.Format("{0} , Назва двигуна:{1}, к-сть циліндрів:{2},об'єм{3},турбонаддув:{4} ", Id, name, cylinders,objem,turbo);
        }
       
    }
}
