docker manifest create maiznpetr/pets-webapi:v1.2.22 --amend maiznpetr/pets-webapi:v1.2.22-amd64 --amend maiznpetr/pets-webapi:v1.2.22-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.22