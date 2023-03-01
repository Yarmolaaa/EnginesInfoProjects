using EnginesInfo.Repositories.Interfaces;
using Ninject;
using System.Collections.Generic;
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
    }
}