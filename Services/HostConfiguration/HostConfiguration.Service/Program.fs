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

namespace UnitPlanner.Services.HostConfiguration.Service

#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Server.Kestrel.Core;
open Microsoft.AspNetCore.Hosting

open UnitPlanner.Services.HostConfiguration.Service.Services

module Program =
    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddGrpc()
        builder.WebHost.ConfigureKestrel (fun so ->
            so.ListenAnyIP (5050, fun options ->
                options.Protocols <- HttpProtocols.Http2))

        let app = builder.Build()

        app.MapGrpcService<HostConfigurationService>()

        app.Run()

        0
