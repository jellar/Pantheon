using System;
using System.Collections.Generic;
using System.Text;
using PantheonTest.Application.Features.Transaction.Commands.Export;

namespace PantheonTest.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<TransactionExportDto> transactionExportDtos);
    }
}
