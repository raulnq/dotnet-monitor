FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["DotNetMonitorSandBox/DotNetMonitorSandBox.csproj", "DotNetMonitorSandBox/"]
RUN dotnet restore "DotNetMonitorSandBox/DotNetMonitorSandBox.csproj"
COPY . .
WORKDIR "/DotNetMonitorSandBox"
RUN dotnet build "DotNetMonitorSandBox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetMonitorSandBox.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetMonitorSandBox.dll"]