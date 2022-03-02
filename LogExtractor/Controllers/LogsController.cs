using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogExtractor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private LogFiles _logFiles = new LogFiles();

        public LogsController(ILogger<HealthController> logger, IOptions<List<string>> LogFiles)
        {
            _logger = logger;
            _logFiles.FileList = StringListToLogFileList(LogFiles.Value);
        }

        [HttpGet]
        public List<LogFile> Get()
        {
            return _logFiles.FileList;
        }

        [HttpGet ("download")]
        public IActionResult GetAllFiles()
        {
            try
            {
                var (fileType, archiveData, archiveName) = _logFiles.DownloadFiles();

                return File(archiveData, fileType, archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("download/{id}")]
        public IActionResult GetFilesByID(int id)
        {
            try
            {
                var (archiveData, archiveName) = _logFiles.DownloadFile(id);

                return File(archiveData, "text / plain");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET /logs/download/{id} - Download specific file by id
        //Instead of using full file path an id(list order) will be used

        private List<LogFile> StringListToLogFileList(List<string> FilesPaths)
        {
            List<LogFile> list = new List<LogFile>();
            for (int i = 0; i < FilesPaths.Count; ++i)
            {
                list.Add(new LogFile()
                {
                    Id = i,
                    FilePath = FilesPaths[i]
                });
            }
            return list;
        }
    }
}
