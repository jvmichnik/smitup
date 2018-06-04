using SmitUp.Customers.Domain.Entities;
using System.Threading.Tasks;

namespace SmitUp.Customers.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<User> GetUser(string username);
        Task<bool> EmailExists(string email);
        Task SaveCustomer(Customer customer);

        Task<bool> UsernameExists(string username);
    }
}
