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

COPY Services/Graph/Graph.Grpc/UnitPlanner.Services.Graph.Grpc.csproj Services/Graph/Graph.Grpc/
COPY Services/Graph/Graph.Service/UnitPlanner.Services.Graph.Service.fsproj Services/Graph/Graph.Service/
RUN dotnet restore Services/Graph/Graph.Service/UnitPlanner.Services.Graph.Service.fsproj

COPY Services/Graph Services/Graph
WORKDIR /src/Services/Graph/Graph.Service

ARG RELEASE_CONFIGURATION=Release
RUN dotnet build UnitPlanner.Services.Graph.Service.fsproj -c ${RELEASE_CONFIGURATION} -o /app

FROM build as dev

WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "UnitPlanner.Services.Graph.Service.dll"]

FROM build AS publish
RUN dotnet publish UnitPlanner.Services.Graph.Service.fsproj -c $RELEASE_CONFIGURATION -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "UnitPlanner.Services.Graph.Service.dll"]
