# LunaEdgeTestAssignment

## Setup instruction:
- Install .NET 8 from https://dotnet.microsoft.com/ru-ru/download/dotnet/8.0
- Download repository
- Write the correct connection string in appsettings.json into "AppDbConnectionString"
- Build the solution, set a startup project as LunaEdgeWebAPI, and run the project. 
You can also go to the solution folder with the command line and run
```sh
dotnet build
```
After that go to the LunaEdgeWebAPI folder with 
```sh
cd LunaEdgeWebAPI
```
and run 
```sh
dotnet run
```

## API documentation

### Users

#### Create new user

<details>
 <summary><code>POST</code> <code><b>/users/register</b></code></summary>

##### Parameters

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | userename |  required | string                  | Name of new user                                                      |
> | email     |  required | string                  | Email of new user                                                     |
> | password  |  required | string                  | Password                                                              |


##### Responses

> | http code     | content-type                      | response                                                            |
> |---------------|-----------------------------------|---------------------------------------------------------------------|
> | `200`         | `application/json; charset=utf-8  | `{"username": "", "token": ""}`                                     |
> | `500`         |                                   | `Server error`                                                      |

##### Example cURL

> ```javascript
>  curl -X 'POST' \
>  'https://localhost:7169/Users/Register' \
>  -H 'accept: */*' \
>  -H 'Content-Type: application/json' \
>  -d '{
>  "username": "string",
>  "email": "user@example.com",
>  "password": "shhSfz$6"
> }'
> ```

</details>

-----------------------------------------------------------------------------------------------------------

#### Login

<details>
 <summary><code>POST</code> <code><b>/users/login</b></code></summary>

##### Parameters

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | userename |  required | string                  | Name of new user                                                      |
> | password  |  required | string                  | Password                                                              |


##### Responses

> | http code     | content-type                      | response                                                            |
> |---------------|-----------------------------------|---------------------------------------------------------------------|
> | `200`         | `application/json; charset=utf-8  | `{"username": "", "token": ""}`                                     |
> | `500`         |                                   | `Server error`                                                      |

##### Example cURL

> ```javascript
>  curl -X 'POST' \
>   'https://localhost:7169/Users/Login' \
>   -H 'accept: */*' \
>   -H 'Content-Type: application/json' \
>   -d '{
>   "login": "string",
>   "password": "string"
> }'
> ```

</details>
