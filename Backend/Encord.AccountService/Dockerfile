#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Encord.AccountService/Encord.AccountService.csproj", "Encord.AccountService/"]
COPY ["Common/Encord.Common.csproj", "Common/"]
RUN dotnet restore "Encord.AccountService/Encord.AccountService.csproj"
COPY . .
WORKDIR "/src/Encord.AccountService"
RUN dotnet build "Encord.AccountService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Encord.AccountService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Encord.AccountService.dll"]