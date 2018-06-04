using SmitUp.Customers.Domain.Transaction;
using SmitUp.Customers.Infra.Context;
using SmitUp.Infra.Data;

namespace SmitUp.Customers.Infra
{
    public class CustomerUow : UnitOfWork<CustomerContext>, ICustomerUow
    {
        public CustomerUow(CustomerContext context)
            :base(context)
        {

        }
    }
}
