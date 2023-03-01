using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnginesInfo.IO.Interfaces;

namespace EnginesInfo
{
    public class DataIO : IFileIoController
    {
        public string FileExtension { get; } = ".txt";

        public void Load(DataSet dataSet, string fileName)
        {
            if (!File.Exists(fileName))
                return;
            using (StreamReader sr = new StreamReader(fileName, Encoding.Unicode))
            {
                while (sr.Peek() >= 0)
                {
                    string line = GetValue(sr.ReadLine());
                    if (line == nameof(Engine))
                    {
                        dataSet.Engines.Add(ReadEngines(dataSet, sr));
                    }
                    else if (line == nameof(Model))
                    {
                        dataSet.Models.Add(ReadModels(sr));
                    }
                }

            }

        }
        public void Save(DataSet dataSet, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            foreach (Model model in dataSet.Models)
            {
                string recordString = CreateObjectString(model);
                File.AppendAllText(fileName, recordString, Encoding.Unicode);
            }
            foreach (Engine engine in dataSet.Engines)
            {
                string recordString = CreateObjectString(engine);
                File.AppendAllText(fileName, recordString, Encoding.Unicode);
            }
        }
        
        private string CreateObjectString(Engine engine)
        {
            return string.Format("Type: Engine\n Id: {0}\n  Назва моделі: {1}\n  Рік випуску: {2}\n",
                engine.Id, engine.model.name, engine.year);
        }
        private string CreateObjectString(Model model)
        {
            return string.Format("Type: Model\n Id: {0}\n Назва двигуна: {1}\n К-сть циліндрів: {2}\n Об'єм: {3}\n Турбонаддув: {4}\n",
                model.Id, model.name, model.cylinders, model.objem, model.turbo );

        }
        public Engine ReadEngines(DataSet dataSet, StreamReader sr)
        {
            Engine engine = new Engine();
            string s = sr.ReadLine();
            engine.Id = Convert.ToInt32(GetValue(s));
            s = GetValue(sr.ReadLine());
            engine.model = dataSet.Models.FirstOrDefault(e => e.name == s);
            s = GetValue(sr.ReadLine());
            engine.year = string.IsNullOrEmpty(s) ? (int?)null : int.Parse(s);
            return engine;
        }
        public Model ReadModels(StreamReader sr)
        {
            Model model = new Model();
            model.Id = Convert.ToInt32(GetValue(sr.ReadLine()));
            model.name = GetValue(sr.ReadLine());
            string s = GetValue(sr.ReadLine());
            model.cylinders = string.IsNullOrEmpty(s) ? (int?)null : int.Parse(s);
            s = GetValue(sr.ReadLine());
            model.objem = string.IsNullOrEmpty(s) ? (int?)null : int.Parse(s);
            s = GetValue(sr.ReadLine());
            model.turbo = Convert.ToBoolean(s);
            return model;
        }
        private string GetValue(string s)
        {
            int pos = s.IndexOf(":");
            return s.Substring(pos + 1).Trim();
        }
    }
}
