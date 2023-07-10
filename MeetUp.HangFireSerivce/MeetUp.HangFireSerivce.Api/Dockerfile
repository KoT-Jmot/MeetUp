#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MeetUp.HangFireSerivce/MeetUp.HangFireSerivce.Api/MeetUp.HangFireSerivce.Api.csproj", "MeetUp.HangFireSerivce/MeetUp.HangFireSerivce.Api/"]
RUN dotnet restore "MeetUp.HangFireSerivce/MeetUp.HangFireSerivce.Api/MeetUp.HangFireSerivce.Api.csproj"
COPY . .
WORKDIR "/src/MeetUp.HangFireSerivce/MeetUp.HangFireSerivce.Api"
RUN dotnet build "MeetUp.HangFireSerivce.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetUp.HangFireSerivce.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetUp.HangFireSerivce.Api.dll"]