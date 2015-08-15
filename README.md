##WinServiceManager
#####A tool to manage Windows services in a user-friendly way

The standard Windows console services.msc is not convenient for dealing with services. 
The common way to restart a service is to right-click the service row in the console and select a menu item; 
if you need to restart many services, is might be time-consuming.

WinServiceManager helps to avoid this difficulties.

###Features
* stop / start / restart a service in one click
* show service states with color
* restart several services at once
* quickly find a service by its name
* filter out stopped services

###Installation
The recent stable version of WinServiceManager can be compiled from source.  
The tool is portable --- just copy it to any directory and launch .exe file.

Windows Service Manager requires .NET Framework 4.5 or higher installed on your Windows machine.  
Your account must have Windows administrative privileges to manipulate service states.

###What's next
Currently, state of the services is not automatically updated. 
You need to press "Refresh" button or F5 to acknowledge the actual state.  
Auto-update is going to be supported in one of next releases.
