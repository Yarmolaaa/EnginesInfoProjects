using System;
using System.Collections.Generic;

namespace EnginesInfo
{
    [Serializable]
    public class DataSet
    {
        public List<Model> Models { get; private set; }
        public List<Engine> Engines { get; private set; }

        public DataSet()
        {
            Models = new List<Model>();
            Engines = new List<Engine>();
        }
    }
}
