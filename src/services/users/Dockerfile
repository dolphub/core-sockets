FROM microsoft/aspnetcore-build:2.0

# Create src folder, copy everything in and set working directory
RUN mkdir -p /app
ADD . /app/
WORKDIR /app

# Install dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

EXPOSE 5000
ENTRYPOINT /bin/bash -c "dotnet ef database update && dotnet out/users.dll"

# dotnet ef database update && 
# ENTRYPOINT /bin/bash -c "dotnet run"
