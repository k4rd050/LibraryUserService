using UserManagementCommand.Broker.Publisher;
using UserManagementCommand.Model;
using System;
using UserManagerCommand.Data.Repository.Interfaces;

namespace UserManagementCommand.Services
{
    public class UsersCommandsService
    {
        private IUserRepository _repository;

        public UsersCommandsService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User CreateUser()
        {
            var newUser = new User { Status = StatusEnum.Active };
            _repository.Add(newUser);
            
            return newUser;
        }

        public bool SuspendUser(string userId)
        {
            var user = _repository.Get(userId);
            user.Status = StatusEnum.Suspended;

            _repository.UpdateUser(user);
            
            Publisher.Publish("userStatusQueue", userId);
            
            return true;
        }

        public bool DisableUser(string userId)
        {
            Publisher.Publish("userStatusQueue", userId);
            return true;
        }

        public bool UpdateUserReputation(object userMessage)
        {
            try
            {

            }
            catch
            {

            }

            var user = new User(); //  TODO GET USER
            Publisher.Publish("userStatusQueue", new { userId, value });
            return true;
        }
    }
}
