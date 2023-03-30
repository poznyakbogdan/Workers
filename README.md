# Api to manage Workers with Positions

## Run on local env with `docker compose`
1. Change `{ROOT_PASSWORD}` to actual password for root db user in `docker-compose.yml`
2. Build images
```bash
docker compose build
```
3. Run
```bash
docker compose up
```
## Run on local env with `dotnet`
1. Set env variables
```bash
export DB_CONNECTION_STRING="\
server=localhost;\
uid=root;\
database=workers;\
pwd={YOUR_PASSWORD};\
"
```
```bash
export ASPNETCORE_ENVIRONMENT=Development
```
2. Restore packages
```bash 
dotnet restore
```
2. Build API
```bash 
dotnet build -o build Workers.API/Workers.API.csproj 
```
2. Run
```bash 
cd build && dotnet Workers.API.dll
```