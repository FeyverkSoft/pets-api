docker manifest create maiznpetr/pets-webapi:v1.2.3 --amend maiznpetr/pets-webapi:v1.2.3-amd64 --amend maiznpetr/pets-webapi:v1.2.3-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.3