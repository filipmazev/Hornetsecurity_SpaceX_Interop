# SpaceX Interop Fluent API

### Screenshots:
![Group 2](https://github.com/user-attachments/assets/b40a05d5-b8a3-4f68-8cf3-0d801e7641cd)

## Angular Front-End
Available at:
https://hsspacexinterop.azurewebsites.net/

### Installation

1. Clone the repository:

```bash
git clone https://github.com/filipmazev/Hornetsecurity_SpaceX_Interop.git
cd spacexinterop.client
```

2. Install npm packages (make sure to have Node version 22 or later and NPM installed)

```bash
npm install
```

3. Run the application

```bash
npm start
```

### Build Command for Angular
```bash
ng build --configuration=production --base-href=/
```

## API
Available at:
https://spacexinterop-api.azurewebsites.net/api/

## Features

- Retrieve SpaceX launches with sorting, paging, and optional payload data.
- User authentication with login, logout, registration, and session validation.
- Role-based and authenticated API access.
- Standardized `Result` response wrapper for consistent API responses.
- Standardized wrapper for easy queries to interop with the SpaceX API (https://api.spacexdata.com/)
- Mapper functionality without `AutoMapper`

---

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or any .NET-compatible IDE  
- [SQL Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Optional: [Scalar](https://scalar.com/) for API testing  

### Installation

1. Clone the repository (if not done yet):

```bash
git clone https://github.com/filipmazev/Hornetsecurity_SpaceX_Interop.git
cd spacexinterop.api
```

2. Restore NuGet packages:

```bash
dotnet restore
```

3. Run publis profile

Double click on `SpaceXInteropDb.Local.publish.xml` -> Click `Publish` in the pop-up window.

4. Build the project:

```bash
dotnet build
```

5. Run the project:

```bash
dotnet run --project spacexinterop.api
```

The API should now be available at (for https) https://localhost:7200 or (for http) http://localhost:5267

### Note:
If making changes to the `User` model, please run the following script after making said changes:

```bash
dotnet ef migrations add InitialIdentity
dotnet ef migrations script -o IdentityScript.sql
```

This will generate a `IdentityScript.sql` file, copy it's contents over the contents of `Identity.PostDeployment.sql` and delete `IdentityScript.sql` afterwards.

---

### Key Endpoints

#### /api/SpaceX/GetLaunches

Example request:
```bash
{
  "Upcoming": true,
  "SortDirection": 0,
  "PageIndex": 0,
  "PageSize": 10,
  "IncludePayloads": true
}
```
`SortDirection` is an enum where: Ascending = 0 | Descending = 1

`GetLaunches` sorts by `DateUtc` in the direction provided in `SortDirection`

---

## Fluent API Queries

`QueryOptions` provides a structured way to define how data should be retrieved from the SpaceX API:

- **Sorting:** Specify which fields to sort by and the direction (ascending/descending).  
- **Pagination:** Control page size, page index, and whether pagination is enabled.  
- **Population:** Include related entities and select specific fields from them, supporting nested population.

This system allows you to construct complex queries with a fluent API while maintaining type safety.

---

### QueryOptions

The `QueryOptions` class contains the following properties:

| Property    | Type                    | Description |
|-------------|-------------------------|-------------|
| `Sort`      | `SortOption?`           | Defines the sorting fields and directions. |
| `Offset`    | `int?`                  | Optional offset for pagination. |
| `Page`      | `int?`                  | Optional page index for pagination. |
| `Limit`     | `int?`                  | Optional page size for pagination. |
| `Pagination`| `bool?`                 | Enable or disable pagination. |
| `Populate`  | `List<PopulateOption>?` | Defines which related entities to include and which fields to select. |

### SortOption

`SortOption` allows you to sort by any property of a model.  

**Usage:**

```csharp
var sort = new SortOption()
    .By<Launch>(launch => launch.DateUtc, SortDirectionEnum.Descending);
```

This will generate a JSON object like: 
```json
{
  "date_utc": "desc"
}
```

### PopulateOption

`PopulateOption` allows you to include related entities (e.g., Rocket, Launchpad, Payload) and select specific fields.

Basic usage:
```csharp
var populateRocket = PopulateOption.With<Launch, GuidOrObject<Rocket>>(launch => launch.Rocket!)
    .Selecting<Rocket, string>(rocket => rocket.Name);
```

Nested population:
```csharp
var populatePayloads = PopulateOption.With<Launch, List<GuidOrObject<Payload>>>(launch => launch.Payloads)
    .Selecting<Payload, string?>(payload => payload.Name)
    .Selecting<Payload, string?>(payload => payload.Type)
    .Selecting<Payload, bool>(payload => payload.Reused)
    .Selecting<Payload, List<string>>(payload => payload.Customers)
    .Selecting<Payload, List<string>>(payload => payload.Manufacturers);
```

You can also define nested `PopulateOption`s using P`opulateNested`
```csharp
populateRocket.PopulateNested<Rocket, Engine>(r => r.FirstStage.Engine, engine =>
{
    engine.Selecting<Engine, string>(e => e.Type);
});
```

### Example: Complete Complex Query

```csharp
QueryOptions queryOptions = new()
{
    Sort = new SortOption().By<Launch>(launch => launch.DateUtc, sortDirection),
    Populate =
    [
        PopulateOption.With<Launch, List<GuidOrObject<Payload>>>(launch => launch.Payloads)
            .Selecting<Payload, string?>(payload => payload.Name)
            .Selecting<Payload, string?>(payload => payload.Type)
            .Selecting<Payload, bool>(payload => payload.Reused)
            .Selecting<Payload, List<string>>(payload => payload.Customers)
            .Selecting<Payload, List<string>>(payload => payload.Manufacturers),

        PopulateOption.With<Launch, GuidOrObject<Rocket>>(launch => launch.Rocket!)
            .Selecting<Rocket, string>(rocket => rocket.Name),

        PopulateOption.With<Launch, GuidOrObject<Launchpad>>(launch => launch.Launchpad!)
            .Selecting<Launchpad, string?>(launchpad => launchpad.FullName)
    ],
    Offset = pageIndex * pageSize,
    Page = pageIndex,
    Limit = pageSize,
    Pagination = true
};
```

---

## Known Issues
Safari will not save the cookie as it blocks cross-site tracking of any kind with the option 'Prevent Cross-Site Tracking' which is enabled by default. 
The API and front-end need to be hosted on the same domain and subdomain for this to work.

---
