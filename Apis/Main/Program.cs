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

using Microsoft.EntityFrameworkCore;

using UnitPlanner.Apis.Main.Data;
using UnitPlanner.Apis.Main.Models;
using UnitPlanner.Apis.Main.Services;
using UnitPlanner.Apis.Main.Services.Authentication;
using UnitPlanner.Apis.Main.Services.Capwatch;
using UnitPlanner.Apis.Main.Services.Files;
using UnitPlanner.Apis.Main.Services.Graph;
using UnitPlanner.Apis.Main.Services.HostConfiguration;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

if (Environment.GetEnvironmentVariable("RUNNING_IN_CI") == "1")
{
    Console.Error.WriteLine($"Using connection string: {builder.Configuration.GetConnectionString("MainDB")}");
}

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddEnvironmentVariables();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UnitPlannerDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("MainDB"), new MySqlServerVersion(new Version(8, 0, 26)));
    
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
    }
});

builder.Services.RegisterAuthenticationService(builder.Environment);
builder.Services.RegisterCapwatchService(builder.Environment);
builder.Services.RegisterFilesService(builder.Environment);
builder.Services.RegisterGraphService(builder.Environment);
builder.Services.RegisterHostConfigurationService(builder.Environment);

builder.Services.AddTransient<IAccountsService, AccountsService>();
builder.Services.AddTransient<IEventsService, EventsService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<UnitPlannerDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");

        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();