ARG BUILD_IMAGE=mcr.microsoft.com/dotnet/sdk:7.0
ARG RUNTIME_BASE_IMAGE=mcr.microsoft.com/dotnet/aspnet:7.0-alpine

FROM $RUNTIME_BASE_IMAGE AS base
# Install cultures (same approach as Alpine SDK image)
RUN apk add --no-cache icu-libs

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 7113

FROM $BUILD_IMAGE AS build

WORKDIR /src
COPY MoneyTracker.sln ./
COPY ["MoneyTracker.Application/MoneyTracker.Application.csproj", "MoneyTracker.Application/"]
COPY ["MoneyTracker.Authentication/MoneyTracker.Authentication.csproj", "MoneyTracker.Authentication/"]
COPY ["MoneyTracker.Logic/MoneyTracker.Logic.csproj", "MoneyTracker.Logic/"]
COPY ["MoneyTracker.Logic.Service/MoneyTracker.Logic.Service.csproj", "MoneyTracker.Logic.Service/"]
COPY ["MoneyTracker.Storage/MoneyTracker.Storage.csproj", "MoneyTracker.Storage/"]
COPY ["MoneyTracker.Storage.Models/MoneyTracker.Storage.Models.csproj", "MoneyTracker.Storage.Models/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/MoneyTracker.Application"
RUN dotnet build "MoneyTracker.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoneyTracker.Application.csproj" -c Release -o /app/publish

FROM base AS final
#ENV key=value
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MoneyTracker.Application.dll"]