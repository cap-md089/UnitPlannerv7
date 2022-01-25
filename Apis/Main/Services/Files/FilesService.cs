using UnitPlanner.Services.Files.Protos;

namespace UnitPlanner.Apis.Main.Services.Files;

public interface IFilesService
{

}

public class DevFilesService : IFilesService
{

}

public class GrpcFilesService : IFilesService
{
    private readonly FilesService.FilesServiceClient _filesClient;

    public GrpcFilesService(FilesService.FilesServiceClient filesClient) =>
        (_filesClient) = (filesClient);
}