# Catalog Service

## Core Features
The catalog service has multiple responsibilities, listed as follows

- **Store books information**
- **Filter books and make searches**
- **Upload or edit books**

## Domain

To achieve those, we define the following models (Following C# types)

`Book`
``` json
{
    "id": "Guid",
    "title": "string",
    "description": "string",
    "authorId": "Guid",
    "price": "decimal",
    "created-at": "DateTime",
    "updated-at": "DateTime",
    "tags": [
        "BookTag"
    ]
}
```

`Author`
``` json
{
    "id": "Guid",
    "username": "string",
    "email": "string",
    "firstname": "string",
    "lastname": "string",
    "bio": "string",
    "image-url": "string"
}
```

`BookTag`
``` json
{
    "id": "int",
    "tag": "string"
}
```
## Tech stack

For this service, we opted for this tech stack:

- **API**: REST-full `.NET 8 Minimal API` to interact with
- **Database**: `Postgresql` database
- **Messaging**: Listening to a `RabbitMQ` messaging service

> Note: This tech stack is the one used in production environment
