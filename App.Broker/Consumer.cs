using App.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace App.Broker
{
    public class Consumer
    {
        private readonly ConnectionFactory? _factory = null;
        private readonly IConnection? _connection = null;
        public Consumer()
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

        public void Consume(string queue)
        {
            using var channel = _connection?.CreateModel();
            channel?.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var tcs = new TaskCompletionSource<Queue>();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Mensagem recebida!" + message);
                var data = JsonSerializer.Deserialize<Queue>(message);
            };


         //   while (true)
           // {
                channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
            
           // }
        }
 
      

    }
}
