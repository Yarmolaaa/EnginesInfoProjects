using Common.Repositories.Interfaces;
using EnginesInfo;

namespace EnginesInfo.Repositories.Interfaces
{
    public interface IInfoUnitOfWork : IUnitOfWork
    {
        IRepository<Model> ModelsRepository { get; }
        IRepository<Engine> EnginesRepository { get; }
    }    
}
