FROM microsoft/dotnet:latest

# Create src folder, copy everything in and set working directory
RUN mkdir -p /src
ADD . /src/
WORKDIR /src

# Install dependencies
RUN dotnet restore

EXPOSE 5000

# Update database tables from migrations
# CMD ls
# CMD dotnet ef database update
# CMD which dotnet ef

# CMD [ "dotnet", "restore"]
# CMD [ "dotnet-ef", "database", "update" ]
ENTRYPOINT [ "dotnet", "run" ]