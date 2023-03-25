FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine-amd64 AS runtime

COPY /build /app

WORKDIR /app

ENTRYPOINT ["dotnet", "Pets.Api.dll"]