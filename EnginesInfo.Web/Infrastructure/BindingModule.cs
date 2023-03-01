using EnginesInfo.IO.Interfaces;
using EnginesInfo.Repositories;
using EnginesInfo.Repositories.Interfaces;
using Ninject.Modules;
using System.Web;
using System.Web.Hosting;

namespace EnginesInfo.Web.Infrastructure
{
    public class BindingModule : NinjectModule 
    {
        public override void Load()
        {
            //throw new NotImplementedException();
            string virtualPath = "~" + HttpContext.Current
                .Application["dataFilesPath"] as string;
            string path = HostingEnvironment.MapPath(virtualPath);
            Bind<IFileIoController>().To<XmlFileIoController>().InSingletonScope();
            Bind<DataContext>().ToSelf().InSingletonScope()
                .WithPropertyValue("Directory", path);
            Bind<IInfoUnitOfWork>().To<FileBasedUnitOfWork>().InSingletonScope();
        }
    }
}