using System.Threading.Tasks;
using UserManagementCommand.Broker.Publisher;
using UserManagementCommand.Model;
using UserManagementCommand.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserManagementCommand.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly UsersCommandsService _service;

        public UsersController(UsersCommandsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/utente/estado/{id}", Name = "EstadoUtenteRoute")]
        public async Task<ActionResult> GetEstadoAsync(string id)
        {
            return new JsonResult(await Task.FromResult(StatusEnum.Active));
        }

        [HttpGet]
        [Route("/utente/create", Name = "CreateUtenteRoute")]
        public async Task<ActionResult> CreateUtenteAsync()
        {
            return new JsonResult(await Task.FromResult(_service.CreateUser()));
        }

        [HttpGet]
        [Route("/utente/changeStatus/{estado}", Name = "ChangeUtenteStatusRoute")]
        public async Task<ActionResult> ChangeUtenteStatusAsync(string estado)
        {
            Publisher.Publish("teste", estado);
            return Ok();
        }

    }
}
