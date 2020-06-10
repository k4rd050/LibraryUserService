using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagerQuery.Services;

namespace UserManagerQuery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersQueryService _service;

        public UsersController(UsersQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/user/info/{id}", Name = "InfoUserRoute")]
        public async Task<ActionResult> GetUserInfoAsync(string id)
        {
            var userInfo = _service.GetUserInfo(id);

            return new JsonResult(userInfo);
        }
    }
}
