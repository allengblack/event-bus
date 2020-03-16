using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace event_bus.Services
{
     public class Subscriber<T>
    {
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }

        public void Register()
        {
            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(body));
            };

            channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
        }

        public void Deregister()
        {
            connection.Close();
        }

        public Subscriber()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            Console.WriteLine("Event bus Subscriber initialised");
        }
    }
}