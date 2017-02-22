using RabbitMQ.Client;

namespace MqConsumer.RabbitMq
{
    public class MqHelper
    {
        private static IConnection _connection;

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <returns></returns>
        public static IConnection GetConnection()
        {
            if (_connection != null) return _connection;
            _connection = GetNewConnection();
            return _connection;
        }

        public static IConnection GetNewConnection()
        {

            //从工厂中拿到实例 本地host、用户admin
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin",

            };

            _connection = factory.CreateConnection();
            return _connection;

        }
    }
}