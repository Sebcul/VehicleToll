FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk

WORKDIR /build/
COPY . /build/

RUN dotnet restore --locked-mode -r linux-x64

RUN dotnet format --verify-no-changes --verbosity diagnostic --no-restore ./VehicleToll.sln

RUN dotnet test -c Release -r linux-x64 ./VehicleToll.sln