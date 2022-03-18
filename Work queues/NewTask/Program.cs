using RabbitMQ.Client;
using System.Text;

var message = GetMessage(Console.ReadLine());
var body = Encoding.UTF8.GetBytes(message);

var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672"),
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "task_queue",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var propeties = channel.CreateBasicProperties();
propeties.Persistent = true;


channel.BasicPublish(exchange: "", routingKey: "task_queue", basicProperties: propeties, body: body);

static string GetMessage(string args)
{
    return ((args.Length > 0) ? string.Join(string.Empty, args) : "Hello World");
}