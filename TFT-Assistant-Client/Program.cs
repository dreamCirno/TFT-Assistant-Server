﻿using Greet;
using Grpc.Core;

var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

var client = new Greeter.GreeterClient(channel);
string user = "world";

var reply = client.SayHello(new HelloRequest() {Name = user});
Console.WriteLine("Greeting: " + reply.Message);

channel.ShutdownAsync().Wait();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();