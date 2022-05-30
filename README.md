# Voice Backend API

## Technologies

- Language: C#
- Framework: .Net Core 5.0, Entity Framework
- Database: PostgreSQL
- Driver: Npgsql.EntityFrameworkCore.PostgreSQL
- Testing: Microsoft.NET.Test
- Deployment: AWS

## Pre-reqs

- Install [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- Install [Postgresql 13.5](https://www.postgresqltutorial.com/install-postgresql/)

## Getting started

- Clone the repository

```
git clone https://github.com/TienNLN/voice-api
```

- Go to ./VoiceAPI folder, run the project in development

```
dotnet run
```

Navigate to `http://localhost:5000` and enjoy!

## Useful Commands

| Command                                                      | Description                                   |
| ------------------------------------------------------------ | --------------------------------------------- |
| `cd ./VoiceAPI && dotnet run`                            | Run project in develop enviroment             |
| `cd ./VoiceAPI && dotnet run watch`                      | Run project in watch-mode                     |
| `cd ./VoiceAPI && dotnet ef migrations add <message>`    | Add new migration                             |
| `cd ./VoiceAPI && dotnet ef database update`             | Update database based on migration history    |

## Copyright and License

Copyright 2022 by Nguyen Lam Nhat Tien - TienNLN. Contact me: tien20001810@gmail.com
# voice-api-backend
# voice-api-backend
