# Data Analysis Of Sales

System developed in c# (.net core) with the objective to import lots of flat files, read and analyse the data, and output a report.

## Requirements for execution
 * .Net Core 3.1 (https://dotnet.microsoft.com/download/dotnet-core/3.1)
 
### How to execution

### Before
 1) Create folders in path $HOME/data/in, $HOME/data/out and $HOME/data/processing ($HOME = The user's home folder.)
 2) Change values of configurations in DataAnalysisOfSales/DaOfSales.Worker/appsettings.json
 ```
  "PathConfigurations": {
    "RootPathIn": "/home/your user/data/in",
    "RootPathProcessing": "/home/your user/data/processing",
    "RootPathOut": "/home/your user/data/out"
  } 
 ```
 3) The files to be processed must be placed in the "in" folder (at the root of the project there is a "Sample" folder with sample files). <b>Warning - They will be deleted after processing.<b>

#### Command Line - Makefile

 1) After project clone, go to folder of project and run command
 ```
 make
 ```
 2) After run command:
 ```
 make run
 ```
 
#### Command Line (tests) - Makefile 
 1) After project clone, go to folder of project and run command
 ```
 make
 ```
 2) After run command:
 ```
 make test
 ```

#### Command Line

 1) After project clone, go to folder of project and run command
 ```
 dotnet restore
 ```
 2) After run command:
 ```
 dotnet build
 ```
 3) After run command:
 ```
 dotnet run DaOfSales.Worker/DaOfSales.Worker.csproj
 ```
 
#### Command Line (tests)

 1) After project clone, go to folder of project and run command
 ```
 dotnet restore
 ```
 2) After run command:
 ```
 dotnet build
 ```
 3) After run command:
 ```
 dotnet test DaOfSales.Test/DaOfSales.Test.csproj
 ```