# OpenApiUserStudy

Documentation: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/aspnetcore-openapi

## Visual Studio

To run the project with a nightly build of the .NET SDK (which are not signed),
you must set an environment variable to tell VS to skip signature validation:

```powershell
$env:VSDebugger_ValidateDotnetDebugLibSignatures=0
start devenv
```

## Task 1

Using the documentation above, update the project to serve the
OpenAPI document at the `/openapi/v1.json` endpoint.

