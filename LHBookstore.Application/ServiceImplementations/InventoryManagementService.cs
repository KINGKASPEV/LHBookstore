//using LHBookstore.Application.Interfaces.Services;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;

//namespace LHBookstore.Application.ServiceImplementations
//{
//    public class InventoryManagementService : BackgroundService
//    {
//        private readonly IConnectionProvider _connectionProvider;
//        private readonly ILogger<InventoryManagementService> _logger;

//        public InventoryManagementService(IConnectionProvider connectionProvider, ILogger<InventoryManagementService> logger)
//        {
//            _connectionProvider = connectionProvider;
//            _logger = logger;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            try
//            {
//                var connection = _connectionProvider.GetConnection();
//                var channel = connection.CreateModel();

//                channel.QueueDeclare(queue: "books", durable: true, exclusive: true, autoDelete: false, arguments: null);

//                var consumer = new EventingBasicConsumer(channel);
//                consumer.Received += async (model, ea) =>
//                {
//                    try
//                    {
//                        var body = ea.Body;
//                        var message = Encoding.UTF8.GetString(body.ToArray());

//                        // Process the order message and update inventory
//                        await ProcessOrderMessageAsync(message);

//                        channel.BasicAck(ea.DeliveryTag, multiple: false);
//                    }
//                    catch (Exception ex)
//                    {
//                        // Log or handle the exception as needed
//                        _logger.LogError($"Error processing order message: {ex.Message}");
//                        channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
//                    }
//                };

//                channel.BasicConsume(queue: "books", autoAck: false, consumer: consumer);

//                await Task.Delay(Timeout.Infinite, stoppingToken);
//            }
//            catch (Exception ex)
//            {
//                // Log or handle the exception as needed
//                _logger.LogError($"Error in ExecuteAsync: {ex.Message}");
//            }
//        }

//        private async Task ProcessOrderMessageAsync(string message)
//        {
//            // Your logic to process the order message and update inventory
//            await Task.Delay(1000); // Simulating processing time
//            _logger.LogError($"Order message processed: {message}");
//        }
//    }
//}
