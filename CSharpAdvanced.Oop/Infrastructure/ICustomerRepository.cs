using CSharpAdvanced.Shared;

namespace CSharpAdvanced.Oop.Infrastructure;

public interface ICustomerRepository
{
    int Add(Customer customer);
    IEnumerable<Customer> GetAllCustomers();
    Customer? GetCustomerById(int id);
}