# === Stage 1: Build ===
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем весь проект и публикуем
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# === Stage 2: Runtime ===
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# 🧩 Устанавливаем netcat для wait-for-postgres.sh
RUN apt-get update && apt-get install -y netcat-traditional && rm -rf /var/lib/apt/lists/*

# Копируем публикуемое приложение из build stage
COPY --from=build /app/publish .

# Копируем скрипт ожидания Postgres
COPY wait-for-postgres.sh .
RUN chmod +x wait-for-postgres.sh

# Экспортируем порт
EXPOSE 5000

# Запуск приложения через скрипт ожидания Postgres
ENTRYPOINT ["./wait-for-postgres.sh", "postgres", "dotnet", "UEdit.Backend.dll", "--urls=http://0.0.0.0:5000"]
