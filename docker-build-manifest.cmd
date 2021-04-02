docker manifest create maiznpetr/pets-webapi:v1.2.18 --amend maiznpetr/pets-webapi:v1.2.18-amd64 --amend maiznpetr/pets-webapi:v1.2.18-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.18