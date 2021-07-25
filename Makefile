.PHONY: build test pack

build:
	dotnet build src/HttpContextMoq.sln

test:
	dotnet test src/HttpContextMoq.sln

coverage:
	dotnet test src/HttpContextMoq.sln --collect:"XPlat Code Coverage"

pack:
	dotnet pack src/HttpContextMoq/HttpContextMoq.csproj -c Release --include-source --include-symbols -o nupkgs
