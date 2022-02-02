# C# Repository Pattern
This is an example of how to use the repository pattern in C# (C Sharp).   
This example is a bit extreme where it will interface multiple data stores (MSSQL, PostgreSQL, or Mongo DB).  
Typically, the repository pattern is used to decouple the data layer for unit testing purposes. While this
tutorial does include that the primary focus is to demonstrate the repository pattern to it's full potential.

## Getting Started
You will need to have Docker and Docker Compose installed. The application is pre-configured to connect
to the multiple database services hosted in Docker.

`docker compose up`   
