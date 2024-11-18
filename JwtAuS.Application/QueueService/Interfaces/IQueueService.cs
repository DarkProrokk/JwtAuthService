namespace JwtAuS.Application.QueueService.Interfaces;

public interface IQueueService
{
    void SendMessage(string message, string queueName);
}