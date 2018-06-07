using Microsoft.EntityFrameworkCore;
using SmitUp.Customers.Domain.Entities;
using SmitUp.Customers.Domain.Interfaces;
using SmitUp.Infra.Data.Context;
using System.Threading.Tasks;

namespace SmitUp.Infra.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SmitUpContext _contexto;
        private readonly DbSet<Customer> _customer;

        public CustomerRepository(SmitUpContext contexto)
        {
            _contexto = contexto;
            _customer = _contexto.Set<Customer>();
        }

        public async Task SaveCustomer(Customer customer)
        {
            await _customer.AddAsync(customer);
        }

    }
}
