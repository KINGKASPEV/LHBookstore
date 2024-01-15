//using LHBookstore.Application.Interfaces.Services;
//using RabbitMQ.Client;

//namespace LHBookstore.Application.ServiceImplementations
//{
//    public class ConnectionProvider : IConnectionProvider
//    {
//        private readonly IConnection _connection;

//        public ConnectionProvider()
//        {
//            var factory = new ConnectionFactory()
//            { 
//                HostName = "localhost",
//                UserName = "guest",
//                Password = "password@123"
//            };
//            _connection = factory.CreateConnection();
//        }

//        public IConnection GetConnection()
//        {
//            return _connection;
//        }
//    }
//}
