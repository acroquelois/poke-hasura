# Poke Hasura

* Author: **Croquelois Adrien**
* Date: **10/11/2020**

## Run instance
> docker-compose up -d

## Apply migration
> hasura migrate apply --endpoint http://localhost:8090

## Apply metadata
> hasura metadata apply --endpoint http://localhost:8090

## Bench
> cat bench.yaml | docker run -i --rm -p 8050:8050 -v "$(pwd):/graphql-bench/ws" --net poke-hasura_static-network --ip 172.30.10.12 hasura/graphql-bench:v0.3
