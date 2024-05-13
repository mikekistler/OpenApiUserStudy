# OpenApiUserStudy

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
