using SmitUp.Customers.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmitUp.Customers.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task SaveCustomer(Customer customer);
        Task<Customer> GetCustomerByUser(Guid userId);
    }
}
