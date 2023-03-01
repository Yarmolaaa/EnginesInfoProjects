using Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnginesInfo.IO.Interfaces;

namespace EnginesInfo.IO
{
    public class BinarySerializationAdapter : IFileIoController
    {
        GenericBinarySerializationController<DataSet> fileIoController 
            = new GenericBinarySerializationController<DataSet>();

        public string FileExtension
        {
            get { return fileIoController.FileExtension; }
            set { fileIoController.FileExtension = value;}
        }

        public BinarySerializationAdapter()
        {
            FileExtension = ".eibd";
        }

        public void Load(DataSet dataSet, string fileName)
        {
            //throw new NotImplementedException();
            DataSet newDataSet = fileIoController.Load(fileName);
            foreach(Model el in newDataSet.Models)
            {
                dataSet.Models.Add(el);
            }
            foreach (Engine el in newDataSet.Engines)
            {
                dataSet.Engines.Add(el);
            }
        }

        public void Save(DataSet dataSet, string fileName)
        {
            //throw new NotImplementedException();
            fileIoController.Save(dataSet, fileName);
        }
    }
}
