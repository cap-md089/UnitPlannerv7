FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY Common/Authentication/*.csproj Common/Authentication/
COPY Apis/Main/*.csproj Apis/Main/
RUN dotnet restore Apis/Main/UnitPlanner.Apis.Main.csproj 

COPY . .
WORKDIR /src/Apis/Main

ARG RELEASE_CONFIGURATION=Release
RUN dotnet build UnitPlanner.Apis.Main.csproj -c ${RELEASE_CONFIGURATION} -o /app

FROM build as dev

RUN adduser api-user

WORKDIR /app
USER api-user
ENTRYPOINT ["dotnet", "UnitPlanner.Apis.Main.dll"]

FROM build AS publish
RUN dotnet publish UnitPlanner.Apis.Main.csproj -c $RELEASE_CONFIGURATION -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN adduser api-user
USER api-user

ENTRYPOINT ["dotnet", "UnitPlanner.Apis.Main.dll"]
