using AutoMapper;
using SmitUp.Api.ViewModels.Customer;
using SmitUp.Customers.Domain.Commands.CustomerCommands;
using SmitUp.Customers.Domain.Commands.CustomerCommands.Create;
using SmitUp.Customers.Domain.Enum;
using System;

namespace SmitUp.Api.AutoMapper.Customer
{
    public class CustomerViewModelToDomainMappingProfile : Profile
    {
        public CustomerViewModelToDomainMappingProfile()
        {
            CreateMap<CreateNewCustomerViewModel, CreateCustomerCommand>()
                .ConstructUsing(v => new CreateCustomerCommand(v.Name, v.Gender, v.Birthday, v.EMaritalStatus));
        }
    }
}
