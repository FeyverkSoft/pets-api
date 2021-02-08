docker manifest create maiznpetr/pets-webapi:v1.2.13 --amend maiznpetr/pets-webapi:v1.2.13-amd64 --amend maiznpetr/pets-webapi:v1.2.13-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.13