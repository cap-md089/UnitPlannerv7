// GreeterService.cs: An example service to start the development of the project
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

using Grpc.Core;
using UnitPlanner.Services.Graph.Protos;

namespace UnitPlanner.Services.Graph.Services;

public class GreeterService : GraphGreeter.GraphGreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<GraphHelloReply> SayHello(GraphHelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GraphHelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
