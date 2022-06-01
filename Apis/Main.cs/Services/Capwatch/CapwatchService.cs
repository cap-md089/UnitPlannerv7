using UnitPlanner.Services.Capwatch.Protos;

namespace UnitPlanner.Apis.Main.Services.Capwatch;

public interface ICapwatchService
{

}

public class DevCapwatchService : ICapwatchService
{

}

public class GrpcCapwatchService : ICapwatchService
{
    private readonly CapwatchImport.CapwatchImportClient _capwatchClient;

    public GrpcCapwatchService(CapwatchImport.CapwatchImportClient capwatchClient) =>
        (_capwatchClient) = (capwatchClient);
}