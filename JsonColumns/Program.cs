await using var context = new CustomerContext();
// await context.Database.EnsureDeletedAsync();
// await context.Database.EnsureCreatedAsync();

// Join
// _ = await context.Customers.CountAsync(c => c.JoinedCountry.Code == "UK");
    
// JsonWithIndex
_ = await context.Customers.CountAsync(c => c.Contact.Address.Country == "UK");

// IndexedComputedProperty
// _ = await context.Customers.CountAsync(c => c.ComputedJsonCountry == "UK");

// JsonWithoutIndex
// _ = await context.Customers.CountAsync(c => c.Contact.Address.City == "London");

