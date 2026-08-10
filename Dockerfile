# syntax=docker/dockerfile:1

# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy just the project file first so NuGet restore is cached
# separately from source changes (faster rebuilds).
COPY fullstack-project-1.csproj .
RUN dotnet restore fullstack-project-1.csproj

# Now copy the rest of the source and publish.
COPY . .
RUN dotnet publish fullstack-project-1.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Matches "user: 1001:1001" in docker-compose.yaml.
# The base image already ships a non-root "app" user (UID 64198 on some
# tags), but we pin an explicit UID/GID here so it lines up exactly with
# compose and with the read_only + tmpfs settings there.
RUN addgroup --gid 1001 appgroup \
    && adduser --uid 1001 --gid 1001 --disabled-password --gecos "" appuser \
    && chown -R appuser:appgroup /app

COPY --from=build --chown=appuser:appgroup /app/publish .

COPY --chown=appuser:appgroup entrypoint.sh .
RUN chmod +x entrypoint.sh

USER appuser
EXPOSE 8080

ENTRYPOINT ["./entrypoint.sh"]
