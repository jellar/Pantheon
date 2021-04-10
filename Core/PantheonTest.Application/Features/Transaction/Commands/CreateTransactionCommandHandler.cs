using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
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

        public CreateTransactionCommandHandler(IAccountRepository accountRepository, 
            ITransactionRepository transactionRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        public async Task<AccountDetailsVm> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId);
            if (account == null) throw new Exception("Account not found");

            try
            {
                var id = await _transactionRepository.Add(account.Id, request.TransactionType, request.Reference,
                    request.Amount);
                switch (request.TransactionType)
                {
                    case TransactionType.Deposit:
                        account.Balance += request.Amount;
                        break;
                    default:
                        account.Balance -= request.Amount;
                        break;
                }

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
