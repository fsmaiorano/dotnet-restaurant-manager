## About

This is a simple API for a restaurant management system. It is built with .NET 7 C# and Entity Framework Core with InMemory Database.

## Architecture
Clean architecture is used to separate the application into layers. The layers are as follows: application, domain, and infrastructure. The application layer contains use-cases and all core implementations. The domain layer contains the business logic and
models. The infrastructure layer contains the database context and migrations.

The implementation between the Api and the rest of the application is built with CQRS (Command and Query Responsibility Segregation), a pattern that separates read and update operations for a data store.


## Technologies Used
- .NET 7 C#
- Entity Framework Core with InMemory Database
- JWT Authentication

## Goals

- [x]List all restaurants
- [x]Register new restaurants
- [x]List restaurant data
- [x]Change restaurant data
- [x]Delete a restaurant
- [ ]List all products from a restaurant
- [x]Create a restaurant product
- [x]Change a restaurant product
- [x]Delete a product from a restaurant

- The restaurant registration must have the following fields:
    - [ ]Photo of the restaurant
    - [x]Restaurant name
    - [x]Restaurant address

- The restaurant's product registration must have the following fields:
    - [ ]Product photo
    - [x]Product's name
    - [x]Price of the product
    - [ ]Product category (e.g.: Sweet, Savory, Juices...)

    - When the Product is placed on promotion, it must have the following fields:
        - [x]Description for product promotion (e.g.: Beer at half price)
        - [x]Promotional price
        - [x]Days of the week and time when the product should be on sale