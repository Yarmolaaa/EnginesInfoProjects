using Common.Collection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnginesInfo.IO.Interfaces;



namespace EnginesInfo
{
    public class DataContext
    {
        readonly DataSet dataSet = new DataSet();
        public ICollection<Engine> Engines
        {
            get { return dataSet.Engines; }
        }

        public ICollection<Model> Models
        {
            get { return dataSet.Models; }
        }

        //XmlFileIoController xmlFileIoController = new XmlFileIoController();
        private IFileIoController fileIoController;

        public IFileIoController FileIoController
        {
            get
            {
                return fileIoController;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException();
                }
                fileIoController = value;
            }
        }

        public static string FileName = "EnginesInfo";

        public DataContext(IFileIoController fileIoController)
        {
            FileIoController = fileIoController;
        }

        private string directory = "";

        public string Directory
        {
            get { return directory; }
            set
            {
                directory = value ?? "";
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
            }
        }

        public string FilePath {
            get
            {
                return Path.Combine( Directory,
                    FileName + FileIoController.FileExtension);
            } 
        }
         
        public void Save()
        {
            fileIoController.Save(dataSet, FilePath);
        }

        public void Load()
        {
            fileIoController.Load(dataSet, FilePath);
        }

        public override string ToString()
        {
            return string.Concat("Інформація про об'єкти ПО \"Двигуни\"\n",
                Models.ToLineList("Модель"),
                Engines.ToLineList("Рік випуску"));
        }

        public void Clear()
        {
            Models.Clear();
            Engines.Clear();
        }

        public void CreateTestingData()
        {
            CreateModels();
            CreateEngines();
        }

        private void CreateEngines()
        {
            Engines.Add(new Engine(Models.First(e => e.name == "Mercedes-Benz OM642"), 2005) { Id = 1 });
            Engines.Add(new Engine(Models.First(e => e.name == "AUDI CAEB"), 2008) { Id = 2 });
            Engines.Add(new Engine(Models.First(e => e.name == "HYUNDAI G6DG"), 2011) { Id = 3 });
        }

        public void CreateModels()
        {
            Models.Add(new Model("Mercedes-Benz OM642",6, 2987,true)
            {
                Id = 1,
            });
            Models.Add(new Model("AUDI CAEB", null, 1984,false)
            {
                Id = 2,
            });
            Models.Add(new Model("MITSUBISHI 4G18",4, 1584, false)
            {
                Id = 3,
            });
            Models.Add(new Model("HYUNDAI G6DG",6, null, true)

            {
                Id = 4,
            });
        }
    }
}
