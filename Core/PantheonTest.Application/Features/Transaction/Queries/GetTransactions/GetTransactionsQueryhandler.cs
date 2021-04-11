using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Application.Features.Transaction.Commands.Export;

namespace PantheonTest.Application.Features.Transaction.Queries.GetTransactions
{
    class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, PagedTransactionsVm>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetTransactionsQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        public async Task<PagedTransactionsVm> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var list = await _transactionRepository.GetPagedTransactionsAsync(request.AccountId, request.Page,
                request.Size);

            var transactions = _mapper.Map<List<TransactionExportDto>>(list);
            var count = (await _transactionRepository.GetAccountTransactions(request.AccountId)).Count;
            return new PagedTransactionsVm()
                {Count = count, Transactions = transactions, Page = request.Page, Size = request.Size};

        }
    }
}
