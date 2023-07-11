FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MeetUp.Gateway/MeetUp.Gateway.Api/MeetUp.Gateway.Api.csproj", "MeetUp.Gateway/MeetUp.Gateway.Api/"]
RUN dotnet restore "MeetUp.Gateway/MeetUp.Gateway.Api/MeetUp.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/MeetUp.Gateway/MeetUp.Gateway.Api"
RUN dotnet build "MeetUp.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetUp.Gateway.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetUp.Gateway.Api.dll"]