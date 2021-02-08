docker manifest create maiznpetr/pets-webapi:v1.2.8 --amend maiznpetr/pets-webapi:v1.2.8-amd64 --amend maiznpetr/pets-webapi:v1.2.8-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.8