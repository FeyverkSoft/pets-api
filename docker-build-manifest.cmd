docker manifest create maiznpetr/pets-webapi:v1.2.20 --amend maiznpetr/pets-webapi:v1.2.20-amd64 --amend maiznpetr/pets-webapi:v1.2.20-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.20