# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY UserService.API.csproj ./
RUN dotnet restore


COPY . ./
RUN dotnet publish UserService.API.csproj -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "AI Powered Ticketing and Support System - Backend.dll"]
# the container port can be same but the host should have different port