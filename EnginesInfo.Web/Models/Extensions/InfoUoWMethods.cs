using EnginesInfo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnginesInfo.Web.Models.Extensions
{
    public static class InfoUoWMethods
    {
      
        public static void UpdateEngine(this IInfoUnitOfWork uow,
            EngineEditingModel model){
            Engine engine = uow.EnginesRepository.GetById(model.Id);
            UpdateEngine(uow, ref engine, model);
        }
        private static void UpdateEngine(IInfoUnitOfWork uow,
                ref Engine engine, EngineEditingModel model)
        {
            engine.model = model.Models;
            engine.year = model.Year;

        }

        public static void AddEngine(this IInfoUnitOfWork uow,
            EngineEditingModel model)
        {
            Engine engine = new Engine();
            UpdateEngine(uow, ref engine, model);
            uow.EnginesRepository.Add(engine);
        }


    }
}