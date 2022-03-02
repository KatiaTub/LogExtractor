# LogExtractor
Log Extractor -  WebAPI Project using .NET 5

# LogExtractor
Log Extractor -  WebAPI Project using .NET 5

Configuration:
![alt text](https://github.com/[username]/[reponame]/blob/[branch]/image.jpg?raw=true)
 

Add “LogFiles” section to appsettings.json file that will contains full paths to files


 

The configuration should be injected to LogController using Dependency Injection

 
 

Controllers:

 

HealthController

GET /health – returns Http Code 200
 

LogsController

GET /logs – returns List of LogFile class



 

in JSON format (note, WebAPI will serialize the response automatically to JSON):





GET /logs/Download - Download all log files as single file – wrap them all in ZIP archive and return a single file
 

GET /logs/download/{id} - Download specific file by id
Instead of using full file path an id (list order) will be used

 

 

 

Some hints for implementation:

Loading Logs to ConfigureServices(IServiceCollection services)



