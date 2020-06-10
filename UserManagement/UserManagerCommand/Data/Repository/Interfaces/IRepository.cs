using System.Collections.Generic;

namespace UserManagerCommand.Data.Repository.Interfaces
{
    public interface IRepository<TModel>
    {
        TModel Get(int id);
        bool Add(TModel entity);
        bool Remove(TModel entity);
    }
}
