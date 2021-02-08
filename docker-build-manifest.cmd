docker manifest create maiznpetr/pets-webapi:v1.2.9 --amend maiznpetr/pets-webapi:v1.2.9-amd64 --amend maiznpetr/pets-webapi:v1.2.9-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.9