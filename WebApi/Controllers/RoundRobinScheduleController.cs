using Logic.RoundRobin;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet]
        [Route("max-rounds")]
        public async Task<IActionResult> GetMaxRounds([FromQuery] int participantCount)
        {
            return Ok(await generator.GetMaxRounds(participantCount));
        }

        [EnableCors]
        [HttpGet]
        public async Task<IActionResult> GenerateSchedule([FromQuery] int participantCount, [FromQuery] int roundCount)
        {
            return Ok(await generator.GenerateSchedule(participantCount, roundCount));
        }

        [EnableCors]
        [HttpGet]
        [Route("validate")]
        public async Task<IActionResult> ValidateGenerateScheduleRequest([FromQuery] int participantCount, [FromQuery] int roundCount)
        {
            return Ok(await generator.ValidateGenerateScheduleRequest(participantCount, roundCount));
        }
    }
}
