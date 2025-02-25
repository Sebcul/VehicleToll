FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk

WORKDIR /build/
COPY . /build/

RUN dotnet restore --locked-mode -r linux-x64

RUN dotnet format --verify-no-changes --verbosity diagnostic --no-restore ./VehicleToll.sln

RUN dotnet test -c Release -r linux-x64 ./VehicleToll.sln

RUN dotnet publish -c Release -r linux-x64 -o /build/build_files --no-restore ./src/VehicleToll.Core/VehicleToll.Core.csproj


FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app/
COPY --from=sdk /build/build_files /app/

CMD ["dotnet", "VehicleToll.Core.dll"]