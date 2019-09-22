FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Community.Backend/Community.Backend.csproj", "Community.Backend/"]
RUN dotnet restore "Community.Backend/Community.Backend.csproj"
COPY . .
WORKDIR "/src/Community.Backend"
RUN dotnet build "Community.Backend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Community.Backend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Community.Backend.dll"]