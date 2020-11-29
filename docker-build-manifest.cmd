docker manifest create maiznpetr/pets-webapi:v1.1 --amend maiznpetr/pets-webapi:v1.1-amd64 --amend maiznpetr/pets-webapi:v1.1-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.1