// Copyright (C) 2022 Andrew Rioux
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
using Microsoft.AspNetCore.Mvc;

using UnitPlanner.Services.HostConfiguration.Protos;

namespace UnitPlanner.Services.HostConfiguration.Services;

public class HostConfigurationService : Protos.HostConfiguration.HostConfigurationBase
{
    ILogger<HostConfigurationService> _logger;

    public HostConfigurationService(ILogger<HostConfigurationService> logger) =>
        (_logger) = (logger);

    public override Task<HostChangeResponse> AddNewHost(HostChangeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HostChangeResponse
        {
            ErrorMessage = "",
            HasError = false
        });
    }

    public override Task<HostChangeResponse> RemoveHost(HostChangeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HostChangeResponse
        {
            ErrorMessage = "",
            HasError = false
        });
    }
}