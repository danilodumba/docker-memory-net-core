FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

#COPY ./DockerTestMemory30/*.sln .
COPY DockerTestMemory30/ .
RUN dotnet restore

COPY DockerTestMemory30/. ./DockerTestMemory30/
WORKDIR /app/DockerTestMemory30
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/DockerTestMemory30/out ./
ENTRYPOINT ["dotnet", "DockerTestMemory30.dll"]