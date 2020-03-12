FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY DockerTestMemory22/ .
RUN ls -R
RUN dotnet restore

COPY DockerTestMemory22/. ./DockerTestMemory22/
WORKDIR /app/DockerTestMemory22
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/DockerTestMemory22/out ./
ENTRYPOINT ["dotnet", "DockerTestMemory22.dll"]