using System.Diagnostics;

using UnitPlanner.Services.HostConfiguration.Protos;

using GrpcClient = UnitPlanner.Services.HostConfiguration.Protos.HostConfiguration.HostConfigurationClient;

namespace UnitPlanner.Apis.Main.Services.HostConfiguration;

public interface IHostConfigurationService
{
    Task AddNewHost(string accountID, string baseUrl, string hostName);

    Task RemoveHost(string accountID, string baseUrl, string hostName);
}

public class GrpcHostConfigurationService : IHostConfigurationService
{
    private readonly GrpcClient _hostsClient;
    private readonly ILogger<GrpcHostConfigurationService> _logger;

    public GrpcHostConfigurationService(GrpcClient hostsClient, ILogger<GrpcHostConfigurationService> logger) =>
        (_hostsClient, _logger) = (hostsClient, logger);

    public async Task AddNewHost(string accountID, string baseUrl, string hostName)
    {
        _logger.LogInformation($"Used URL: {Environment.GetEnvironmentVariable("SERVICE_HOSTCONFIGURATION_URL")!}");

        var result = await _hostsClient.AddNewHostAsync(new HostChangeRequest
        {
            AccountID = accountID,
            BaseUrl = baseUrl,
            Host = hostName
        });

        if (result.HasError)
        {
            throw new HostConfigurationException(result.ErrorMessage);
        }
    }

    public async Task RemoveHost(string accountID, string baseUrl, string hostName)
    {
        var result = await _hostsClient.RemoveHostAsync(new HostChangeRequest
        {
            AccountID = accountID,
            BaseUrl = baseUrl,
            Host = hostName
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