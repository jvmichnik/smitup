using Microsoft.EntityFrameworkCore;
using SmitUp.Customers.Domain.Entities;
using SmitUp.Customers.Domain.Repositories;
using SmitUp.Domain.Core.Transaction;
using SmitUp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmitUp.Infra.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SmitUpContext _contexto;
        private readonly DbSet<Customer> _customer;
        private readonly DbSet<User> _user;

        public CustomerRepository(SmitUpContext contexto)
        {
            _contexto = contexto;
            _customer = _contexto.Set<Customer>();
            _user = _contexto.Set<User>();
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _customer.AnyAsync(x => x.Email == email);
        }

        public async Task<User> GetUser(string username)
        {
            return await _user.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task SaveCustomer(Customer customer)
        {
            await _customer.AddAsync(customer);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _user.AnyAsync(x => x.Username == username);
        }
    }
}
