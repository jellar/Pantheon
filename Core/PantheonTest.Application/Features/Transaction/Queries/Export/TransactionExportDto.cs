using System;
using System.Collections.Generic;
using System.Text;

namespace PantheonTest.Application.Features.Transaction.Commands.Export
{
    public class TransactionExportDto
    {
        public DateTime DateOn { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }
}
