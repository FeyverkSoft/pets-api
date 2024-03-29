dotnet restore
dotnet build -c release /nowarn:CS1591
dotnet publish --framework net6.0 -c release -o ./docker/build
rmdir ./docker/build /S /Q
docker build ./docker -f ./docker/Dockerfile -t pets-webapi --build-arg ARCH=amd64
docker tag pets-webapi maiznpetr/pets-webapi:v1.2.37-amd64
docker push maiznpetr/pets-webapi:v1.2.37-amd64