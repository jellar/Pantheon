using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PantheonTest.Application.Contracts.Infrastructure;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Application.Features.Account.Queries;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Features.Transaction.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, AccountDetailsVm>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ICurrencyConvertService _currencyConvertService;

        public CreateTransactionCommandHandler(IAccountRepository accountRepository, 
            ITransactionRepository transactionRepository, IMapper mapper, 
            ICurrencyConvertService currencyConvertService)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _currencyConvertService = currencyConvertService;
            
        }
        public async Task<AccountDetailsVm> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId);
            if (account == null) throw new Exception("Account not found");

            try
            {
                if (request.CurrencyType != "GBP")
                {
                    var conversion = await _currencyConvertService.GetCurrencyConvert(request.CurrencyType);
                    request.Amount = conversion * request.Amount;
                }
                
                
                var transaction = _mapper.Map<Domain.Entities.Transaction>(request);
                
                switch (request.TransactionType)
                {
                    case TransactionType.Deposit:
                        account.Balance += request.Amount;
                        break;
                    default:
                        account.Balance -= request.Amount;
                        break;
                }
                transaction.Balance = account.Balance;

                var id = await _transactionRepository.Add(transaction);
                await _accountRepository.UpdateAsync(account);
                return _mapper.Map<AccountDetailsVm>(account);
            }
            catch
            {
                throw;
            }
        }
    }
}
