using System.Diagnostics;

using UnitPlanner.Services.HostConfiguration.Protos;

namespace UnitPlanner.Apis.Main.Services.HostConfiguration;

public interface IHostConfigurationService
{
    Task AddNewHost(string accountID, string baseUrl, string hostName);

    Task RemoveHost(string accountID, string baseUrl, string hostName);
}

public class DevHostConfigurationService : IHostConfigurationService
{
    public async Task AddNewHost(string accountID, string baseUrl, string hostName)
    {
        {
            var tcs = new TaskCompletionSource();

            var process = new Process
            {
                StartInfo = { FileName = "/usr/bin/dev-add-host", Arguments = $"\"127.0.0.1\t{hostName}.localunitplanner.org\t\t# {hostName}.{baseUrl}\"" },
                EnableRaisingEvents = true
            };

            process.Exited += (sender, args) =>
            {
                Console.WriteLine("process exited");
                tcs.SetResult();
                process.Dispose();
            };

            process.Start();

            await tcs.Task;
        }
    }

    public Task RemoveHost(string accountID, string baseUrl, string hostName) => Task.CompletedTask;
}

public class GrpcHostConfigurationService : IHostConfigurationService
{
    private readonly HostConfigurationService.HostConfigurationServiceClient _hostsClient;

    public GrpcHostConfigurationService(HostConfigurationService.HostConfigurationServiceClient hostsClient) =>
        (_hostsClient) = (hostsClient);

    public async Task AddNewHost(string accountID, string baseUrl, string hostName)
    {
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