using System.Diagnostics;
using System.Text;
using FonTech2.Domain.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FonTech2.Consumer;

public class RabbitMqListener : BackgroundService
{

    private readonly IConnection _connection;
    private readonly IModel _cannel;
    private readonly IOptions<RabbitMqSettings> _options;

    public RabbitMqListener(IOptions<RabbitMqSettings> options)
    {
        _options = options;
        var factory = new ConnectionFactory() { HostName = "localhost" };

        _connection = factory.CreateConnection();
        _cannel = _connection.CreateModel();
        _cannel.QueueDeclare(_options.Value.QueueName , durable: true,
            exclusive:true, autoDelete: false , arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_cannel);
        consumer.Received += (obj, basicDeliver) =>
        {
            var content = Encoding.UTF8.GetString(basicDeliver.Body.ToArray());
            Debug.WriteLine($"Получено сообщение : {content}");
            
            _cannel.BasicAck(basicDeliver.DeliveryTag, false);
        };
        
        _cannel.BasicConsume(_options.Value.QueueName, false,consumer);
        
         Dispose();
         
         return Task.CompletedTask;
         
    }

    public override void Dispose()
    {
        _cannel.Dispose();
        _connection.Dispose();
        base.Dispose();
      
    }
}