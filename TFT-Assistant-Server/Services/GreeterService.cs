using Greet;
using Grpc.Core;

namespace TFT_Assistant_Server.Services;

class GreeterService : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply {Message = "Hello " + request.Name});
    }
}