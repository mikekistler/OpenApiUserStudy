# OpenApiUserStudy

## Setup

### Visual Studio

To run the project with a nightly build of the .NET SDK (which are not signed),
you must set an environment variable to tell VS to skip signature validation:

```powershell
$env:VSDebugger_ValidateDotnetDebugLibSignatures=0
start devenv
```

## Tasks

### Add OpenAPI support

Using the [documentation], update the project to serve the
OpenAPI document at the `/openapi/v1.json` endpoint.



## Add build-time generation of OpenAPI definition

Add the [`ApiDescription.Server`] package.

```bash
dotnet add package Microsoft.Extensions.ApiDescription.Server
```

and enable it in the project file.

```xml
  <PropertyGroup>
    <OpenApiGenerateDocumentsOnBuild>true</OpenApiGenerateDocumentsOnBuild>
  </PropertyGroup>
```

<!-- Links -->

[documentation]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/aspnetcore-openapi
