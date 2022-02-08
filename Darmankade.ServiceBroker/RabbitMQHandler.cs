using Darmankade.ServiceBroker.ViewModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Darmankade.ServiceBroker
{
    public class RabbitMQHandler
    {
        private static string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly Lazy<ConnectionFactory> lazy = new(() => new ConnectionFactory());
        private static RabbitMQConfiguration Configuration
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<RabbitMQConfiguration>(File.ReadAllText(path + @"\rabbitmqsettings.json"));

            }
        }

        public static IConnection GetConnection()
        {
            ConnectionFactory connectionFactory = lazy.Value;
            connectionFactory.HostName = Configuration.HostName;
            connectionFactory.UserName = Configuration.UserName;
            connectionFactory.Password = Configuration.Password;
            connectionFactory.Port = Protocols.DefaultProtocol.DefaultPort;
            return connectionFactory.CreateConnection();
        }
        public static void Send(string queue, string data)
        {
            using IConnection connection = GetConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue, false, false, false, null);
            channel.BasicPublish(string.Empty, queue, null, Encoding.UTF8.GetBytes(data));
        }

        public static string Receive(string queue)
        {
            using IConnection connection = GetConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue, false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            BasicGetResult result = channel.BasicGet(queue, true);
            if (result != null)
            {
                string data =
                Encoding.UTF8.GetString(result.Body.ToArray());
                return data;
            }
            else
            {
                return "";
            }
        }

        public static void ReceiveEvent(string queue, EventHandler<BasicDeliverEventArgs> consumerReceived)
        {
            IConnection connection = GetConnection();
            IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue, false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += consumerReceived;
            channel.BasicConsume(
                queue: queue,
                autoAck: false,
                consumer: consumer);
        }

    }
}
