#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/LHBookstore.Application/LHBookstore.Application/LHBookstore.csproj", "src/LHBookstore.Application/LHBookstore.Application/"]
COPY ["LHBookstore.Application/LHBookstore.Application.csproj", "LHBookstore.Application/"]
COPY ["src/LHBookstore.Domain/LHBookstore.Domain.csproj", "src/LHBookstore.Domain/"]
COPY ["src/LHBookstore.Infrastructure/LHBookstore.Infrastructure.csproj", "src/LHBookstore.Infrastructure/"]
RUN dotnet restore "src/LHBookstore.Application/LHBookstore.Application/LHBookstore.csproj"
COPY . .
WORKDIR "/src/src/LHBookstore.Application/LHBookstore.Application"
RUN dotnet build "LHBookstore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LHBookstore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LHBookstore.dll"]