using App.Broker;
using App.Domain.Entities;
using App.Domain.Interfaces.Hubs;
using App.Domain.Interfaces.Repositories;
using App.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portifolio_queue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExportController(ILogger<Queue> logger, IQueueRepository queueRepository, IFileRepository fileRepository, IHubContext<ExportHub> hub) : ControllerBase
    {

        private readonly ILogger<Queue> _logger = logger;
        private readonly IQueueRepository _repository = queueRepository;
        private readonly IFileRepository _fileRepository = fileRepository;
        private readonly IHubContext<ExportHub> _hub = hub;

        [HttpPost]
        public IActionResult Post([FromBody] Queue data)
        {
            var publisher = new Publisher();
            var entity = data;
            entity.Id = Guid.NewGuid();
            _ = publisher.Sender("export", JsonSerializer.Serialize(data));
            return Ok($" Exportação enviada para fila: {entity.Id}");
        }


        [HttpPost("Download")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            // TODO: update when finish the upload/download tests
            var file = _fileRepository.Find(Guid.Parse("8a84ad17-7de0-4f61-983d-c1decc762a15"));

            if (file == null)
            {
                return NotFound();
            }
            await Task.Run(() => _hub.Clients.All.SendAsync("ReceiveMessage", "Download", "Finalizado"));
            return File(file.Content, "application/pdf", file.Name);
        }


    }
}
