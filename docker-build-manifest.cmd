docker manifest create maiznpetr/pets-webapi:v1.2.14 --amend maiznpetr/pets-webapi:v1.2.14-amd64 --amend maiznpetr/pets-webapi:v1.2.14-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.14