docker manifest create maiznpetr/pets-webapi:v1.2.2 --amend maiznpetr/pets-webapi:v1.2.2-amd64 --amend maiznpetr/pets-webapi:v1.2.2-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.2