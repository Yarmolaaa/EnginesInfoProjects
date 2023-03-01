using Common.ConsoleIO;
using System;
using System.Collections.Generic;


namespace EnginesInfo
{

    public partial class Editor
    {

        private DataContext dataContext;

        IEnumerable<Model> sortingModels;
        IEnumerable<Engine> sortingEngines;

        public Editor(DataContext dataContext)
        {
            this.dataContext = dataContext;
            sortingModels = dataContext.Models;
            sortingEngines = dataContext.Engines;
            IniCommandsInfo();
        }

        private CommandInfo[] commandsInfo;
        
        private void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("вийти", null),
                new CommandInfo("створити тестові дані",  dataContext.CreateTestingData),
                new CommandInfo("зберегти дані",  dataContext.Save),
                new CommandInfo("завантажити дані", dataContext.Load),
                new CommandInfo("видалити усі записи...", dataContext.Clear),
                new CommandInfo("додати запис про модель...", AddModel),
                new CommandInfo("додати запис про двигун...", AddEngine),
                new CommandInfo("видалити запис про модель...", RemoveModel),
                new CommandInfo("видалити запис про двигун...", RemoveEngine),
                new CommandInfo("відредагувати запис про двигун...", EditEngine),
                new CommandInfo("відредагувати запис про модель...", EditModel),
                new CommandInfo("Детальна інформація про модель двигуна...", ShowModelsInfo),
                new CommandInfo("Детальна інформація про двигун...", ShowEnginesInfo),
                new CommandInfo("сортувати моделі за назвою", SortModelsByName),
                new CommandInfo("сортувати двигуни за назвою", SortEnginesByName),
                new CommandInfo("сортувати двигуни за назвою моделі", SortEnginesByModel),
                new CommandInfo("сортувати моделі за к-стю циліндрів", SortModelsByCylinders),
                new CommandInfo("сортувати моделі за об'ємом", SortModelsByObjem),

           };
        }

        public void Run()
        {
            dataContext.Load();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Реалізація класів для предметної області");
                OutData();               
                ShowAllData();
                ShowCommandsMenu();
                Command command = EnterCommand();
                if (command == null)
                {
                    return;
                }
                command();
            }
        }

        private void ShowCommandsMenu()
        {
            Console.WriteLine("  Список команд меню:");
            for (int i = 0; i < commandsInfo.Length; i++)
            {
                Console.WriteLine("\t{0,2} - {1}",
                    i, commandsInfo[i].name);
            }
        }

        private Command EnterCommand()
        {
            Console.WriteLine();
            int number = Entering.EnterInt32(
                "Введіть номер команди меню");
            return commandsInfo[number].command;
        }
        protected virtual void KeyPressWaiting()
        {
            Console.Write("\nДля продовження роботи програми "
                + "натисніть довільну клавішу...");
            Console.ReadKey(true);
        }

    }
}
