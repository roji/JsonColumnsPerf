INSERT INTO Countries (Id, Code) VALUES (0, 'UK'), (1, 'FR'), (2, 'DE');

BEGIN TRANSACTION;

DECLARE @i INT = 0;
DECLARE @j INT = 0;
WHILE @i < 1000000
BEGIN
    SET @j = 0
    WHILE @j < 3
BEGIN
INSERT INTO [Customers] ([FullName], [InformalName], [Contact], [JoinedCountryId]) VALUES (
    'Name' + CAST(@i AS nvarchar(max)),
    'Informal' + CAST(@i AS nvarchar(max)),
    '{
      "IsActive": true,
      "Address": {
        "City": "' + (CASE @j WHEN 0 THEN 'London' WHEN 1 THEN 'Paris' WHEN 2 THEN 'Berlin' END) + '",
        "Country": "' + (CASE @j WHEN 0 THEN 'UK' WHEN 1 THEN 'FR' WHEN 2 THEN 'DE' END) + '",
    "Postcode": "CW1 5ZH",
    "Street": "1 Main St"
  },
  "PhoneNumbers": [
    {"CountryCode": 44, "Number": "01632 12345", "Type": "Home"},
    {"CountryCode": 44, "Number": "01632 72345", "Type":"Mobile"}
  ]
}',
    @j);
SET @j = @j + 1;
END;
    SET @i = @i + 1;
END;
COMMIT;
