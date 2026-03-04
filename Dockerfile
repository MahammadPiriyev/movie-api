FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./MovieAPI/MovieAPI.csproj ./MovieAPI/
COPY ./MovieAPI.DataAccess/MovieAPI.DataAccess.csproj ./MovieAPI.DataAccess/
COPY ./MovieAPI.Domain/MovieAPI.Domain.csproj ./MovieAPI.Domain/
COPY ./MovieAPI.Infrastructure/MovieAPI.Infrastructure.csproj ./MovieAPI.Infrastructure/

RUN dotnet restore "MovieAPI/MovieAPI.csproj"   

COPY . .

WORKDIR "/src/MovieAPI"
RUN dotnet build "MovieAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MovieAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieAPI.dll"]