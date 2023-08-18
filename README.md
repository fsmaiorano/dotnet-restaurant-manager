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

- [ ]Listar todos os restaurantes
- [ ]Cadastrar novos restaurantes
- [ ]Listar os dados de um restaurante
- [ ]Alterar os dados um restaurante
- [ ]Excluir um restaurante
- [ ]Listar todos os produtos de um restautante
- [ ]Criar um produto de um restaurante
- [ ]Alterar um produto de um restaurante
- [ ]Excluir um produto de um restaurante

- O cadastro do restaurante precisa ter os seguintes campos:
    - [ ]Foto do restaurante
    - [ ]Nome do restaurante
    - [ ]Endereço do restaurante
    - [ ]Horários de funcionamento do restaurante (ex.: De Segunda à Sexta das 09h as 18h e de Sabado à Domingo das 11h as 20h).

- O cadastro de produtos do restaurante precisa ter os seguintes campos:
    - [ ]Foto do produto
    - [ ]Nome do produto
    - [ ]Preço do produto
    - [ ]Categoria do produto (ex.: Doce, Salgados, Sucos...)

    - Quando o Produto for colocado em promoção, precisa ter os seguintes campos:
        - [ ]Descrição para a promoção do produto (ex.: Chopp pela metade do preço)
        - [ ]Preço promocional
        - [ ]Dias da semana e o horário em que o produto deve estar em promoção
