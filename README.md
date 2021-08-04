# Forecast api

<h3 align="center"> 
	🌞 A Microservice Forecast by city name 🌧️
</h3>

### Features

- [Relieble] Retry request when it failed, base on policy
- [Circuit-Break] Stops to retry case it failed more than configuration parameters
- [Health-Checks] Check if the service provider is online

### Config

- Go to https://openweathermap.org/ register and get a apikey.

```
dotnet user-secrets init
```

```
dotnet user-secrets set ServiceSettings:ApiKey <your api-key>
```
