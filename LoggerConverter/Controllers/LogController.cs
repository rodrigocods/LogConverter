using LoggerConverter.Dtos;
using LoggerConverter.Models;
using LoggerConverter.Repository.Interfaces;
using LoggerConverter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LoggerConverter.Controllers
{
    [ApiController]
    [Route("log")]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _logRepository;
        private readonly ILogService _logService;
        private readonly ILogConvertedRepository _logConvertedRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogController(ILogRepository logRepository, ILogService logService, ILogConvertedRepository logConvertedRepository, 
            IUnitOfWork unitOfWork)
        {
            _logRepository = logRepository;
            _logService = logService;
            _logConvertedRepository = logConvertedRepository;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("convert")]
        public async Task<IActionResult> ConvertLog([FromBody]ConvertLogRequest body)
        {
            body.Validate();

            Log log = null;
            string logContent = "";

            if (body.IdLog != null && body.IdLog != default)
            {
                log = await _logRepository.Find(body.IdLog.Value);
                logContent = await System.IO.File.ReadAllTextAsync(log.Path);
            }
            else
            {
                logContent = await _logService.DownloadFileContent(body.LogFileUrl);
            }

            if (log.IsConverted && body.SaveOnDatabase) throw new ArgumentException($"Esse log já foi convertido.");

            var logConvertedContent = _logService.ConvertLog(logContent);

            if (body.SaveOnDatabase)
            {

                if (log == null)
                {
                    var logFilePath = await _logService.SaveLog(logContent);

                    log = new Log()
                    {
                        IsConverted = false,
                        Path = logFilePath
                    };

                    await _logRepository.Add(log);
                }

                log.IsConverted = true;

                var logConvertedFilePath = await _logService.SaveLogConverted(logConvertedContent);

                var logConverted = new LogConverted
                {
                    Path = logConvertedFilePath,
                    IdLog = log.Id
                };

                await _logConvertedRepository.Add(logConverted);

                await _unitOfWork.Commit();

                return Ok(logConverted);
            }
            else
            {
                return File(Encoding.UTF8.GetBytes(logConvertedContent), "text/plain");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int page = 1, [FromQuery]int pageSize = 10)
        {
            return Ok(await _logRepository.GetAll(page, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find([FromRoute] int id)
        {
            return Ok(await _logRepository.Find(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LogAddDto body)
        {
            var oldFileContent = await _logService.DownloadFileContent(body.LogFileUrl);

            var logFilePath = await _logService.SaveLog(oldFileContent);

            var log = new Log()
            {
                IsConverted = false,
                Path = logFilePath
            };

            await _logRepository.Add(log);

            await _unitOfWork.Commit();

            return Ok(new { id = log.Id });
        }
    }
}