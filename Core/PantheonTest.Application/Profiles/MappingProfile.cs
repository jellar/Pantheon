using AutoMapper;
using PantheonTest.Application.Features.Account.Queries;
using PantheonTest.Application.Features.Transaction.Commands;
using PantheonTest.Application.Features.Transaction.Commands.Export;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDetailsVm>().ReverseMap();
            CreateMap<CreateTransactionCommand, Transaction>().ReverseMap();
            CreateMap<Transaction, TransactionExportDto>().ForMember(dest=>dest.Type,
                opt=>
                    opt.MapFrom(source=> source.TransactionType == TransactionType.Deposit ? "Deposit" : "Withdraw"));
        }
    }
}
