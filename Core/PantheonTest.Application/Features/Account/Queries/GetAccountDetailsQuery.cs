using System;
using MediatR;

namespace PantheonTest.Application.Features.Account.Queries
{
    public class GetAccountDetailsQuery : IRequest<AccountDetailsVm>
    {
        public Guid UserId { get; set; }
    }
}
