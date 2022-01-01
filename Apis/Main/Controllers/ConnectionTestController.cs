// ConnectionTestController.cs: Used to test gRPC connections to different services
// 
// Copyright (C) 2021 Andrew Rioux
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using Microsoft.AspNetCore.Mvc;

using UnitPlanner.Services.Authentication.Protos;
using UnitPlanner.Services.Capwatch.Protos;
using UnitPlanner.Services.Files.Protos;
using UnitPlanner.Services.Graph.Protos;

namespace UnitPlanner.Apis.Main.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ConnectionTestController : ControllerBase
{
    private readonly AuthGreeter.AuthGreeterClient _authClient;
    private readonly CapwatchGreeter.CapwatchGreeterClient _capwatchClient;
    private readonly FilesGreeter.FilesGreeterClient _filesClient;
    private readonly GraphGreeter.GraphGreeterClient _graphClient;

    public ConnectionTestController(AuthGreeter.AuthGreeterClient authClient, CapwatchGreeter.CapwatchGreeterClient capwatchClient, FilesGreeter.FilesGreeterClient filesClient, GraphGreeter.GraphGreeterClient graphClient) =>
        (_authClient, _capwatchClient, _filesClient, _graphClient) = (authClient, capwatchClient, filesClient, graphClient);

    [HttpGet(Name = "TestAuthConnection")]
    public async Task<bool> Get(int clientId)
    {
        try
        {
            switch (clientId)
            {
                case 0 :
                    await _authClient.SayHelloAsync(new AuthHelloRequest());
                    return true;
                
                case 1 :
                    await _capwatchClient.SayHelloAsync(new CapwatchHelloRequest());
                    return true;
                
                case 2 :
                    await _filesClient.SayHelloAsync(new FilesHelloRequest());
                    return true;
                
                case 3 :
                    await _graphClient.SayHelloAsync(new GraphHelloRequest());
                    return true;
            }
            
            return false;
        }
        catch
        {
            return false;
        }
    }
}