# BenchmarkEmbeddedDatabases.NET
This project contains code that can run benchmarks for the following .NET centric embedded database technologies:

* Access 2013
* SQL Server Compact 4.0.8876.1
* SQL Server Express LocalDB 2012 - 2014 - 2016
* System.Data.Sqlite (1.0.104)

A full description of the benchmark results [can be read in this blog post](http://www.diericx.net/post/benchmark-embedded-dotnet-databases/).

# Install prerequisites:

* Access 2013 requires the [Access 2013 Runtime](https://www.microsoft.com/en-ca/download/details.aspx?id=39358) installed for those without a suitable Office installed (please note that the 32-bit version is required for JET).
* [SQl Server Express LocalDB 2012](https://www.microsoft.com/en-ca/download/details.aspx?id=29062) - install SqlLocalDB.msi.
* [SQL Server Express LocalDB 2014](https://www.microsoft.com/en-ca/download/details.aspx?id=42299) - install SqlLocalDB.msi.
* [SQL Server Express LocalDB 2016](https://www.microsoft.com/en-cy/sql-server/sql-server-editions-express) - as far as I know there exists no official standalone SqlLocalDB installer, so it must be installed through feature selection in the SQL Server Express installer instead.
