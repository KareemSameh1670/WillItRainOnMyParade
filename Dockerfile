# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["WillItRainOnMyParade.csproj", "./"]
COPY ["../WillItRainOnMyParade.BLL/WillItRainOnMyParade.BLL.csproj", "../WillItRainOnMyParade.BLL/"]
COPY ["../WillItRainOnMyParade.DAL/WillItRainOnMyParade.DAL.csproj", "../WillItRainOnMyParade.DAL/"]

RUN dotnet restore "./WillItRainOnMyParade.csproj"

# Copy everything
COPY . .
COPY ../WillItRainOnMyParade.BLL ../WillItRainOnMyParade.BLL
COPY ../WillItRainOnMyParade.DAL ../WillItRainOnMyParade.DAL

# Build and publish
RUN dotnet build "./WillItRainOnMyParade.csproj" -c Release -o /app/build
RUN dotnet publish "./WillItRainOnMyParade.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "WillItRainOnMyParade.dll"]
