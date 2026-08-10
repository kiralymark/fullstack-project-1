#!/bin/sh
set -eu

# Reads Docker secrets (mounted as files at /run/secrets/*) and builds the
# Postgres connection string at container startup. The credentials only
# ever exist in this container's memory at runtime - never in the
# compose YAML, an image layer, or a .env file on disk.

POSTGRES_USER_VAL="$(cat /run/secrets/postgres_user)"
POSTGRES_PASS_VAL="$(cat /run/secrets/postgres_pass)"
POSTGRES_DB_VAL="$(cat /run/secrets/postgres_db)"

export ConnectionStrings__PostgresDb="Server=postgres;Database=${POSTGRES_DB_VAL};User Id=${POSTGRES_USER_VAL};Password=${POSTGRES_PASS_VAL};"

exec dotnet fullstack-project-1.dll
