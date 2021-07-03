using Logic.RoundRobin;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Types;

namespace WebApi.Controllers
{
    [Route("schedule/round-robin")]
    [ApiController]
    public class RoundRobinScheduleController : ControllerBase
    {
        private readonly IRoundRobinScheduleGenerator generator;

        public RoundRobinScheduleController(IRoundRobinScheduleGenerator generator)
        {
            this.generator = generator;
        }

        [EnableCors]
        [HttpPost]
        [HttpOptions]
        public async Task<IActionResult> GenerateSchedule(GenerateScheduleRequest request)
        {
            return Ok(await generator.GenerateSchedule(request.ParticipantCount, request.RoundCount));
        }

        [EnableCors]
        [HttpPost]
        [HttpOptions]
        [Route("validate")]
        public async Task<IActionResult> ValidateGenerateScheduleRequest(GenerateScheduleRequest request)
        {
            return Ok(await generator.ValidateGenerateScheduleRequest(request.ParticipantCount, request.RoundCount));
        }
    }
}
