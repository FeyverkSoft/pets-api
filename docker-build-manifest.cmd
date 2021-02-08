docker manifest create maiznpetr/pets-webapi:v1.2.12 --amend maiznpetr/pets-webapi:v1.2.12-amd64 --amend maiznpetr/pets-webapi:v1.2.12-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.12