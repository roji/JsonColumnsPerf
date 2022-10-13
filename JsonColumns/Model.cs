[PrimaryKey(nameof(_id))]
public class Customer
{
    [Column("Id")]
    private readonly int _id; 

    public Customer(string fullName, string informalName)
    {
        FullName = fullName;
        InformalName = informalName;
    }

    public string FullName { get; init; }
    public string InformalName { get; init; }
    public string? Business { get; set; }
    public Contact Contact { get; set; } = null!;
    
    public string ComputedJsonCountry { get; }
    
    public Country JoinedCountry { get; set; }
}

[Owned]
public class Contact
{
    public Address Address { get; set; } = null!;
    public List<PhoneNumber> PhoneNumbers { get; } = new();
    public bool IsActive { get; set; } = true;
}

[Owned]
public class Address
{
    public Address(string street, string city, string postcode, string country)
    {
        Street = street;
        City = city;
        Postcode = postcode;
        Country = country;
    }

    public string Street { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
    public string Country { get; set; }
}

[Owned]
public class PhoneNumber
{
    public PhoneNumber(int countryCode, string number, PhoneType type)
    {
        CountryCode = countryCode;
        Number = number;
        Type = type;
    }

    public int CountryCode { get; init; }
    public string Number { get; init; }
    public PhoneType Type { get; init; }
}

public enum PhoneType
{
    Home,
    Work,
    Mobile
}

public class Country
{
    public int Id { get; set; }
    
    [MaxLength(2)]
    public string Code { get; set; }
    
    public List<Customer> Customers { get; set; } 
}
