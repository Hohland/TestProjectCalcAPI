# Calculator test API

## Start execution

`docker-compose up`

## Usage
Swagger http://localhost:57589/

### Authorization
Via header: Token = "<token>"

|Operation|Token|
|---|---|
|Add|AddToken|
|Multiplucate|MultiplucateToken|
|Deduct||
|Divide||

```
Example

curl -X GET "http://localhost:57589/calc/add?summands=1&summands=2" -H  "accept: text/plain" -H  "Token: AddToken"

```
