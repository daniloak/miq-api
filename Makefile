.DEFAULT_GOAL := help

createmigration:
	dotnet build ./src/Core/Omini.Miq.Migrations/Omini.Miq.Migrations.csproj
	dotnet ef migrations add $(name) --project ./src/Core/Omini.Miq.Migrations/Omini.Miq.Migrations.csproj --startup-project ./src/Api/Omini.Miq.Api
	
migrate:
	dotnet build ./src/Core/Omini.Miq.Migrations/Omini.Miq.Migrations.csproj
	dotnet ef database update $(name) --project ./src/Core/Omini.Miq.Migrations/Omini.Miq.Migrations.csproj --startup-project ./src/Api/Omini.Miq.Api

build:
	dotnet build ./src/Omini.Miq.sln

build-docker-api:
	docker build -t dkasparian/miqapi:latest . -f ./docker/Dockerfile

build-docker-db:
	docker build -t dkasparian/miqdb:latest . -f ./docker/sql/DockerfileDb

docker-up:
	docker-compose -f ./docker/docker-compose.yaml up

docker-down:
	docker-compose -f ./docker/docker-compose.yaml down -v

rundb:
	docker run  --name miqdb -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password!" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest 

# .PHONY: migrate

# help:
# 	@echo "Usage: make <target>"
# 	@echo ""
# 	@echo "Targets:"
# 	@echo "  createmigration Create a new migration (name=<value>)"
# 	@echo "  migrateup Run migrate up"
# 	@echo "  migratedown Run migrate down"
# 	@echo "  run Run application"
# 	@echo "  help Display this help message"