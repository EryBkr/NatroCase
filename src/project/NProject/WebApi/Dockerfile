# .NET 8 SDK'sını temel alan imaj kullan
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["project/NProject/WebApi/WebApi.csproj", "project/NProject/WebApi/"]
COPY ["project/NProject/Application/Application.csproj", "project/NProject/Application/"]
COPY ["project/NProject/Domain/Domain.csproj", "project/NProject/Domain/"]
COPY ["project/NProject/Infrastructure/Infrastructure.csproj", "project/NProject/Infrastructure/"]
COPY ["project/NProject/Persistence/Persistence.csproj", "project/NProject/Persistence/"]
COPY ["core-packages/Core.Packages/Core.Application/Core.Application.csproj", "core-packages/Core.Packages/Core.Application/"]
COPY ["core-packages/Core.Packages/Core.CrossCuttingConcerns/Core.CrossCuttingConcerns.csproj", "core-packages/Core.Packages/Core.CrossCuttingConcerns/"]
COPY ["core-packages/Core.Packages/Core.Persistence/Core.Persistence.csproj", "core-packages/Core.Packages/Core.Persistence/"]
COPY ["core-packages/Core.Packages/Core.Polly/Core.Polly.csproj", "core-packages/Core.Packages/Core.Polly/"]
COPY ["core-packages/Core.Packages/Core.SecurityIdentity/Core.SecurityIdentity.csproj", "core-packages/Core.Packages/Core.SecurityIdentity/"]
RUN dotnet restore "project/NProject/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/project/NProject/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
