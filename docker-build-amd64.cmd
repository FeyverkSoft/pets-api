dotnet restore
dotnet build -c release /nowarn:CS1591
dotnet publish -c release -o ./docker/build
rmdir ./docker/build /S /Q
docker build ./docker -f ./docker/Dockerfile -t pets-webapi --build-arg ARCH=amd64
docker tag pets-webapi maiznpetr/pets-webapi:v1.2.7-amd64
docker push maiznpetr/pets-webapi:v1.2.7-amd64