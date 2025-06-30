FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env

WORKDIR /src

COPY "BlazorClient/*.csproj" "BlazorClient/"
COPY "Shared/*.csproj" "Shared/"

RUN dotnet restore "./BlazorClient/BlazorClient.csproj"

COPY "BlazorClient/" "BlazorClient/"
COPY "Shared/" "Shared/"

WORKDIR /src/BlazorClient

RUN dotnet publish -o /app/output

FROM nginx:alpine AS runtime

WORKDIR /var/www/web

COPY --from=build-env /app/output/wwwroot .

COPY BlazorClient/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

