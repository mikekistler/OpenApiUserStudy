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

### Task 1 - Add OpenAPI support

Using the [documentation], update the project to serve the
OpenAPI document at the `/openapi/v1.json` endpoint.

### Task 2 - Add features in OpenAPI document

Add the following features to the OpenAPI document:
- Add a `description` on each operation
- Add an `operationId` on each operation
- Add a `description` on each parameter

### Task 3 - Add Spectral linting

Add Spectral linting to the project as described in the
[documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/aspnetcore-openapi?view=aspnetcore-9.0&tabs=visual-studio#linting-generated-openapi-documents-with-spectral),

This task requires enabling build-time generation of the OpenAPI document.

<!-- Links -->

[documentation]: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/aspnetcore-openapi
