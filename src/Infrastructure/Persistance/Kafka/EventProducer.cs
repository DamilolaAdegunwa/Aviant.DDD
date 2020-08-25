namespace Aviant.DDD.Infrastructure.Persistance.Kafka
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Application.EventBus;
    using Confluent.Kafka;
    using Domain.Aggregates;
    using Domain.EventBus;
    using Microsoft.Extensions.Logging;

    public class EventProducer<TAggregateRoot, TKey> : IDisposable, IEventProducer<TAggregateRoot, TKey>
        where TAggregateRoot : IAggregateRoot<TKey>
    {
        private IProducer<TKey, string> _producer;
        private readonly string _topicName;
        private readonly ILogger<EventProducer<TAggregateRoot, TKey>> _logger;

        public EventProducer(
            string topicBaseName,
            string kafkaConnString,
            ILogger<EventProducer<TAggregateRoot, TKey>> logger)
        {
            _logger = logger;

            var aggregateType = typeof(TAggregateRoot);

            _topicName = $"{topicBaseName}-{aggregateType.Name}";

            var producerConfig = new ProducerConfig {BootstrapServers = kafkaConnString};
            var producerBuilder = new ProducerBuilder<TKey, string>(producerConfig);
            producerBuilder.SetKeySerializer(new KeySerializer<TKey>());
            _producer = producerBuilder.Build();
        }

        public async Task DispatchAsync(TAggregateRoot aggregateRoot)
        {
            if (null == aggregateRoot)
                throw new ArgumentNullException(nameof(aggregateRoot));

            if (!aggregateRoot.Events.Any())
                return;

            _logger.LogInformation(
                "publishing " + aggregateRoot.Events.Count + " events for {AggregateId} ...",
                aggregateRoot.Id);

            foreach (var @event in aggregateRoot.Events)
            {
                var eventType = @event.GetType();

                var serialized = System.Text.Json.JsonSerializer.Serialize(@event, eventType);

                var headers = new Headers
                {
                    {"aggregate", Encoding.UTF8.GetBytes(@event.AggregateId.ToString())},
                    {"type", Encoding.UTF8.GetBytes(eventType.AssemblyQualifiedName)}
                };

                var message = new Message<TKey, string>()
                {
                    Key = @event.AggregateId,
                    Value = serialized,
                    Headers = headers
                };

                await _producer.ProduceAsync(_topicName, message);
            }

            aggregateRoot.ClearEvents();
        }

        public void Dispose()
        {
            _producer?.Dispose();
            _producer = null;
        }
    }
}