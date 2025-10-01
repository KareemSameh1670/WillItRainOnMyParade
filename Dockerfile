# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WillItRainOnMyParade/WillItRainOnMyParade.csproj", "WillItRainOnMyParade/"]
COPY ["WillItRainOnMyParade.BLL/WillItRainOnMyParade.BLL.csproj", "WillItRainOnMyParade.BLL/"]
COPY ["WillItRainOnMyParade.DAL/WillItRainOnMyParade.DAL.csproj", "WillItRainOnMyParade.DAL/"]
RUN dotnet restore "./WillItRainOnMyParade/WillItRainOnMyParade.csproj"
COPY . .
WORKDIR "/src/WillItRainOnMyParade"
RUN dotnet build "./WillItRainOnMyParade.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WillItRainOnMyParade.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "WillItRainOnMyParade.dll"]
