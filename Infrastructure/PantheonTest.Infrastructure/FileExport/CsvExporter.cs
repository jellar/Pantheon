using System.Collections.Generic;
using System.IO;
using CsvHelper;
using PantheonTest.Application.Contracts.Infrastructure;
using PantheonTest.Application.Features.Transaction.Commands.Export;

namespace PantheonTest.Infrastructure.FileExport
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<TransactionExportDto> transactionExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(transactionExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}
