using UnitPlanner.Services.HostConfiguration.Protos;

namespace UnitPlanner.Apis.Main.Services.HostConfiguration;

public interface IHostConfigurationService
{

}

public class DevHostConfigurationService : IHostConfigurationService
{

}

public class GrpcHostConfigurationService : IHostConfigurationService
{
    private readonly HostConfigurationService.HostConfigurationServiceClient _hostsClient;

    public GrpcHostConfigurationService(HostConfigurationService.HostConfigurationServiceClient hostsClient) =>
        (_hostsClient) = (hostsClient);
}