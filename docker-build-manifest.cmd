docker manifest create maiznpetr/pets-webapi:v1 --amend maiznpetr/pets-webapi:v1-amd64 --amend maiznpetr/pets-webapi:v1-arm64v8
docker manifest push maiznpetr/pets-webapi:v1