#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ESimple.Web.Host/ESimple.Web.Host.csproj", "src/ESimple.Web.Host/"]
COPY ["src/ESimple.Web.Core/ESimple.Web.Core.csproj", "src/ESimple.Web.Core/"]
COPY ["src/ESimple.Application/ESimple.Application.csproj", "src/ESimple.Application/"]
COPY ["src/ESimple.Core/ESimple.Core.csproj", "src/ESimple.Core/"]
COPY ["src/ESimple.EntityFrameworkCore/ESimple.EntityFrameworkCore.csproj", "src/ESimple.EntityFrameworkCore/"]
RUN dotnet restore "./src/ESimple.Web.Host/ESimple.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/ESimple.Web.Host"
RUN dotnet build "./ESimple.Web.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ESimple.Web.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESimple.Web.Host.dll"]