FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MeetUp.HangFireService/MeetUp.HangFireService.Api/MeetUp.HangFireService.Api.csproj", "MeetUp.HangFireService/MeetUp.HangFireService.Api/"]
COPY ["MeetUp.HangFireService/MeetUp.HangFireService.Application/MeetUp.HangFireService.Application.csproj", "MeetUp.HangFireService/MeetUp.HangFireService.Application/"]
COPY ["MeetUp.MessageBroker/MeetUp.Kafka/MeetUp.Kafka.csproj", "MeetUp.MessageBroker/MeetUp.Kafka/"]
COPY ["MeetUp.HangFireService/MeetUp.HangFireService.Infrastructure/MeetUp.HangFireService.Infrastructure.csproj", "MeetUp.HangFireService/MeetUp.HangFireService.Infrastructure/"]
RUN dotnet restore "MeetUp.HangFireService/MeetUp.HangFireService.Api/MeetUp.HangFireService.Api.csproj"
COPY . .
WORKDIR "/src/MeetUp.HangFireService/MeetUp.HangFireService.Api"
RUN dotnet build "MeetUp.HangFireService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetUp.HangFireService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetUp.HangFireService.Api.dll"]