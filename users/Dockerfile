FROM microsoft/dotnet:latest

# Create src folder, copy everything in and set working directory
RUN mkdir -p /src
ADD . /src/
WORKDIR /src

# Install dependencies
RUN dotnet restore

EXPOSE 5000

ENTRYPOINT [ "dotnet", "run" ]