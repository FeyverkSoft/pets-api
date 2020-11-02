dotnet restore
dotnet build -c release /nowarn:CS1591
dotnet publish -c release -o ./docker/build
rmdir ./docker/build /S /Q
docker build ./docker -t pets-webapi
docker tag pets-webapi maiznpetr/pets-webapi:v1
docker push maiznpetr/pets-webapi:v1