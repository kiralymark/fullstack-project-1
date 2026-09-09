#!/bin/sh
set -eu

# Load PostgreSQL credentials
# Reads the 
# Docker secrets (mounted as files at /run/secrets/*) and builds the
# Postgres connection string at container startup. The credentials only
# ever exist in this container's memory at runtime - never in the
# compose YAML, an image layer, or a .env file on disk.
POSTGRES_USER_VAL="$(cat /run/secrets/postgres_user)"
POSTGRES_PASS_VAL="$(cat /run/secrets/postgres_pass)"
POSTGRES_DB_VAL="$(cat /run/secrets/postgres_db)"

# Load JWT secrets
JWT_ISSUER_VAL="$(cat /run/secrets/jwt_issuer)"
JWT_AUDIENCE_VAL="$(cat /run/secrets/jwt_audience)"
JWT_SECRET_VAL="$(cat /run/secrets/jwt_secret)"

# Export for ASP.NET Core (.NET translates '__' to ':')
export ConnectionStrings__PostgresDb="Server=postgres;Database=${POSTGRES_DB_VAL};User Id=${POSTGRES_USER_VAL};Password=${POSTGRES_PASS_VAL};"
export JWT__Issuer="${JWT_ISSUER_VAL}"
export JWT__Audience="${JWT_AUDIENCE_VAL}"
export JWT__Secret="${JWT_SECRET_VAL}"

exec dotnet fullstack-project-1.dll
