using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserManagerQuery.Services;

namespace UserManagerQuery.Broker.Subscriber
{
    public abstract class GenericSubscriber : BackgroundService
    {
        internal readonly UsersQueryService _utentesService;

        internal IConnection _connection;
        internal IModel _channel;

        internal GenericSubscriber(UsersQueryService utentesService, string queueName)
        {
            _utentesService = utentesService;
            InitializeRabbitMqListener(queueName);
        }

        private void InitializeRabbitMqListener(string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        internal void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        internal void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        internal void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        internal void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        internal void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
