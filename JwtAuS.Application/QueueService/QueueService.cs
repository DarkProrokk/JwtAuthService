using System.Text;
using System.Text.Json;
using JwtAuS.Application.QueueService.Interfaces;
using RabbitMQ.Client;

namespace JwtAuS.Application.QueueService;

public class QueueService(ConnectionFactory connectionFactory): IQueueService
{
    public void SendMessage(string message, string queueName)
    {
        using var connection = connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
            arguments: null);
        
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }
}