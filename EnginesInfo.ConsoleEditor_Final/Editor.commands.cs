using System;
using System.Linq;
using Common.Collection;
using Common.ConsoleIO;



namespace EnginesInfo
{

    public partial class Editor
    {
        public static string format = "{0,40}: ";

        private void OutData()
        {
            OutEnginesData();
            OutModelsData();
        }

        private void OutEnginesData()
        {
            Console.WriteLine("  Список двигунів:");
            Console.WriteLine("    №  Двигун \t\t\t\t Рік випуску");
            foreach (var obj in sortingEngines)
            {
                string modelName = obj.model == null ? ""
                    : obj.model.name;
                int? year = obj.year == null ? (int?)null : obj.year;
                Console.WriteLine("{0,5}) {1,-31} {2,4}", obj.Id, modelName, year);

            }
        }
        private void OutModelsData()
        {
            Console.WriteLine("\n  Список моделей двигунів:");
            foreach (var obj in sortingModels)
            {
                Console.WriteLine("{0,5} {1,-24} кількість циліндрів: {2,2}      об'єм: {3,8} см³      турбонаддув: {4,5}",
                    obj.Id, obj.name, obj.cylinders, obj.objem, obj.turbo ? "Так" : "Ні");
            }
        }

        public void AddModel()
        {
            Model inst = new Model();
            inst.name = Entering.EnterString("Модель двигуна");
            inst.cylinders = Entering.EnterInt32("Кількість циліндрів");
            inst.objem = Entering.EnterInt32("Об'єм");
            inst.turbo = Entering.EnterBool("З турбонаддувом");
            if (dataContext.Models.Count == 0) { inst.Id = 1; }
            else
            {
                inst.Id = dataContext.Models.Select(e => e.Id).Max() + 1;
            }
            dataContext.Models.Add(inst);
        }

        public void AddModel(string name)
        {
            Model inst = new Model();
            inst.name = name;
            inst.cylinders = Entering.EnterInt32("Кількість циліндрів");
            inst.objem = Entering.EnterInt32("Об'єм");
            inst.turbo = Entering.EnterBool("З турбонаддувом");
            if (dataContext.Models.Count == 0) { inst.Id = 1; }
            else
            {
                inst.Id = dataContext.Models.Select(e => e.Id).Max() + 1;
            }
            dataContext.Models.Add(inst);
        }

        public void EditModel()
        {
            if (dataContext.Models.Count == 0)
            {
                Console.WriteLine(format, "Потрібно створити дані");
                Console.Write("\nДля продовження роботи програми "
                + "натисніть довільну клавішу...");
                Console.ReadKey(true);
            }
            else
            {
                int id = Entering.EnterInt32("\t\tВведіть Id поля, яке потрібно редагувати");
                Model inst = dataContext.Models.FirstOrDefault(e => e.Id == id);
                inst.name = Entering.EnterString("Модель двигуна");
                inst.cylinders = Entering.EnterInt32("Кількість циліндрів");
                inst.objem = Entering.EnterInt32("Об'єм");
                inst.turbo = Entering.EnterBool("З турбонаддувом");
                Console.WriteLine(format, "Редагування завершено");
                Console.Write("\nДля продовження роботи програми "
                + "натисніть довільну клавішу...");
                Console.ReadKey(true);
            }

        }

        public void AddEngine()
        {
            Engine inst = new Engine();
            if (dataContext.Models.Count == 0)
            {
                Console.WriteLine("\t\tДля того, щоб вести дані про двигун - треба вести дані про модель:");
                AddModel();
                Console.WriteLine("\t\tмодель додано:");
            }
            if (dataContext.Engines.Count == 0) { inst.Id = 1; }
            else
            {
                inst.Id = dataContext.Engines.Select(e => e.Id).Max() + 1;
            }
            inst.model = SelectModel();
            inst.year = Entering.EnterInt32("Рік випуску");
            inst.Id = dataContext.Engines.Select(e => e.Id).Max() + 1;
            dataContext.Engines.Add(inst);
        }

        public void EditEngine()
        {
            if (dataContext.Engines.Count == 0)
            {
                Console.WriteLine(format, "Потрібно спершу створити дані!");
                Console.Write("\nДля продовження роботи програми "
                + "натисніть довільну клавішу...");
                Console.ReadKey(true);
            }
            else
            {
                int id = Entering.EnterInt32("\t\tВведіть Id поля, яке потрібно редагувати");
                Engine inst = dataContext.Engines.FirstOrDefault(e => e.Id == id);
                inst.model = SelectModel();
                inst.year = Entering.EnterInt32("Рік випуску");
                inst.Id = dataContext.Engines.Select(e => e.Id).Max() + 1;
                Console.WriteLine(format, "Редагування завершено");
                Console.Write("\nДля продовження роботи програми "
                + "натисніть довільну клавішу...");
                Console.ReadKey(true);
            }
        }

        private Model SelectModel ()
        {

            string modelName = Entering.EnterString("Модель");
            try
            {
                return dataContext.Models.First(e => e.name == modelName);
            }
            catch (System.InvalidOperationException)
            {
                AddModel(modelName);
                return dataContext.Models.First(e => e.name == modelName);
            }
        }

        public void ShowAllData()
        {
            Console.WriteLine("\nІнформація про ПО \"Облік двигунів\"");
            Console.WriteLine("Представлено:\n"
                + "\tДані про двигуни: {0,-30}\n"
                + "\tДані моделі двигунів: {1,-30}",
                dataContext.Engines.Count,
                dataContext.Models.Count
                );
        }


        public void RemoveModel()
        {
            int id = Entering.EnterInt32("Введіть числовий ідентифікатор моделі");
            Model inst = dataContext.Models.FirstOrDefault(e => e.Id == id);
            dataContext.Models.Remove(inst);
        }

        public void RemoveEngine()
        {
            int id = Entering.EnterInt32("Введіть числовий ідентифікатор двигуна");
            Engine inst = dataContext.Engines.FirstOrDefault(e => e.Id == id);
            dataContext.Engines.Remove(inst);
        }


        private void SortModelsByName()
        {
            sortingModels = sortingModels.OrderBy(e => e.name);
        }

        private void SortEnginesByName()
        {
            sortingEngines = sortingEngines.OrderBy(e => e.model?.name);
        }

        private void SortEnginesByModel()
        {
            sortingEngines = sortingEngines.OrderBy(e => e.model.name);
        }

        private void SortModelsByCylinders()
        {
            sortingModels = sortingModels.OrderBy(e => e.cylinders);
        }

        private void SortModelsByObjem()
        {
            sortingModels = sortingModels.OrderBy(e => e.objem);
        }
        private void ShowModelsInfo()
        {
            int id = Entering.EnterInt32(
                "Введіть Id моделі двигуна",
                1, dataContext.Models.Count);
            var collection = sortingModels.Where(e => e.Id == id);
            Console.Write(collection.ToLineList(
                "\nДетальна інформація про модель двигуна"));
            KeyPressWaiting();
        }
        private void ShowEnginesInfo()
        {
            int id = Entering.EnterInt32(
                "Введіть Id двигуна",
                1, dataContext.Engines.Count);
            var collection = sortingEngines.Where(e => e.Id == id);
            Console.Write(collection.ToLineList(
                "\nДетальна інформація про двигун"));
            KeyPressWaiting();
        }
    }
}
