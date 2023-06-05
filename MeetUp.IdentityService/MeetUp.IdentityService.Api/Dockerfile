FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MeetUp.IdentityService/MeetUp.IdentityService.Api/MeetUp.IdentityService.Api.csproj", "MeetUp.IdentityService/MeetUp.IdentityService.Api/"]
RUN dotnet restore "MeetUp.IdentityService/MeetUp.IdentityService.Api/MeetUp.IdentityService.Api.csproj"
COPY . .
WORKDIR "/src/MeetUp.IdentityService/MeetUp.IdentityService.Api"
RUN dotnet build "MeetUp.IdentityService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetUp.IdentityService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetUp.IdentityService.Api.dll"]
