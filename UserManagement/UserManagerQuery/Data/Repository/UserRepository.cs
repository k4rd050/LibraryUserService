using System;
using UserManagerQuery.Model;
using UserManagerQuery.Data.Repository.Interfaces;

namespace UserManagerQuery.Data.Repository
{
    public class UserRepository : IRepository<User>
    {
        protected readonly UsersContext DatabaseContext;

        public UserRepository(UsersContext context)
        {
            this.DatabaseContext = context;
        }

        public User Get(int id)
        {
            try
            {
                return DatabaseContext.Set<User>().Find(id);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                
                return null;
            }
        }
    }
}
