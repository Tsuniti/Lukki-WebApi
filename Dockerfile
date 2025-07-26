FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lukki.Api/Lukki.Api.csproj", "Lukki.Api/"]
COPY ["Lukki.Contracts/Lukki.Contracts.csproj", "Lukki.Contracts/"]
COPY ["Lukki.Application/Lukki.Application.csproj", "Lukki.Application/"]
COPY ["Lukki.Domain/Lukki.Domain.csproj", "Lukki.Domain/"]
COPY ["Lukki.Infrastructure/Lukki.Infrastructure.csproj", "Lukki.Infrastructure/"]
RUN dotnet restore "Lukki.Api/Lukki.Api.csproj"
COPY . .
WORKDIR "/src/Lukki.Api"
RUN dotnet build "Lukki.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lukki.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lukki.Api.dll"]
