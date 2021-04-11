using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PantheonTest.Application.Contracts.Persistence;


namespace PantheonTest.Application.Features.Account.Queries
{
    public class GetAccountDetailsQueryHandler : IRequestHandler<GetAccountDetailsQuery, AccountDetailsVm>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountDetailsQueryHandler(IAccountRepository accountRepository
            , IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<AccountDetailsVm> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetUserAccount(request.UserId);
            
            var accountDetails = _mapper.Map<AccountDetailsVm>(account);

            return accountDetails;
        }
    }
}
