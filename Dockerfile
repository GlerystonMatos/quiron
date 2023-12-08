FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY Quiron.Api/*.csproj ./Quiron.Api/
COPY Quiron.Auditoria/*.csproj ./Quiron.Auditoria/
COPY Quiron.CrossCutting/*.csproj ./Quiron.CrossCutting/
COPY Quiron.Data.Dapper/*.csproj ./Quiron.Data.Dapper/
COPY Quiron.Data.EF/*.csproj ./Quiron.Data.EF/
COPY Quiron.Domain/*.csproj ./Quiron.Domain/
COPY Quiron.NUnitTest/*.csproj ./Quiron.NUnitTest/
COPY Quiron.Service/*.csproj ./Quiron.Service/
RUN dotnet restore

COPY Quiron.Api/. ./Quiron.Api/
COPY Quiron.Auditoria/. ./Quiron.Auditoria/
COPY Quiron.CrossCutting/. ./Quiron.CrossCutting/
COPY Quiron.Data.Dapper/. ./Quiron.Data.Dapper/
COPY Quiron.Data.EF/. ./Quiron.Data.EF/
COPY Quiron.Domain/. ./Quiron.Domain/
COPY Quiron.NUnitTest/. ./Quiron.NUnitTest/
COPY Quiron.Service/. ./Quiron.Service/

WORKDIR /app/Quiron.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/Quiron.Api/out .

ENTRYPOINT [ "dotnet", "Quiron.Api.dll" ]