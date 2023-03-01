using System;
using Common.ConsoleIO;
using System.IO;
using EnginesInfo;
using Common.Entities;

namespace EnginesInfo.ConsoleEditor
{
    class Program
    {
        static DataContext dataContext;
        static Editor editor;
        static string directory = "files";
        static XmlFileIoController xmlFileIoController = new XmlFileIoController();
        static DataIO txtFileIoContriller = new DataIO();
        

        static void Main(string[] args)
        {
            Console.Title = "EnginesInfo.ConsoleEditor (Ярмолюк О.Ю.)";
            Settings.SetConsoleParam();
            Console.WriteLine("Реалізація класів для предметної області");
            //dataContext = new DataContext(xmlFileIoController)
             dataContext = new DataContext(txtFileIoContriller)
            {
                Directory = directory
            };
            editor = new Editor(dataContext);
            editor.Run();
                
            Console.ReadKey(true);
        }

    }
}
