namespace FonTech2.Producer.Interfaces;

public interface IMessageProducer
{
    void SandMessage<T>(T message, string routingKey , string? exchange = default);
}