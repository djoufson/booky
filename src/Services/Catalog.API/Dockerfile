# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ["src/Shared/Shared.csproj", "source/src/Shared/"]
COPY ["src/Aspire/Aspire.ServiceDefaults/Aspire.ServiceDefaults.csproj", "source/src/Aspire/Aspire.ServiceDefaults/"]
COPY ["src/Services/Catalog.API/Catalog.API.csproj", "source/src/Services/Catalog.API/"]
COPY ["src/Services/Catalog.API/Files/", "/app/Files"]

RUN dotnet restore "source/src/Services/Catalog.API/Catalog.API.csproj"

COPY . .
WORKDIR "/source/src/Services/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

# Build the application for publishing
FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 80

# Copy the published output from the 'publish' stage
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
