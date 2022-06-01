using UnitPlanner.Services.Authentication.Protos;

namespace UnitPlanner.Apis.Main.Services.Authentication;

public interface IAuthenticationService
{

}

public class DevAuthenticationService : IAuthenticationService
{

}

public class GrpcAuthenticationService : IAuthenticationService
{
    private readonly AuthenticationService.AuthenticationServiceClient _authClient;

    public GrpcAuthenticationService(AuthenticationService.AuthenticationServiceClient authClient) =>
        (_authClient) = (authClient);
}