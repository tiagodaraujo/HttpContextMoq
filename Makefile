.PHONY: build pack

build:
	dotnet build src/HttpContextMoq/HttpContextMoq.csproj -c Release

test:
	dotnet test tests/HttpContextMoq.Tests/HttpContextMoq.Tests.csproj

pack:
	dotnet pack src/HttpContextMoq/HttpContextMoq.csproj -c Release --include-source --include-symbols -o nupkgs
