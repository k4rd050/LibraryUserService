using UserManagementCommand.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagerCommand.Broker.Subscriber;

namespace UserManagementCommand.Broker.Subscriber
{
    public class UserReputationChangedSubscriber : GenericSubscriber
    {
        private const string _queueName = "userReputationQueue";

        public UserReputationChangedSubscriber(UsersCommandsService utentesService) : base(utentesService, _queueName) { }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine(" [x] Received User Reputation Update Request {0}", message);

                _utentesService.UpdateUserReputation(message);

                Console.WriteLine(" [x] Done");

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
