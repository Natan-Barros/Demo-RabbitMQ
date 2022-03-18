using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost"};
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    //Para declarar a Fila
    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    //Escrever  a mensagem e converter em bytes
    string message = "Hello World!";
    var body = Encoding.UTF8.GetBytes(message);


    //Publicar a mensagem
    channel.BasicPublish(exchange: "",
                         routingKey: "hello",
                         basicProperties: null,
                         body: body);

    Console.WriteLine(" [x] Sent {0}", message);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();