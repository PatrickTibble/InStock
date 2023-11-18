# InStock
 Inventory Management and POS Scanner

# Configuring the Backend


## Create Databases:
The backend uses SQL Server. Create your SQL Server databases and then run the SQL to create tables and stored procedures.

1. InStock.IdentityServer
1. InStock.AccountServer
1. InStock.InventoryServer

You'll find the .sql files in the Solution Items directory.

Execute the sql queries. Create the tables first, then create the Stored Procedures.

## Connect Projects to Databases
Add the connection strings to your UserSecrets (secrets.json, included by right+clicking your csproj in Visual Studio and clicking Manage User Secrets)

### InStock.Backend.AccountService (csproj -> Manage User Secrets)
``` js
{
  "ConnectionStrings": {
    "AccountServer": "{{YourConnectionString.ToTheDatabase}}"
  }
}
```

### InStock.Backend.IdentityService (csproj -> Manage User Secrets)
``` js
{
  "AppSettings": {
    "Token": "{{SomeTokenThatIsSixteenOrMoreCharacters.TheLongerTheBetter}}"
  },
  "ConnectionStrings": {
    "IdentityServer": "{{YourConnectionString.ToTheDatabase}}"
  }
}
```

### InStock.Backend.InventoryService (csproj -> Manage User Secrets)
``` js
{
  "ConnectionStrings": {
    "InventoryServer": "{{YourConnectionString.ToTheDatabase}}"
  }
}
```

# Running the App

1. Start backend services (Start Without Debugging)
   - InStock.Backend.IdentityService
   - InStock.Backend.AccountService
   - Ensure their Swagger specs appear in your browser
1. Start the mobile app (with or without debugging)
   - InStock.Frontend.Mobile
1. Create an Account
1. Log In with created account