using UnitPlanner.Services.Graph.Protos;

namespace UnitPlanner.Apis.Main.Services.Graph;

public interface IGraphService
{

}

public class DevGraphService : IGraphService
{

}

public class GrpcGraphService : IGraphService
{
    private readonly GraphService.GraphServiceClient _graphClient;

    public GrpcGraphService(GraphService.GraphServiceClient graphClient) =>
        (_graphClient) = (graphClient);
}