using Common.Repositories.Interfaces;
using EnginesInfo.Repositories.Interfaces;
using EnginesInfo.Web.Models;
using EnginesInfo.Web.Models.Extensions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnginesInfo.Web.Controllers
{
    public class EnginesCrudController : Controller
    {
        [Inject]
        public IInfoUnitOfWork UoW { get; set; }
        private IRepository<Engine> repository => UoW.EnginesRepository;

        //public CountriesCrudController(IInfoUnitOfWork uow) {
        //    UoW = uow;
        //}

        // GET: CountriesCrud
        public ActionResult Index()
        {
            IEnumerable<EngineBrowsingModel> browsingModelObjects =
                repository.GetAll()
                .Select(e => (EngineBrowsingModel)e).OrderBy(e => e.engineModel);
            //return View();
            return View(browsingModelObjects);
        }

        public ActionResult Details(int id)
        {
            EngineEditingModel model =
                (EngineEditingModel)repository.GetById(id);
            return View(model);
        }

        public ViewResult Create()
        {
            ViewBag.Models = UoW.ToModelsSelectList();
            return View(new EngineEditingModel());
        }

        [HttpPost]
        public ActionResult Create(EngineEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            UoW.AddEngine(model);
            UoW.Save();
            TempData["message"] = string.Format(
                "Дані двигуна \"{0}\" збережено", model.Models);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EngineEditingModel model =
                (EngineEditingModel)repository.GetById(id);            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EngineEditingModel model)
        {
 
            UoW.UpdateEngine(model);
            UoW.Save();
            TempData["message"] = string.Format(
                "Зміни даних двигуна \"{0}\" збережено", model.Models);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            EngineEditingModel model =
                (EngineEditingModel)repository.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Engine model)
        {
            repository.Delete(repository.GetById(model.Id));
            UoW.Save();
            return RedirectToAction("Index");
        }



    }
}