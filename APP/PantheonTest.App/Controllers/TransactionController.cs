using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PantheonTest.App.Utility;
using PantheonTest.Application.Features.Transaction.Commands;
using PantheonTest.Application.Features.Transaction.Queries.Export;
using PantheonTest.Application.Features.Transaction.Queries.GetTransactions;

namespace PantheonTest.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTransactionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("export", Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEvents(Guid accountId)
        {
            var fileDto = await _mediator.Send(new GetTransactionExportQuery(){Id = accountId});

            return File(fileDto.Data, fileDto.ContentType, fileDto.TransactionExportFileName);
        }

        [HttpGet("getpaged", Name = "GetPagedTransactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedTransactionsVm>> GetPagedTransactions(Guid accountId, int page, int size)
        {
            var getTransactionsQuery = new GetTransactionsQuery() { AccountId = accountId, Page = page, Size = size };
            var dtos = await _mediator.Send(getTransactionsQuery);

            return Ok(dtos);
        }
    }
}
