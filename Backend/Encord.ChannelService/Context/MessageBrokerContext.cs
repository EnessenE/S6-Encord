using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encord.ChannelService.Handlers;
using Encord.Common.Configuration;
using Encord.Common.MessageBrokerModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Encord.ChannelService.Context
{
    public class MessageBrokerContext
    {
        private ILogger<MessageBrokerContext> _logger;
        private IConnection _savedConnection;
        private IConnection _connection => GetConnection();
        private readonly ConnectionFactory _factory;
        private readonly MessageBrokerSettings _brokerSettings;
        private MessageHandler _handler;

        public MessageBrokerContext(ILogger<MessageBrokerContext> logger, IOptions<MessageBrokerSettings> brokerSettings, MessageHandler handler)
        {
            _logger = logger;
            _handler = handler;
            _brokerSettings = brokerSettings.Value;
            _factory = new ConnectionFactory() { HostName = _brokerSettings.Host, UserName = _brokerSettings.Username, Password = _brokerSettings.Password};
            ReceiveMessages("Channel");
        }

        public IConnection GetConnection()
        {
            if (_savedConnection != null && _savedConnection.IsOpen)
            {
                return _savedConnection;
            }
            _logger.LogWarning("Creating a new connection to the Content broker");
            _savedConnection = _factory.CreateConnection();
            return _savedConnection;
        }

        private void ReceiveMessages(string exchange)
        {
            var channel = GetConnection().CreateModel();

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                exchange: exchange,
                routingKey: "");

            _logger.LogInformation("Starting to listen to exchange: {ex}", exchange);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventargs) =>
            {
                var body = eventargs.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                _logger.LogDebug("Message received!: {Content}", message);
                var item = JsonConvert.DeserializeObject<ChannelMessage>(message);
                _handler.HandleMessage(item);
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }


        /// <summary>
        /// Send a Content to the Content broker
        /// </summary>
        /// <param Name="exchange">The exchange/queue you want to throw the Content into</param>
        /// <param Name="data">The object that has to be converted to JSON and sent to the queue</param>
        public void CreateMessage(string exchange, object data)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                channel.BasicPublish(exchange: exchange,
                    routingKey: "",
                    basicProperties: null,
                    body: body);
                _logger.LogInformation("Sending Content to exchange {exchange} with body: {body}", exchange, body);
            }
        }
    }
}
