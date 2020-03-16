using System.Text;
using RabbitMQ.Client;

namespace event_bus.Services
{
    public class Publisher
    {
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }

        public Publisher()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void Publish<T>(T model)
        {
            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var byteArray = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: byteArray);
        }
    }
}