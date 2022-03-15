# Future Value Calculator API

This API has two endpoints. The first receives a present value and a number of months. Using these two pieces of information (and the interest rate returned by InterestRateApi) this endpoint calculates the future value of an amount of money. The second endpoint just returns the URL for the project source code and the URL for the production running app.

## Outline

 - [Libraries](#libraries)
 - [Production](#production)
 - [Running locally](#running-locally)
 - [Quick Reference](#quick-reference)

## Libraries

This project is using some libraries and frameworks:

 - [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
 - [Docker](https://docs.docker.com/)
 - [MSTest](https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.testtools.unittesting?view=visualstudiosdk-2022)
 - [Swagger](https://swagger.io/)

## Production

To see the app running in a production environment, access https://osm-future-value-calculator.herokuapp.com

## Running locally

First, clone the project to your local machine using the following command:

```
git clone https://github.com/ozmartins/Osm.FutureValueCalculator.git
```

Then, to enter into the project directory, type into the terminal:

```
cd Osm.FutureValueCalculator\Osm.FutureValueCalculator.Api
```

Next, you have to set an environment variable (the command below applies to Windows)

```
setx INTEREST_RATE_API https://localhost:5001/taxajuros
```

Finally, run the app using the command shown below:

```
dotnet run --project Osm.FutureValueCalculator.Api.csproj
```

Now, the app is running and you can try it accessing the URL https://localhost:5001

## Quick Reference

### Key files

The table below shows the main files in the project

|File|Namespace|Comment|
| ------ | ------ | ----- |
|FutureValueService.cs|Osm.FutureValueCalculator.Domain.Services|A class that encapsulates the whole logic for future value calculation.|
|InterestRateApp.cs|Osm.FutureValueCalculator.App.Apps|A class that accesses InteresRateApi to retrieve the interest rate value.|
|FutureValueApp.cs|Osm.FutureValueCalculator.App.Apps|A class that coordinates the future value calculation.|
|FutureValueController.cs|Osm.FutureValueCalculator.Api.Controllers|A controller which exposes the service to the external world.|
|ShowMeTheCodeController.cs|Osm.FutureValueCalculator.Api.Controllers|A controller that returns the addresses to access the project source code and the project production environment.|

### Endpoints

The API has two endpoints: 
- /CalculaJuros: Calculates the future value of 'present value' after 'X' months.
- /ShowMeTheCode: Returns a JSON with the project's source code URL and the API's production URL.

#### Request 

`GET /CalculaJuros`

    curl -X 'GET' https://osm-future-value-calculator.herokuapp.com/CalculaJuros?ValorInicial=100&Meses=5 -H 'accept: application/json'

#### Response
    {
        "success": true,
        "errors": [],
        "futureValue": 105.1
    }

#### Request 

`GET /ShowMeTheCode`

    curl -X 'GET' https://osm-future-value-calculator.herokuapp.com/ShowMeTheCode -H 'accept: application/json'

#### Response
    {
        "gitHub": {
            "description": "These are the URLs to my GitHub repositories. The InterestRateUrl property shows the address for the project which returns the fixed interest rate. The FutureValueCalculatorUrl shows the address to the project which calculates the future value based on present value, interest rate and time.",
            "interestRateUrl": "https://github.com/ozmartins/Osm.InterestRate",
            "futureValueCalculatorUrl": "https://github.com/ozmartins/Osm.FutureValueCalculator"
        },
        "heroku": {
            "description": "I have decided to deploy the app on Heroku to help you to execute the application. So, just access both URL below to get access to the running app. It's important to notice that I used a free Heroku Dyno, so the response time can be not that good at the first access.",
            "interestRateUrl": "https://osm-interest-rate.herokuapp.com/",
            "futureValueCalculatorUrl": "https://osm-future-value-calculator.herokuapp.com/"
        }
    }
