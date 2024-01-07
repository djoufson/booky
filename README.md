[![wakatime](https://wakatime.com/badge/github/Djoufson/booky.svg)](https://wakatime.com/badge/github/Djoufson/booky)

# Booky 📚

Booky is an opinionated open source project to showcase microservices best practices from my (Djoufson) personal experience and preferences.

# Project overview

The project is a booking system, that basically enables users to buy and download books through a mobile application or the website.
Additionally, authors can subscribe to a plan and upload their books on the platform.

# Features

The below list describes a brief overview of what the system should be capable of,
and the current status of each feature

| Feature                         | Details                                                                                                                     | Status         |
|:--------------------------------|:----------------------------------------------------------------------------------------------------------------------------|:---------------|
| Authentication                  | Authenticate th users with various providers                                                                                | ✅ Done         |
| Visit the books catalog         | Filter through the list of available books                                                                                  | ✅️ Done        |
| Add / Remove books to the cart  | Through the Basket Service, authenticated users will be able to add books to the cart or remove others from it              | 〽️ In progress |
| Purchase and download a book    | Initiate a an order request, and purchase the selected book. After the purchase, the user will be able to download the book | ⛔️ Not started |
| Upgrade plan to become a author | The author role gives permission to upload books and earn money on the platform by selling those books                      | ⛔️ Not started |
| Make CRUD operations on a book  | Each author is able at any time to upload, delete or update a book on the platform, following some rules and restrictions   | ⛔️ Not started |

# Micro-services

The below list describes what are the microservices involved in our system and their responsibilities

| Service          | Responsibility                                                                                               |
|:-----------------|:-------------------------------------------------------------------------------------------------------------|
| Catalog Service  | This service is sort of the books repository.                                                                |
| Identity Service | Responsible for users management, authentication and access control                                          |
| Basket Service   | Responsible for storing the state of customers cart                                                          |
| Order Service    | Responsible for processing order requests from users, as well as making payments using a third party service |

# Getting started

Do you want to try this on your own? Here are the steps to follow to do so.

## Prerequisites

- Clone the booky repository: https://github.com/Djoufson/booky
- (Windows only) Install Visual Studio. Visual Studio contains tooling support for .NET Aspire that you will want to have. [Visual Studio 2022 version 17.9 Preview](https://visualstudio.microsoft.com/vs/preview/). 

    During installation, ensure that the following are selected:
    - `ASP.NET and web development` workload.
    - `.NET Aspire SDK` component in `Individual components`.

- Install the latest [.NET 8 SDK](https://github.com/dotnet/installer#installers-and-binaries)
- On Mac/Linux (or if not using Visual Studio), install the Aspire workload with the following commands:
    ```powershell
    dotnet workload update
    dotnet workload install aspire
    dotnet restore booky.Web.slnf
    ```
- Install & start Docker Desktop:  https://docs.docker.com/engine/install/

## Running the solution

> ⚠️
> Remember to ensure that Docker is running

* (Windows only) Run the application from Visual Studio:
  - Open the `booky.Web.slnf` file in Visual Studio
  - Ensure that `booky.AppHost.csproj` is your startup project
  - Hit Ctrl-F5 to launch Aspire


* Or run the application from your terminal:
    ```powershell
    dotnet run --project src/Aspire/Aspire.AppHost/Aspire.AppHost.csproj
    ```
    then look for lines like this in the console output in order to find the URL to open the Aspire dashboard:
    ```sh
    Now listening on: http://localhost:15040
    ```

# Credits

This project is highly inspired by the following ones:
- [eShop](https://github.com/dotnet/eShop)
- [aspire](https://github.com/dotnet/aspire) (`eShop-lite`, `dapr`)
