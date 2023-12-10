# Catalog Service

The catalog service has multiple responsibilities, listed as follows

- **Store books information**
- **Filter books and make searches**
- **Upload or edit books**

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
    "description": "string",
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
