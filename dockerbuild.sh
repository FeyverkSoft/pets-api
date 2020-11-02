dotnet restore
dotnet build -c release /nowarn:CS1591
dotnet publish -c release -o ../../docker/build
docker build ./docker -t pets-api
docker tag pets-api maiznpetr/pets-api:latest
docker push maiznpetr/pets-api:latest