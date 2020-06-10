using Microsoft.Extensions.Logging;
using System;
using UserManagerQuery.Data.Repository;
using UserManagerQuery.ViewModel;

namespace UserManagerQuery.Services
{
    public class UsersQueryService
    {
        private readonly UserRepository _repository;

        public UsersQueryService(UserRepository repository)
        {
            _repository = repository;
        }

        public UserInfoViewModel GetUserInfo(string userId)
        {
            try
            {
                var userInfo = _repository.Get(Convert.ToInt32(userId));

                if (userInfo == null) return null;

                return new UserInfoViewModel
                {
                    Id = userInfo.Id,
                    Reputation = userInfo.Reputation,
                    Status = userInfo.Status
                };
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error getting user info", ex);

                return null;
            }
            
        }
    }
}
