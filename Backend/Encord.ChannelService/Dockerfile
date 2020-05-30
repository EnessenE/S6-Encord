#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Encord.ChannelService/Encord.ChannelService.csproj", "Encord.ChannelService/"]
COPY ["Common/Encord.Common.csproj", "Common/"]
RUN dotnet restore "Encord.ChannelService/Encord.ChannelService.csproj"
COPY . .
WORKDIR "/src/Encord.ChannelService"
RUN dotnet build "Encord.ChannelService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Encord.ChannelService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Encord.ChannelService.dll"]