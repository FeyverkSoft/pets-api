dotnet restore
dotnet build -c release /nowarn:CS1591
dotnet publish -c release -o ./docker/build
rmdir ./docker/build /S /Q
docker build ./docker -f ./docker/Dockerfile-arm64v8 -t pets-webapi --build-arg ARCH=arm64
 docker tag pets-webapi maiznpetr/pets-webapi:v1.2-arm64v8
 docker push maiznpetr/pets-webapi:v1.2-arm64v8