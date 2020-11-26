#!/bin/sh

# Author: acroquelois
# Date: 26/11/2020

docker-compose up -d

echo "waiting for container deployment..."

while (curl -s -o /dev/null -w '%{http_code}' http:/localhost:8090/healthz) -ne 200; sleep 1; done

echo "containers started up..."
sleep 