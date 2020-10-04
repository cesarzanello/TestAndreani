using ApiGeo.geodecodificador.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGeo.geodecodificador.Core
{
    public class HostedServiceRabbit : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IServiceProvider _serviceCollection;
        private readonly ILogger _logger;
        private IConnection _connection;

        private IModel _channel;

        private string _queueName;
        IGeocoder _geo = new Geocoder();
        public HostedServiceRabbit(ILoggerFactory loggerFactory,
            IServiceProvider serviceCollection) {
            _serviceCollection = serviceCollection;
            _logger = loggerFactory.CreateLogger<HostedServiceRabbit>();
            ConsumirEvento();
        }
        public void ConsumirEvento() {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = _channel.QueueDeclare(queue: "mensajeQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var a = new ReciveMessages();
            //a.ReciveMessege();  
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var adress = JsonSerializer.Deserialize<Adress>(message);
                    _geo.GeocoderAdress(adress);
                };
            _channel.BasicConsume(_queueName, false, consumer);
            return Task.CompletedTask;
        }

        
    }
}
