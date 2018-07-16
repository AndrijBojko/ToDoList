using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace ToDoList.RabbitMQ
{
    public class EventBus : IEventBus//, IDisposable
    {
        const string _brokerName = "eshop_event_bus";
        private string _connectionString = "";

        bool _disposed;

        public EventBus()
        {
        }


        public void Publish(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name;
            var factory = new ConnectionFactory() { HostName = _connectionString };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _brokerName, type: "direct");
                string message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: _brokerName,
                    routingKey: eventName,
                    basicProperties: null,
                    body: body);
            }
        }
        public void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent
        {
            //_subsManager.RemoveSubscription<T, TH>();
        }

        //public void Subscribe<T, TH>()
        //where T : IntegrationEvent
        //where TH : IIntegrationEventHandler<T>
        //{
        //    var eventName = _subsManager.GetEventKey<T>();

        //    var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
        //    if (!containsKey)
        //    {
        //        if (!_persistentConnection.IsConnected)
        //        {
        //            _persistentConnection.TryConnect();
        //        }

        //        using (var channel = _persistentConnection.CreateModel())
        //        {
        //            channel.QueueBind(queue: _queueName, exchange: _brokerName, routingKey: eventName);
        //        }
        //    }

        //    _subsManager.AddSubscription<T, TH>();
        //}

        //public void Unsubscribe<T, TH>()
        //    where TH : IIntegrationEventHandler<T>
        //    where T : IntegrationEvent
        //{
        //    _subsManager.RemoveSubscription<T, TH>();
        //}

        //public void Dispose()
        //{
        //    if (_disposed) return;

        //    _disposed = true;

        //    try
        //    {
        //        _connection.Dispose();
        //    }
        //    catch (IOException ex)
        //    {
        //        _logger.LogCritical(ex.ToString());
        //    }
        //}
    }
}
