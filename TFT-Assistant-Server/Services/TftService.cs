using Dapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Tft;

namespace TFT_Assistant_Server.Services;

public class TftService : TftAssistant.TftAssistantBase
{
    public override Task<GetAllOriginResponse> GetAllOrigin(Empty request, ServerCallContext context)
    {
        var origins = Program.Connection
            .Query<Origin>("SELECT originClassId as 'id', name, description FROM OriginClass where isOrigin = 1")
            .ToList();
        return Task.FromResult(new GetAllOriginResponse()
        {
            OriginList = {origins}
        });
    }

    public override Task<GetAllClassResponse> GetAllClass(Empty request, ServerCallContext context)
    {
        var classes = Program.Connection
            .Query<Class>("SELECT originClassId as 'id', name, description FROM OriginClass where isOrigin = 0")
            .ToList();
        return Task.FromResult(new GetAllClassResponse()
        {
            ClassList = {classes}
        });
    }
}