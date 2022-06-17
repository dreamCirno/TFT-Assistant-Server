using Greet;
using Grpc.Core;

class GreeterService : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply {Message = "Hello " + request.Name});
    }
}

class Program
{
    public static void Main(string[] args)
    {
        var server = new Server
        {
            Services = {Greeter.BindService(new GreeterService())},
            Ports = {new ServerPort("localhost", 50051, ServerCredentials.Insecure)}
        };
        server.Start();
        Console.WriteLine("Server listening on port 50051");
        Console.WriteLine("Press any key to stop the server...");
        Console.WriteLine("(type ctrl+c to exit)");
        Console.ReadKey();
        server.ShutdownAsync().Wait();
    }
}