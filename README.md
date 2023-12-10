# Booky 📚

Booky is an opinionated open source project to showcase microservices best practices from (Djoufson) personal experience and preferences.

# Project overview

The project is a booking system, that basically enables users to buy and download books through a mobile application or the website.
Additionally, authors can subscribe to a plan and upload their books on the platform.

# Features

The below list describes a brief overview of what the system should be capable of,
and the current status of each feature

| Feature                         | Details                                                                                                                     | Status         |
|:--------------------------------|:----------------------------------------------------------------------------------------------------------------------------|:---------------|
| Authentication                  | Authenticate th users with various providers                                                                                | ⛔️ Not started |
| Visit the books catalog         | Filter through the list of available books                                                                                  | ⛔️ Not started |
| Purchase and download a book    | Initiate a an order request, and purchase the selected book. After the purchase, the user will be able to download the book | ⛔️ Not started |
| Upgrade plan to become a author | The author role gives permission to upload books and earn money on the platform by selling those books                      | ⛔️ Not started |
| Make CRUD operations on a book  | Each author is able at any time to upload, delete or update a book on the platform, following some rules and restrictions   | ⛔️ Not started |

# Micro-services

The below list describes what are the microservices involved in our system and their responsibilities

| Service          | Responsibility                                                                                               |
|:-----------------|:-------------------------------------------------------------------------------------------------------------|
| Catalog Service  | This service sort of the books repository.                                                                   |
| Identity Service | Responsible for users management, authentication and access control                                          |
| Order Service    | Responsible for processing order requests from users, as well as making payments using a third party service |

# Credits

This project is highly inspired by the following ones:
- [eShop](https://github.com/dotnet/eShop)
- [aspire](https://github.com/dotnet/aspire) (`eShop-lite`, `dapr`)
