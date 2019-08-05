# SmoSample

Sample project for the `Could not load file or assembly 'Microsoft.SqlServer.BatchParser` error, mentioned in [GitHub 783 issue of the SQL Tools Service repository](https://github.com/microsoft/sqltoolsservice/issues/783).

I got this working after installing the full .NET Framework 2.0/3.5 on my Windows 10 machine.

Haven't tried it on an actual App Service, because I should probably target the 3.5 runtime.
Seeing this is a Core (targetting .NET Framework 4.7) project, this probably won't work anyway.

## Running the sample

You should be able to run this Web API project by making a `POST` request to the endpoint `https://localhost:5001/api/migration/RunMigrations`.
This should trigger the `RunMigrations` endpoint in the `MigrationController`.