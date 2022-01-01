FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY Common/Authentication/UnitPlanner.Common.Authentication.csproj Common/Authentication/UnitPlanner.Common.Authentication.csproj
COPY Common/Models/UnitPlanner.Common.Models.csproj Common/Models/UnitPlanner.Common.Models.csproj
COPY Apis/Main/UnitPlanner.Apis.Main.csproj Apis/Main/UnitPlanner.Apis.Main.csproj
COPY . .
WORKDIR /src/Apis/Main
RUN dotnet build UnitPlanner.Apis.Main.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish UnitPlanner.Apis.Main.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UnitPlanner.Apis.Main.dll"]
