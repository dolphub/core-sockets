# .NET Core Scaling Microservices

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

#### Windows 10 issues
* If there are connection issues pulling images down
    * Open Docker settings
    * Navigate to Network
    * re-apply google dnd server settings 8.8.8.8
* If you see the error `Cannot create container for service <theservice>: Drive has not been shared`
    * Open Docker settings
    * Navigate to Shared Drives
    * Enable your drive for sharing so that docker can mount volumes for development purposes
