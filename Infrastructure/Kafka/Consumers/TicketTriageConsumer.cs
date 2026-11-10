using AI_Powered_Ticketing_and_Support_System_Backend.Events;
using Confluent.Kafka;
using System.Text.Json;
using static AI_Powered_Ticketing_and_Support_System_Backend.Infrastructure.Kafka.Consumers.TicketTriageConsumer;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Infrastructure.Kafka.Consumers
{
    public class TicketTriageConsumer : BackgroundService
    {

        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TicketTriageConsumer> _logger;

        public TicketTriageConsumer(
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory,
            ILogger<TicketTriageConsumer> logger)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                GroupId = "ai-service-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer =
                new ConsumerBuilder<string, string>(config).Build();

            consumer.Subscribe("ticket-classified");

            _logger.LogInformation("Kafka consumer started.");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(stoppingToken);

                    var ticketEvent =
                        JsonSerializer.Deserialize<TicketClassifiedEvent>(
                            result.Message.Value);

                    if (ticketEvent is null)
                        continue;

                    // Create a new DI scope for processing
                    using var scope = _scopeFactory.CreateScope();

                    var processor = scope.ServiceProvider
                        .GetRequiredService<ITicketTriageResultHandler>();

                    await processor.HandleAsync(
                        ticketEvent,
                        stoppingToken);

                    consumer.Commit(result);

                    _logger.LogInformation(
                        "Ticket {TicketId} processed successfully.",
                        ticketEvent.TicketId);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka consumer stopping.");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
