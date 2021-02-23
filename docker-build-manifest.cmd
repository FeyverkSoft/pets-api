docker manifest create maiznpetr/pets-webapi:v1.2.15 --amend maiznpetr/pets-webapi:v1.2.15-amd64 --amend maiznpetr/pets-webapi:v1.2.15-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.15