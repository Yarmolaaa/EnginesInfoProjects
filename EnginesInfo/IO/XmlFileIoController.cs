
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using EnginesInfo.IO.Interfaces;

namespace EnginesInfo
{
    public class XmlFileIoController : IFileIoController
    {

        public string FileExtension { get; set; }

        public XmlFileIoController() {
            FileExtension = ".xml";
        }

        public void Save(DataSet dataSet, string fileName)
        {
            fileName = Path.ChangeExtension(fileName, FileExtension);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create(fileName, settings);
                WriteData(dataSet, writer);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        void WriteData(DataSet dataSet, XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("EnginesInfo");
            WriteModels(dataSet.Models, writer);
            WriteEngines(dataSet.Engines, writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        void WriteModels(IEnumerable<Model> collection, XmlWriter writer)
        {
            writer.WriteStartElement("ModelsData");
            foreach (var inst in collection)
            {
                writer.WriteStartElement("Model");
                writer.WriteElementString("Id", inst.Id.ToString());
                writer.WriteElementString("Name", inst.name);
                int engineId = inst.engine == null ? 0 : inst.engine.Id;
                writer.WriteElementString("EngineId", engineId.ToString());
                writer.WriteElementString("Cylinders", inst.cylinders.ToString());
                writer.WriteElementString("Objem", inst.objem.ToString());
                writer.WriteElementString("Turbo", inst.turbo.ToString().ToLower());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteEngines(IEnumerable<Engine> collection, XmlWriter writer)
        {
            writer.WriteStartElement("EnginesData");
            foreach (var inst in collection)
            {
                writer.WriteStartElement("Engine");
                writer.WriteElementString("Id", inst.Id.ToString());
                int modelId = inst.model == null ? 0 : inst.model.Id;
                writer.WriteElementString("ModelId", modelId.ToString());
                writer.WriteElementString("Year", inst.year.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }


        //-----------------------------------------------------------

        public void Load(DataSet dataSet, string fileName)
        {
            if (!File.Exists(fileName)) return;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            using (XmlReader reader = XmlReader.Create(fileName, settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "Engine":
                                ReadEngine(reader, dataSet);
                                break;
                            case "Model":
                                ReadModel(reader, dataSet);
                                break;
                        }
                    }
                }
            }
        }

        void ReadEngine(XmlReader reader, DataSet dataSet )
        {
            reader.ReadStartElement("Engine");
            int id = reader.ReadElementContentAsInt();
            int modelId = reader.ReadElementContentAsInt();
            Model model  = dataSet.Models
                .FirstOrDefault(e => e.Id == modelId);
            int year  = reader.ReadElementContentAsInt();
            Engine inst = new Engine(model, year) { Id = id };
            dataSet.Engines.Add(inst);
        }

        void ReadModel(XmlReader reader, DataSet dataSet)
        {
            Model inst = new Model();
            reader.ReadStartElement("Model");
            inst.Id = reader.ReadElementContentAsInt();
            inst.name = reader.ReadElementContentAsString();
            int engineId = reader.ReadElementContentAsInt();
            inst.engine = dataSet.Engines
                .FirstOrDefault(e => e.Id == engineId);       
            string s1 = reader.ReadElementContentAsString();
            inst.cylinders = string.IsNullOrEmpty(s1) ? (int?)null : int.Parse(s1);
            string s2 = reader.ReadElementContentAsString();
            inst.objem =string.IsNullOrEmpty(s2) ? (int?)null : int.Parse(s2);
            inst.turbo = reader.ReadElementContentAsBoolean();
            dataSet.Models.Add(inst);

        }

    }
}
