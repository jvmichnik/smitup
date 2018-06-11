using AutoMapper;
using SmitUp.Api.Account.ViewModels;
using SmitUp.Infra.CrossCutting.Identity.Models;

namespace SmitUp.Api.AutoMapper.Account
{
    public class AccountViewModelToDomainMappingProfile : Profile
    {
        public AccountViewModelToDomainMappingProfile()
        {
            CreateMap<RegisterViewModel,User>()
                .ConstructUsing(v => new User(v.UserName, v.Email, v.Password));
            CreateMap<LoginViewModel, User>()
               .ConstructUsing(v => new User(v.Username, v.Password));
        }
    }
}
