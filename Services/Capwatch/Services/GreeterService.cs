// GreeterService.cs: An example gRPC service to act as a bootstrap
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
using UnitPlanner.Services.Capwatch.Protos;

namespace UnitPlanner.Services.Capwatch.Services;

public class GreeterService : CapwatchGreeter.CapwatchGreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<CapwatchHelloReply> SayHello(CapwatchHelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new CapwatchHelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
