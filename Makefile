.DEFAULT_GOAL := help

createmigration:
	dotnet ef migrations add $(name) --project ./src/Core/Omini.Miq.Migrations/Omini.Miq.Migrations.csproj --startup-project ./src/Api/Omini.Miq.Api
	
migrate:
	dotnet ef database update $(name) --project ./src/Core/Omini.Miq.Migrations/Omini.Miq.Migrations.csproj --startup-project ./src/Api/Omini.Miq.Api

build:
	dotnet build ./src/Omini.Miq.sln

run:
	go run -ldflags="-X 'main.version=v1.0.0' -X 'main.build=$(shell date +'%Y-%m-%d %H:%M:%S')'" cmd/main.go

wire:
	cd cmd/api && wire

.PHONY: migrate

help:
	@echo "Usage: make <target>"
	@echo ""
	@echo "Targets:"
	@echo "  createmigration Create a new migration (name=<value>)"
	@echo "  migrateup Run migrate up"
	@echo "  migratedown Run migrate down"
	@echo "  run Run application"
	@echo "  help Display this help message"