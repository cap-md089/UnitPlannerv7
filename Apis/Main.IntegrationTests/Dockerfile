FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY Services/Authentication/Protos Services/Authentication/Protos
COPY Services/Capwatch/Protos Services/Capwatch/Protos
COPY Services/Files/Protos Services/Files/Protos
COPY Services/Graph/Protos Services/Graph/Protos
COPY Services/HostConfiguration/Protos Services/HostConfiguration/Protos
COPY Apis/Main/*.csproj Apis/Main/
COPY Apis/Main.IntegrationTests/*.csproj Apis/Main.IntegrationTests/
RUN dotnet restore Apis/Main.IntegrationTests/UnitPlanner.Apis.Main.IntegrationTests.csproj 

COPY Apis/Main Apis/Main
COPY Apis/Main.IntegrationTests Apis/Main.IntegrationTests
WORKDIR /src/Apis/Main.IntegrationTests

ARG RELEASE_CONFIGURATION=Release
RUN dotnet build UnitPlanner.Apis.Main.IntegrationTests.csproj -c ${RELEASE_CONFIGURATION} -o /app

FROM build as dev

RUN adduser api-user

WORKDIR /app
USER api-user
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "UnitPlanner.Apis.Main.IntegrationTests.dll"]