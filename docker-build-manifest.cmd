docker manifest create maiznpetr/pets-webapi:v1.2.5 --amend maiznpetr/pets-webapi:v1.2.5-amd64 --amend maiznpetr/pets-webapi:v1.2.5-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.5