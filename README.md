# Data Analysis Of Sales

System developed in c# (.net core) with the objective to import lots of flat files, read and analyse the data, and output a report.

## Requirements for execution
 * .Net Core 2.2 (https://dotnet.microsoft.com/download/dotnet-core/2.2)
 
### How to execution

#### Command Line

 1) After project clone, go to folder of project and run command
 ```
 dotnet restore
 ```
 2) After run command:
 ```
 dotnet build
 ```
 3) Go to folder DaOsSales.App and execute the command:
 ```
 dotnetrun
 ```
 
 #### Visual Studio
 
  1) After project clone, open project in Visual Studio and push F5. :-)

 
 ### Files
Per default the software find files in %HOMEPATH%\data\in\, using the directories %HOMEPATH%\data\out\ and %HOMEPATH%\data\garbage\ for the out data and discard of the processed files, respectively.
If you want to change these paths, you can do this in the appsettings.json file. (put the \ at the end of the path).

### Improvements for the future

* Implement docker image for execute application.
* Improvements in the logs and exceptions, send data for logstash, per example.
* Implement load tests and, if necessary, implement paralelismo.