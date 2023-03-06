FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SovosCase.WebAPI/SovosCase.WebAPI.csproj", "SovosCase.WebAPI/"]
COPY ["SovosCase.Application/SovosCase.Application.csproj", "SovosCase.Application/"]
COPY ["SovosCase.Domain/SovosCase.Domain.csproj", "SovosCase.Domain/"]
COPY ["SovosCase.Infrastructure/SovosCase.Infrastructure.csproj", "SovosCase.Infrastructure/"]
COPY ["SovosCase.Persistence/SovosCase.Persistence.csproj", "SovosCase.Persistence/"]
RUN dotnet restore "SovosCase.WebAPI/SovosCase.WebAPI.csproj"
COPY . .
WORKDIR "/src/SovosCase.WebAPI"
RUN dotnet build "SovosCase.WebAPI.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "SovosCase.WebAPI.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SovosCase.WebAPI.dll"]