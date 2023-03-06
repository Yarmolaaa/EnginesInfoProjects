using EnginesInfo.Repositories.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnginesInfo.Web.Controllers
{
    public class NavigationController : Controller
    {
        [Inject]
        public IInfoUnitOfWork UoW { get; set; }

        private IEnumerable<Engine> engines => UoW.EnginesRepository.GetAll();

        //// GET: Category
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [ChildActionOnly]
        public PartialViewResult UsedModelsMenu(
                string categoryName = RouteConfig.ALL_VALUES)
        {
            ViewBag.SelectedCategoryName = categoryName;
            List<string> categoryNames = new List<string>();
            categoryNames.Add(RouteConfig.ALL_VALUES);
            categoryNames.AddRange(engines
                    .Select(e => e.model.name)
                    .Distinct().OrderBy(e => e));
            return PartialView(categoryNames);
        }
    }
}