FROM mcr.microsoft.com/dotnet/sdk:6.0 as base
WORKDIR /src
COPY . .
CMD ["dotnet", "restore"]

FROM base as builder
WORKDIR /src
RUN ["dotnet", "build", "-o", "build", "Workers.API/Workers.API.csproj"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=builder /src/build .
CMD ["dotnet", "Workers.API.dll"]