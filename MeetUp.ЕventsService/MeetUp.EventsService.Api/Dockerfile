FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MeetUp.ЕventsService/MeetUp.EventsService.Api/MeetUp.EventsService.Api.csproj", "MeetUp.ЕventsService/MeetUp.EventsService.Api/"]
RUN dotnet restore "MeetUp.ЕventsService/MeetUp.EventsService.Api/MeetUp.EventsService.Api.csproj"
COPY . .
WORKDIR "/src/MeetUp.ЕventsService/MeetUp.EventsService.Api"
RUN dotnet build "MeetUp.EventsService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetUp.EventsService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetUp.EventsService.Api.dll"]