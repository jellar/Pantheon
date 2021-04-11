using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PantheonTest.Application.Contracts.Infrastructure;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Application.Features.Transaction.Commands.Export;

namespace PantheonTest.Application.Features.Transaction.Queries.Export
{
    public class GetTransactionExportQueryHandler : IRequestHandler<GetTransactionExportQuery, TransactionExportVm>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICsvExporter _csvExporter;

        public GetTransactionExportQueryHandler(IMapper mapper, ITransactionRepository transactionRepository, ICsvExporter csvExporter)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _csvExporter = csvExporter;
        }
        public async Task<TransactionExportVm> Handle(GetTransactionExportQuery request, CancellationToken cancellationToken)
        {
            var result = await _transactionRepository.GetAccountTransactions(request.Id);
            var transactions = _mapper.Map<List<TransactionExportDto>>(result);

            var fileData = _csvExporter.ExportEventsToCsv(transactions);

            var eventExportFileDto = new TransactionExportVm() { ContentType = "text/csv", Data = fileData, TransactionExportFileName = $"{Guid.NewGuid()}.csv" };

            return eventExportFileDto;
        }
    }
}
