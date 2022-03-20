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

COPY Services/Capwatch/*.csproj Services/Capwatch/
RUN dotnet restore Services/Capwatch/UnitPlanner.Services.Capwatch.csproj

COPY . .
WORKDIR /src/Services/Capwatch

ARG RELEASE_CONFIGURATION=Release
RUN dotnet build UnitPlanner.Services.Capwatch.csproj -c ${RELEASE_CONFIGURATION} -o /app

FROM build as dev

WORKDIR /app
ENTRYPOINT ["dotnet", "UnitPlanner.Services.Capwatch.dll"]

FROM build AS publish
RUN dotnet publish UnitPlanner.Services.Capwatch.csproj -c $RELEASE_CONFIGURATION -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UnitPlanner.Services.Capwatch.dll"]
