# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем только csproj и восстанавливаем зависимости
COPY UEdit.Backend.csproj ./
RUN dotnet restore

# Копируем весь остальной код
COPY . .

# Публикуем проект (без UseAppHost, минимальный API)
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UEdit.Backend.dll"]
