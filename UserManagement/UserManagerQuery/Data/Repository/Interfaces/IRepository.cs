namespace UserManagerQuery.Data.Repository.Interfaces
{
    public interface IRepository<TModel>
    {
        TModel Get(int id);
    }
}
