using LoggerConverter.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoggerConverter.Controllers
{
    [ApiController]
    [Route("log/converted")]
    public class LogConvertedController : ControllerBase
    {
        private readonly ILogConvertedRepository _logConvertedRepository;

        public LogConvertedController(ILogConvertedRepository logConvertedRepository)
        {
            _logConvertedRepository = logConvertedRepository;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _logConvertedRepository.GetDashboard());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find([FromRoute] int id)
        {
            return Ok(await _logConvertedRepository.Find(id));
        }
    }
}