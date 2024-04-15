using System.Net.Http.Json;
using System.Text;
using FonTech2.Producer.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FonTech2.Producer;

public class Producer : IMessageProducer
{
    public void SandMessage<T>(T message, string routingKey, string? exchange = default)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var json = JsonConvert.SerializeObject(message, Formatting.Indented,
            new JsonSerializerSettings()
            {
               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
        );

        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish(exchange, routingKey,body:body);

    }
}