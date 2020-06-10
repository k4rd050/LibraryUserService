using UserManagementCommand.Model;

namespace UserManagerCommand.Data.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool UpdateUser(User user);
    }
}
