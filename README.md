# Kbtv-webapi

A simple ASP Core RESTful HTTP API for managing missions and timesheets for a small renovation company. 
This API is also responsible for identity management, and uses access & refresh tokens to manage sessions. 
The project is built with the principles of 'Clean Architecture', and with a CQRS pattern. 

## Motivation

Primary motivation for this project is assisting a small renovation company in managing missions and related images, documents and timesheets. 
In addition there has been a focus on learning new technologies and software principles, which is why a CRUD oriented solution was not chosen.

## Tests
1. [Integration tests](./Application.IntegrationTests) - Contains subcutaneous tests which tests the applications commands and queries, ignoring the web layer. 

## Todo

1. Refactoring to be domain focused with mission as aggregate root and isolating domain logic in core library. 
2. Better test coverage, especially for mocked services in current tests. 
3. ...Much more

## Related projects
- [Kbtv-client](https://github.com/fredtvet/TS-Workspace/blob/master/apps/kbtv-client/README.md) - The client application that consumes this API. 
