.DEFAULT_GOAL := help

createmigration:
	dotnet ef migrations add $(name) --project ./src/Core/Omini.Miq.Migrations/Omini.Miq.Migrations.csproj --startup-project ./src/Api/Omini.Miq.Api
	
migrate:
	dotnet ef database update $(name) --project ./src/Core/Omini.Miq.Migrations/Omini.Miq.Migrations.csproj --startup-project ./src/Api/Omini.Miq.Api

build:
	dotnet build ./src/Omini.Miq.sln

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