FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MeetUp.CommentsService/MeetUp.CommentsService.Api/MeetUp.CommentsService.Api.csproj", "MeetUp.CommentsService/MeetUp.CommentsService.Api/"]
RUN dotnet restore "MeetUp.CommentsService/MeetUp.CommentsService.Api/MeetUp.CommentsService.Api.csproj"
COPY . .
WORKDIR "/src/MeetUp.CommentsService/MeetUp.CommentsService.Api"
RUN dotnet build "MeetUp.CommentsService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetUp.CommentsService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetUp.CommentsService.Api.dll"]