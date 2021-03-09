docker manifest create maiznpetr/pets-webapi:v1.2.16 --amend maiznpetr/pets-webapi:v1.2.16-amd64 --amend maiznpetr/pets-webapi:v1.2.16-arm64v8
docker manifest push maiznpetr/pets-webapi:v1.2.16