using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Application.Features.Transaction.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
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
                return id;
            }
            catch
            {
                throw;
            }
        }
    }
}
