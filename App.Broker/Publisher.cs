using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace App.Broker
{
    public class Publisher
    {
        private readonly ConnectionFactory? _factory = null;
        private readonly IConnection? _connection = null;
        public Publisher()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "10.0.0.107",
                VirtualHost = "/",
                UserName = "admin",
                Password = "test"
            };

            _connection = _factory.CreateConnection();
        }

        public async Task Sender(string queue, string message)
        {
            await Task.Run(() =>
            {
                using (var channel = _connection?.CreateModel())
                {
                    channel?.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);
                }
            });
        }

    }
}
