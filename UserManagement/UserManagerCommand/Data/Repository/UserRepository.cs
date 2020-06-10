using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using UserManagementCommand.Model;
using UserManagerCommand.Data.Repository.Interfaces;

namespace UserManagerCommand.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbContext DatabaseContext;
        private readonly ILogger _logger;

        public UserRepository(ILogger<UserRepository> logger, DbContext context)
        {
            this.DatabaseContext = context;
            _logger = logger;
        }

        public bool Add(User user)
        {
            try
            {
                var res = DatabaseContext.Set<User>().Add(user);
                return res.State == EntityState.Added;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public User Get(int id)
        {
            try
            {
                return DatabaseContext.Set<User>().Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                return null;
            }
        }

        public bool Remove(User user)
        {
            try
            {
                var res = DatabaseContext.Set<User>().Remove(user);
                return res.State == EntityState.Deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var res = DatabaseContext.Update<User>(user);
                return res.State == EntityState.Modified;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return false;
            }
        }
    }
}
