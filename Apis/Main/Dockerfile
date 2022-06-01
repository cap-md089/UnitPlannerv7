FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY Common/Authentication/*.csproj Common/Authentication/
COPY Apis/Main.Grpc/*.csproj Apis/Main.Grpc/
COPY Apis/Main/*.fsproj Apis/Main/
RUN dotnet restore Apis/Main/UnitPlanner.Apis.Main.fsproj 

COPY Services/Authentication/Protos Services/Authentication/Protos
COPY Services/Capwatch/Protos Services/Capwatch/Protos
COPY Services/Files/Protos Services/Files/Protos
COPY Services/Graph/Protos Services/Graph/Protos
COPY Services/HostConfiguration/Protos Services/HostConfiguration/Protos
COPY Common/Authentication Common/Authentication
COPY Apis/Main.Grpc Apis/Main.Grpc
COPY Apis/Main Apis/Main
WORKDIR /src/Apis/Main

ARG RELEASE_CONFIGURATION=Release
RUN dotnet build UnitPlanner.Apis.Main.fsproj -c ${RELEASE_CONFIGURATION} -o /app

FROM build as dev

RUN adduser api-user

WORKDIR /app
USER api-user
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
ENTRYPOINT ["dotnet", "UnitPlanner.Apis.Main.dll"]

FROM build AS publish
RUN dotnet publish UnitPlanner.Apis.Main.fsproj -c $RELEASE_CONFIGURATION -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN adduser api-user
USER api-user

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
ENTRYPOINT ["dotnet", "UnitPlanner.Apis.Main.dll"]
