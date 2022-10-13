# Benchmarking around JSON columns in SQL Server

This repo contains a minimal EF model for testing the performance of JSON columns on SQL Server. To use, adjust the connection string in CustomerContext.cs and execute SeedData.sql to seed 3 million rows. A BenchmarkDotNet-based benchmark suite is included.

The interesting scenario here is JsonWithIndex. The following EF LINQ query:

```c#
_ = await context.Customers.CountAsync(c => c.Contact.Address.Country == "UK");
```

Gets translated to the following SQL query:

```sql
SELECT COUNT(*)
FROM [Customers] AS [c]
WHERE CAST(JSON_VALUE([c].[Contact],'$.Address.Country') AS nvarchar(max)) = N'UK'
```

Which executes with the following plan in SQL Server:

```
SELECT COUNT(*)
FROM [Customers] AS [c]
WHERE CAST(JSON_VALUE([c].[Contact],'$.Address.Country') AS nvarchar(max)) = N'UK'
  |--Compute Scalar(DEFINE:([Expr1001]=CONVERT_IMPLICIT(int,[Expr1003],0)))
       |--Stream Aggregate(DEFINE:([Expr1003]=Count(*)))
            |--Filter(WHERE:(CONVERT(nvarchar(max),[JsonColumns].[dbo].[Customers].[ComputedJsonCountry] as [c].[ComputedJsonCountry],0)=N'UK'))
                 |--Index Scan(OBJECT:([JsonColumns].[dbo].[Customers].[IX_Customers_ComputedJsonCountry] AS [c]))
```

The `IX_Customers_ComputedJsonCountry` is an index over a computed column, whose definition is `JSON_VALUE(Contact,'$.Address.Country')`.

The interesting bit here is that the EF query does an index scan with `IX_Customers_ComputedJsonCountry`, despite the (redundant) cast to `nvarchar(max)` which is absent from the computed property.

However, the query is (significantly) faster if we remove that scan:

```sql
SELECT COUNT(*)
FROM [Customers] AS [c]
WHERE JSON_VALUE([c].[Contact],'$.Address.Country') = N'UK'
```

This gets executed with an index seek:

```
SELECT COUNT(*)
FROM [Customers] AS [c]
WHERE JSON_VALUE([c].[Contact],'$.Address.Country') = N'UK'
  |--Compute Scalar(DEFINE:([Expr1001]=CONVERT_IMPLICIT(int,[Expr1003],0)))
       |--Stream Aggregate(DEFINE:([Expr1003]=Count(*)))
            |--Index Seek(OBJECT:([JsonColumns].[dbo].[Customers].[IX_Customers_ComputedJsonCountry] AS [c]), SEEK:([c].[ComputedJsonCountry]=N'UK') ORDERED FORWARD)
```
