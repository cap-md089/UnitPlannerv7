// Program.cs: Starts the main API server
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

using UnitPlanner.Services.Authentication.Protos;
using UnitPlanner.Services.Capwatch.Protos;
using UnitPlanner.Services.Files.Protos;
using UnitPlanner.Services.Graph.Protos;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<AuthGreeter.AuthGreeterClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("SERVICE_AUTH_URL")!);
});
builder.Services.AddGrpcClient<CapwatchGreeter.CapwatchGreeterClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("SERVICE_CAPWATCH_URL")!);
});
builder.Services.AddGrpcClient<FilesGreeter.FilesGreeterClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("SERVICE_FILES_URL")!);
});
builder.Services.AddGrpcClient<GraphGreeter.GraphGreeterClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("SERVICE_GRAPH_URL")!);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
