# ADO.NET-NetCore 3.1

### NuGet package 
```sh
Microsoft.Data.SqlClient
Microsoft.Extensions.Configuration.Binder
Microsoft.Extensions.Configuration.Abstractions
```

### appsettings.json
```sh
"AppSettings": {
    "SessionTimeout": 60,
    "CommandTimeout": 600,
    "ConnectionStrings": [
      {
        "name": "DefaultConnection",
        "value": "Data Source=DESKTOP-0SNFOQK;Initial Catalog=AdventureWorksDW2017;Integrated Security=False;uid=sa;password=xxxx;"
      },
      {
        "name": "Umbraco",
        "value": "Data Source=DESKTOP-0SNFOQK;Initial Catalog=UmbracoDemo;Integrated Security=False;uid=sa;password=xxxx;"
      }
    ]
  }
```