using App.Domain.Entities;

namespace App.Domain.Interfaces.Hubs
{
    public interface IExportHub
    {
        Task SendMessage(string user, string message);
    }
}
