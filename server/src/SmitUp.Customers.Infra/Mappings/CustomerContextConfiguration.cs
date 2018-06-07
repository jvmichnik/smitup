using Microsoft.EntityFrameworkCore;

namespace SmitUp.Customers.Infra.Mappings
{
    public static class CustomerContextConfiguration
    {
        public static void SetConfigurationCustomer(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            //modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
