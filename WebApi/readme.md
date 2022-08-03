# Goals

This project is mainly an excuse for me to explore some technologies that could be useful in my day-to-day job but that
I don't have the opportunity to try at work.
I am therefore starting from my usual tech stack, Angular front end with dotnet backend and a RDBMS for persistence,
iterating from that.

The domain is a matchmaking application like you can see in video games, registering players wanting to find a match and
providing them with one as fast and as suitable as possible. As stated, this is mainly an made-up domain as for some
reasons, it gets me working.

## Roadmap

Technologies I tried in this repo :

- [x] Ef core DB first with SQLite
- [x] Client generation from OpenAPI Doc
- [x] GRPC server and client
    - [x] Simple CRUD
    - [x] Real time update
    - [x] Client generation
- [x] NetMQ message bus with pub/sub
    - [ ] Async interface
- [x] GraphQL through HotChocolate implementation
    - [x] Ef core integration
    - [x] Real time subscription
- [x] SignalR
    - [x] Angular client with Observable
- [x] Hangfire background recurring tasks

Domain goals :

- [x] Create new player
- [x] Manage player queue
- [x] Simple matching algorithm
- [x] Real time match list refresh
- [ ] Matching algorithm taking into account how long the player is waiting in the queue

## Dev Dependencies

``npm install -g grpc-tools ``

### EfCore migrations

``dotnet ef migrations add %MIGRATION_NAME% -p "./DAL/DAL.csproj" -s "./WebApi/WebApi.csproj"``

``dotnet ef database update``

### OpenApi client generation

```shell
npm run generate-client
```

### Grpc client generation

```shell
npm run proto:generate:win
```

## How to start

### Backend

```shell
cd WebApi

dotnet restore

dotnet run
```

### Frontend

```shell
cd AngularClient

npm install

npm run start
```
