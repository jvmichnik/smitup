using SmitUp.Customers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmitUp.Api.ViewModels.Customer
{
    public class CreateNewCustomerViewModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string MaritalStatus { get; set; }
        public EMaritalStatus EMaritalStatus
        {
            get
            {
                var status = (EMaritalStatus)Convert.ToInt32(MaritalStatus);

                return status;
            }
        }
    }
}
