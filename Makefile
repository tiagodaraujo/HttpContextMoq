.PHONY: build test pack

build:
	dotnet build HttpContextMoq.sln

test:
	dotnet test HttpContextMoq.sln

coverage:
	dotnet test HttpContextMoq.sln --collect:"XPlat Code Coverage"

pack:
	dotnet pack src/HttpContextMoq/HttpContextMoq.csproj -c Release --include-source --include-symbols -o nupkgs
