docker manifest create maiznpetr/pets-webapi:v1.2.7 --amend maiznpetr/pets-webapi:v1.2.7-amd64 --amend maiznpetr/pets-webapi:v1.2.7-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.7