using EnginesInfo.Repositories.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EnginesInfo.Web.Controllers
{
    public class EnginesController : Controller
    {

        [Inject]
        public IInfoUnitOfWork UoW { get; set; }

        IEnumerable<Engine> objects => UoW.EnginesRepository.GetAll();

        // GET: Engines
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult ObjectsInfo()
        {
            return View(objects);
        }

        public ViewResult EnginesByModelsInfo(
                string categoryName = RouteConfig.ALL_VALUES)
        {
            IEnumerable<Engine> models = objects.OrderBy(e => e.model);
            if (!string.IsNullOrEmpty(categoryName) &&
                categoryName != RouteConfig.ALL_VALUES)
            {
                models = models
                    .Where(e => e.model.name == categoryName);
            }
            ViewBag.SelectedCategoryName = categoryName;
            return View(models);
        }
    }
}