FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /src

FROM base as deps
WORKDIR /src/BES.OutboxPattern.BusinessLayer
COPY ./src/BES.OutboxPattern.BusinessLayer/*.csproj .
WORKDIR /src/BES.OutboxPattern.DataAccessLayer
COPY ./src/BES.OutboxPattern.DataAccessLayer/*.csproj .
WORKDIR /src/BES.OutboxPattern.Shared
COPY ./src/BES.OutboxPattern.Shared/*.csproj .
WORKDIR /src/BES.OutboxPattern
COPY ./src/BES.OutboxPattern/*.csproj .

COPY *.csproj .
RUN dotnet restore

FROM deps as build
COPY . .
RUN dotnet publish -c Release -o ../compiled

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as deploy
ENV ASPNETCORE_URLS=http://*:5001
ENV ASPNETCORE_ENVIRONMENT="production"

COPY --from=build /src/compiled .

EXPOSE 5001
ENTRYPOINT ["dotnet", "BES.OutboxPattern.dll"]
