FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebSocketExample/WebSocketExample.csproj", "WebSocketExample/"]
RUN dotnet restore "WebSocketExample/WebSocketExample.csproj"
COPY . .
WORKDIR "/src/WebSocketExample"
RUN dotnet build "WebSocketExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebSocketExample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSocketExample.dll"]
