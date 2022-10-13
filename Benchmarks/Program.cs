using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;

BenchmarkRunner.Run<Benchmark>();

public class Benchmark
{
    private CustomerContext _context;

    [GlobalSetup]
    public void Setup()
        => _context = new CustomerContext(withLogging: false);

    [GlobalCleanup]
    public void Cleanup()
        => _context.Dispose();

    [Benchmark]
    public async Task<int> Join()
        => await _context.Customers.CountAsync(c => c.JoinedCountry.Code == "UK");
    
    [Benchmark]
    public async Task<int> JsonWithIndex()
        => await _context.Customers.CountAsync(c => c.Contact.Address.Country == "UK");

    [Benchmark]
    public async Task<int> IndexedComputedProperty()
        => await _context.Customers.CountAsync(c => c.ComputedJsonCountry == "UK");

    [Benchmark]
    public async Task<int> JsonWithoutIndex()
        => await _context.Customers.CountAsync(c => c.Contact.Address.City == "London");
}
