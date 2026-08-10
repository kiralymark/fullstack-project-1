#!/bin/sh
set -eu

# The official pgAdmin image only supports PGADMIN_DEFAULT_EMAIL and
# PGADMIN_DEFAULT_PASSWORD as plain values - there's no documented
# _FILE variant like the postgres image has. So this script reads the
# secrets files ourselves and exports the plain vars just before
# handing off to pgAdmin's own entrypoint, keeping the values out of
# the compose YAML.

export PGADMIN_DEFAULT_EMAIL="$(cat /run/secrets/pgadmin_email)"
export PGADMIN_DEFAULT_PASSWORD="$(cat /run/secrets/pgadmin_pass)"

exec /entrypoint.sh
