# .NET Core Scaling Microservices
[<img src="https://dolphub.visualstudio.com/_apis/public/build/definitions/0b4aa86d-e81f-4cd0-8fd3-adc64f1cea24/3/badge"/>](https://dolphub.visualstudio.com/CoreChat/_build/index?definitionId=1)

## Development with Docker

Make sure you have docker and docker-compose installed

Build images from project root

```
docker-compose build
```

Run the containers

```
docker-compose up

# Or in detatched state
docker-compose up -d
```

## Create Databases for Local Development
### Users
Navigate to `./users` and ensure databases are updated through migrations with dotnetcore cli
```
dotnetcore ef database update
```

Take note that database migration process is done in `users` service docker entrypoint.

#### Windows 10 issues
* If there are connection issues pulling images down
    * Open Docker settings
    * Navigate to Network
    * re-apply google dns server settings 8.8.8.8
* If you see the error `Cannot create container for service <theservice>: Drive has not been shared`
    * Open Docker settings
    * Navigate to Shared Drives
    * Enable your drive for sharing so that docker can mount volumes for development purposes
