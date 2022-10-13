public static class CustomerContextExtensions
{
    public static async Task InitializeDatabase(this CustomerContext customerContext)
    {
        await customerContext.Database.EnsureDeletedAsync();
        await customerContext.Database.EnsureCreatedAsync();

        await customerContext.AddRangeAsync(
            new Customer("Alice C Vickers", "Alice")
            {
                Business = "Retired",
                Contact = new()
                {
                    Address = new("1 Main St", "Camberwick Green", "CW1 5ZH", "UK"),
                    PhoneNumbers = { new(44, "01632 12345", PhoneType.Home), new(44, "01632 72345", PhoneType.Mobile) },
                }
            }
            // new Customer("Macavity C Vickers", "Mac")
            // {
            //     Business = "Biscuits for your Human",
            //     Contact = new()
            //     {
            //         Address = new("17 Station Rd", "Camberwick Green", "CW1 5ZT", "UK"),
            //         PhoneNumbers =
            //         {
            //             new(44, "01632 12375", PhoneType.Home), new(44, "01632 72375", PhoneType.Mobile),
            //             new(44, "01632 82375", PhoneType.Work)
            //         },
            //     }
            // },
            // new Customer("Toast Clark-Vickers", "Toast")
            // {
            //     Business = "Doggie Poker (Presented by Dominoes)",
            //     Contact = new()
            //     {
            //         Address = new("766 Mayfair", "Chigley", "CH1 5ZU", "UK"),
            //         PhoneNumbers = {new(44, "01632 19945", PhoneType.Work), new(44, "01632 799945", PhoneType.Mobile)},
            //     }
            // },
            // new Customer("Baxter C Clark", "Baxter")
            // {
            //     Business = "Pet Phone Industries, Inc.",
            //     Contact = new()
            //     {
            //         Address = new("77 University Way", "Seattle", "98105", "USA"),
            //         PhoneNumbers = {new(1, "(425) 555-1151", PhoneType.Mobile)},
            //     }
            // });
        );
        await customerContext.SaveChangesAsync();
    }
}
