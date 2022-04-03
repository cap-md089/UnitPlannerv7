# Copyright (C) 2022 Andrew Rioux
# 
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU Affero General Public License as
# published by the Free Software Foundation, either version 3 of the
# License, or (at your option) any later version.
# 
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU Affero General Public License for more details.
# 
# You should have received a copy of the GNU Affero General Public License
# along with this program.  If not, see <http://www.gnu.org/licenses/>.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY unitplanner-v7.sln .
COPY Services/HostConfiguration/HostConfiguration.Grpc/UnitPlanner.Services.HostConfiguration.Grpc.csproj Services/HostConfiguration/HostConfiguration.Grpc/
COPY Services/HostConfiguration/HostConfiguration.Service/UnitPlanner.Services.HostConfiguration.Service.fsproj Services/HostConfiguration/HostConfiguration.Service/
RUN dotnet restore Services/HostConfiguration/HostConfiguration.Service/UnitPlanner.Services.HostConfiguration.Service.fsproj

COPY Services/HostConfiguration Services/HostConfiguration
WORKDIR /src/Services/HostConfiguration/HostConfiguration.Service

ARG RELEASE_CONFIGURATION=Release
RUN dotnet build UnitPlanner.Services.HostConfiguration.Service.fsproj -c ${RELEASE_CONFIGURATION} -o /app

FROM build as dev

WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "UnitPlanner.Services.HostConfiguration.Service.dll"]

FROM build AS publish
RUN dotnet publish UnitPlanner.Services.HostConfiguration.Service.fsproj -c $RELEASE_CONFIGURATION -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "UnitPlanner.Services.HostConfiguration.Service.dll"]
