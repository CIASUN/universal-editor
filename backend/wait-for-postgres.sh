#!/bin/sh
host="$1"
shift
cmd="$@"

echo "Waiting for Postgres at $host..."
until nc -z $host 5432; do
  sleep 1
done

echo "Postgres is up! Running command..."
exec $cmd
