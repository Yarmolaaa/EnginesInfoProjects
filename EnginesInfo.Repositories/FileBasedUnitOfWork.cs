using Common.Repositories;
using Common.Repositories.Interfaces;
using EnginesInfo.Repositories.Interfaces;
using System;
using EnginesInfo;

namespace EnginesInfo.Repositories
{
    public class FileBasedUnitOfWork : UnitOfWork, IInfoUnitOfWork
    {
       protected readonly DataContext dataContext;

        public FileBasedUnitOfWork(DataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException();
            }
            this.dataContext = dataContext;
            EnginesRepository = new Repository<Engine>(
                dataContext.Engines);
        }

        public IRepository<Engine> EnginesRepository { get; }
        
        private IRepository<Model> modelsRepository;

        public IRepository<Model> ModelsRepository {

            get
            {
                if (modelsRepository == null)
                {
                    modelsRepository = new Repository<Model>(dataContext.Models);
                }
                return modelsRepository;
            }
        }
       

        public override void Load()
        {
            dataContext.Load();
        }

        public override void Save()
        {
            dataContext.Save();
        }

        protected override void DoDispose()
        {
            //throw new NotImplementedException();
        }
    }
}
