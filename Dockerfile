# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /app

# Copia apenas os csproj (para aproveitar cache no restore)
COPY Apresentation/ReservaHotel.Apresentation.csproj Apresentation/
COPY ReservaHotel.Data/ReservaHotel.Data.csproj ReservaHotel.Data/
COPY ReservaHotel.Domain/ReservaHotel.Domain.csproj ReservaHotel.Domain/
COPY ReservaHotel.Extensions/ReservaHotel.Extensions.csproj ReservaHotel.Extensions/
COPY ReservaHotel.Services/ReservaHotel.Services.csproj ReservaHotel.Services/

# Restaura dependências
RUN dotnet restore Apresentation/ReservaHotel.Apresentation.csproj

# Copia o resto do código
COPY . .

# Build
RUN dotnet build Apresentation/ReservaHotel.Apresentation.csproj -c Release -o /app/build

# Etapa de publish
FROM build AS publish
RUN dotnet publish Apresentation/ReservaHotel.Apresentation.csproj -c Release -o /app/publish

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS final
EXPOSE 5077
ENV ASPNETCORE_URLS=http://+:5077
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "ReservaHotel.Apresentation.dll" ]
