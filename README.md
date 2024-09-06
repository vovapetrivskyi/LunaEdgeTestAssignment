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

--------------------------------------------------------------------------------------------------

### Tasks

#### Get tasks of the authenticated user

<details>
 <summary><code>GET</code> <code><b>/tasks</b></code></summary>

##### Query Parameters

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | sortBy    | not required | string                  | Property for sorting                                             |
> | sortDirection         | not  required | string                  | Sorting direction                                   |
> | page  | not required | integer                  | Page of collection                                                  |
> | pageSize  | not required | integer                  | Items per page                                                  |

#### Request Body

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | title    | not required | string                  | Task title                                             |
> | description         | not  required | string                  | Task description                                  |
> | dueDate  | not required | Date                  | Task due date                                                  |
> | status  | not required | integer                  | Task status                                                 |
> | priority  | not required | integer                  | Task priority                                                  |
> | createdAt  | not required | Date                  | Date of creation                                                 |
> | updatedAt  | not required | Date                  | Update date                                                |

##### Responses

> | http code     | content-type                      | response                                                            |
> |---------------|-----------------------------------|---------------------------------------------------------------------|
> | `200`         | `application/json; charset=utf-8  | `List of tasks`                                     |
> | `500`         |                                   | `Server error`                                                      |

##### Example cURL

> ```javascript
>  curl -X 'GET' \
>  'https://localhost:7169/tasks?sortDirection=asc&page=1&pageSize=10' \
>  -H 'accept: */*' \
>  -H 'Authorization: Bearer ' \
>  -H 'Content-Type: application/json' \
>  -d '{
>  "title": "string",
>  "description": "string",
>  "dueDate": "2024-09-06T12:11:48.572Z",
>  "status": 0,
>  "priority": 0,
>  "createdAt": "2024-09-06T12:11:48.572Z",
>  "updatedAt": "2024-09-06T12:11:48.572Z"
> }'
> ```

</details>

#### Create new task

<details>
 <summary><code>POST</code> <code><b>/tasks</b></code></summary>

#### Request Body

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | title    | not required | string                  | Task title                                             |
> | description         | not  required | string                  | Task description                                  |
> | dueDate  | not required | Date                  | Task due date                                                  |
> | status  | not required | integer                  | Task status                                                 |
> | priority  | not required | integer                  | Task priority                                                  |


##### Responses

> | http code     | content-type                      | response                                                            |
> |---------------|-----------------------------------|---------------------------------------------------------------------|
> | `200`         | `application/json; charset=utf-8  | `Created Task`                                     |
> | `500`         |                                   | `Server error`                                                      |

##### Example cURL

> ```javascript
> curl -X 'POST' \
>  'https://localhost:7169/tasks' \
>  -H 'accept: */*' \
>  -H 'Authorization: Bearer ' \
>  -H 'Content-Type: application/json' \
>  -d '{
>  "title": "string",
>  "description": "string",
>  "dueDate": "2024-09-06T12:21:56.994Z",
>  "status": 0,
>  "priority": 0
> }'
> ```

</details>

#### Get task by id

<details>
 <summary><code>GET</code> <code><b>/tasks/{id}</b></code></summary>

##### Parameters

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | Id    | required | string                  | Id of the task                                             |


##### Responses

> | http code     | content-type                      | response                                                            |
> |---------------|-----------------------------------|---------------------------------------------------------------------|
> | `200`         | `application/json; charset=utf-8  | `Task`                                     |
> | `400`         | `application/json; charset=utf-8  | `Bad request`                                     |
> | `500`         |                                   | `Server error`                                                      |

##### Example cURL

> ```javascript
>  curl -X 'GET' \
>  'https://localhost:7169/tasks/5' \
>  -H 'accept: */*' \
>  -H 'Authorization: Bearer 
> ```

</details>

#### Update task

<details>
 <summary><code>PUT</code> <code><b>/tasks/{id}</b></code></summary>

##### Parameters

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | Id    | required | string                  | Id of the task                                             |


#### Request Body

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | title    | not required | string                  | Task title                                             |
> | description         | not  required | string                  | Task description                                  |
> | dueDate  | not required | Date                  | Task due date                                                  |
> | status  | not required | integer                  | Task status                                                 |
> | priority  | not required | integer                  | Task priority                                                  |


##### Responses

> | http code     | content-type                      | response                                                            |
> |---------------|-----------------------------------|---------------------------------------------------------------------|
> | `200`         | `application/json; charset=utf-8  | `Ok result`                                     |
> | `400`         | `application/json; charset=utf-8  | `Bad request`                                     |
> | `500`         |                                   | `Server error`                                                      |

##### Example cURL

> ```javascript
> curl -X 'PUT' \
>  'https://localhost:7169/tasks/5' \
>  -H 'accept: */*' \
>  -H 'Authorization: Bearer ' \
>  -H 'Content-Type: application/json' \
>  -d '{
>  "title": "string",
>  "description": "string",
>  "dueDate": "2024-09-06T12:30:29.046Z",
>  "status": 0,
>  "priority": 0
> }'
> ```

</details>

#### Delete task

<details>
 <summary><code>DELETE</code> <code><b>/tasks/{id}</b></code></summary>

##### Parameters

> | name      |  type     | data type               | description                                                           |
> |-----------|-----------|-------------------------|-----------------------------------------------------------------------|
> | Id    | required | string                  | Id of the task                                             |


##### Responses

> | http code     | content-type                      | response                                                            |
> |---------------|-----------------------------------|---------------------------------------------------------------------|
> | `200`         | `application/json; charset=utf-8  | `Ok result`                                     |
> | `400`         | `application/json; charset=utf-8  | `Bad request`                                     |
> | `500`         |                                   | `Server error`                                                      |

##### Example cURL

> ```javascript
> curl -X 'DELETE' \
>  'https://localhost:7169/tasks/5' \
>  -H 'accept: */*' \
>  -H 'Authorization: Bearer '
> ```

</details>
