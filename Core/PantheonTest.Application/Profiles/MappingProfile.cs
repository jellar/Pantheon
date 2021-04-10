using AutoMapper;
using PantheonTest.Application.Features.Account.Queries;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDetailsVm>().ReverseMap();
          
        }
    }
}
