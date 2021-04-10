namespace PantheonTest.Application.Features.Transaction.Queries.Export
{
    public class TransactionExportVm
    {
        public string TransactionExportFileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
