docker manifest create maiznpetr/pets-webapi:v1.2.4 --amend maiznpetr/pets-webapi:v1.2.4-amd64 --amend maiznpetr/pets-webapi:v1.2.4-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.4