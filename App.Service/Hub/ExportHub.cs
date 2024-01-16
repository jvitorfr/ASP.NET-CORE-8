using App.Domain.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace App.Service
{
    public class ExportHub : Hub, IExportHub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}