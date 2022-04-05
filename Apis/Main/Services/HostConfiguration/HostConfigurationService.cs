using UnitPlanner.Services.HostConfiguration.Protos;

using GrpcClient = UnitPlanner.Services.HostConfiguration.Protos.HostConfiguration.HostConfigurationClient;

namespace UnitPlanner.Apis.Main.Services.HostConfiguration;

public interface IHostConfigurationService
{
    Task UpdateHosts(string accountID, IEnumerable<string> hosts);

    Task RemoveHost(string accountID);
}

public class GrpcHostConfigurationService : IHostConfigurationService
{
    private readonly GrpcClient _hostsClient;
    private readonly ILogger<GrpcHostConfigurationService> _logger;

    public GrpcHostConfigurationService(GrpcClient hostsClient, ILogger<GrpcHostConfigurationService> logger) =>
        (_hostsClient, _logger) = (hostsClient, logger);

    public async Task UpdateHosts(string accountID, IEnumerable<string> hosts)
    {
        _logger.LogInformation($"Used URL: {Environment.GetEnvironmentVariable("SERVICE_HOSTCONFIGURATION_URL")!}");

        var request = new HostChangeRequest
        {
            AccountID = accountID
        };
        request.Hosts.AddRange(hosts);

        var result = await _hostsClient.SetHostsAsync(request);

        if (result.HasError)
        {
            throw new HostConfigurationException(result.ErrorMessage);
        }
    }

    public async Task RemoveHost(string accountID)
    {
        var result = await _hostsClient.RemoveHostAsync(new HostRemoveRequest
        {
            AccountID = accountID,
        });

        if (result.HasError)
        {
            throw new HostConfigurationException(result.ErrorMessage);
        }
    }
}

public class HostConfigurationException : Exception
{
    internal HostConfigurationException (string message)
        : base(message)
    {
    }
}