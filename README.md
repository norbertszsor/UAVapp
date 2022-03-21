## UAVAPP General info
This project is another part of my engineering thesis. The api was created using clean architecture guidelines. The app deals with data processing and transferring it from the arduino board to a web page that displays the board's location.

## Installation
To run this project, you should first configure a connection with an existing database.

##### Configure connection in Json file:  ```appsettings.json```
```json
{
	"Logging": {

		"LogLevel": {

			"Default": "Information",

			"Microsoft": "Warning",

			"Microsoft.Hosting.Lifetime": "Information"

		}

	},

	"ConnectionStrings": {

		"UavAppConnectionString": paste your connection string here

	},
	
	"AllowedHosts": "*"
}
```

##### You should also configure the app auth setting in static class:```AppAuthSettings.cs```
```c#
public static class AppAuthSettings

{

	public static string JwtKey = "PRIVATE_KEY_DONT_SHARE";
	
	public static int JwtExpire = 7;
	
	public static string JwtIssuer = paste your jwt issuer here;
	
	public static string AppAdress = paste your app address here
	
	public static string AppTokenSeparator = "default";

}
```

To run this project, delete the old migration and run the new one:
```
$ remove-migration 
$ add-migiration newOne
```

## Features: 
- New token generating system
- User activation link creator 

## To do:
- FluentValidation
- Middleware 
- Maintenance service
