all: clean restore build

clean:
	dotnet clean
restore:
	dotnet restore
build:
	dotnet build
test:
	dotnet test DaOfSales.Test/DaOfSales.Test.csproj
run:
	dotnet run DaOfSales.Worker/DaOfSales.Worker.csproj