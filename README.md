# ASP.NET Core Authentication and Authorization Demo

Demo application showing how to do authentication and authorization using ASP.NET Core.

## Bootstrap

### User Accounts

To bootstrap the initial user accounts for the ASP.NET Core MVC application run the following commands:

```bash
cd src/projectManagementTool
dotnet user-secrets init # this command fails if the secrets file already exists
dotnet user-secrets set SeedUserPW "H0merFl1ntst0ne;"
```

### Project database

To create the project database run the following commands:

```bash
sqlite3 projects.db

CREATE TABLE project(id INTEGER NOT NULL, name TEXT NOT NULL, description TEXT, CONSTRAINT pk_project PRIMARY KEY(id));
```

