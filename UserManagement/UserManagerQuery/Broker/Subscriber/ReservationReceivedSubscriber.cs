using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagerQuery.Services;

namespace UserManagerQuery.Broker.Subscriber
{
    public class ReservationReceivedSubscriber : GenericSubscriber
    {
        private const string _queueName = "userReputationQueue";

        public ReservationReceivedSubscriber(UsersQueryService utentesService) : base(utentesService, _queueName) { }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine(" [x] Received User Reputation Update Request {0}", message);

                _utentesService.GetUserInfo(message);

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
