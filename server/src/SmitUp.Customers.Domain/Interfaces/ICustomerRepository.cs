using SmitUp.Customers.Domain.Entities;
using System.Threading.Tasks;

namespace SmitUp.Customers.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task SaveCustomer(Customer customer);
    }
}
