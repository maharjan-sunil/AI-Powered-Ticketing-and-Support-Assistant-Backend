using AI_Powered_Ticketing_and_Support_System_Backend.Events;
using AI_Powered_Ticketing_and_Support_System_Backend.Infrastructure.Kafka;
using AI_Powered_Ticketing_and_Support_System_Backend.Infrastructure.Kafka.Producers;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Infrastructure.Kafka.Producers
{
    public class TicketCreatedProducer : ITicketCreatedProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly KafkaSettings _settings;

        public TicketCreatedProducer(
            IOptions<KafkaSettings> options)
        {
            _settings = options.Value;

            var config = new ProducerConfig
            {
                BootstrapServers = _settings.BootstrapServers,
                Acks = Acks.All
            };

            _producer = new ProducerBuilder<string, string>(config)
                .Build();
        }

        public async Task PublishAsync(
            TicketCreatedEvent ticketEvent,
            CancellationToken cancellationToken = default)
        {
                Console.WriteLine("Publishing ticket event to Kafka...");
            var message = JsonSerializer.Serialize(ticketEvent);

            var kafkaMessage = new Message<string, string>
            {
                Key = ticketEvent.TicketId.ToString(),
                Value = message
            };

            var result = await _producer.ProduceAsync(
                _settings.TicketCreatedTopic,
                kafkaMessage,
                cancellationToken);
            Console.WriteLine($"Kafka event published: {result.TopicPartitionOffset}");
        }
    }
}