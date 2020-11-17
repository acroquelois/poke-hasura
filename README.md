# Poke Hasura

* Author: **Croquelois Adrien**
* Date: **10/11/2020**

## Run instance
> docker-compose up

## Bench
> cat bench.yaml | docker run -i --rm -p 8050:8050 -v "$(pwd):/graphql-bench/ws" --net poke-hasura_static-network --ip 172.30.10.12 hasura/graphql
