using App.Broker;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Repository;
using App.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portifolio_queue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportController(ILogger<Queue> logger, IQueueRepository queueRepository, IFileRepository fileRepository) : ControllerBase
    {

        private readonly ILogger<Queue> _logger = logger;
        private readonly IQueueRepository _repository = queueRepository;
        private readonly IFileRepository _fileRepository = fileRepository;


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var fileEntity = new App.Domain.Entities.File
            {
                Id = Guid.NewGuid(),
                Name = file.FileName
            };
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileEntity.Content = ms.ToArray();

                await _fileRepository.CreateAsync(fileEntity);
            }


            return Ok("File uploaded successfully: " + fileEntity.Id);
        }
    }

}

