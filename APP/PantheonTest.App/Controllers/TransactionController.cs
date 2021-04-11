using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(IMediator mediator, ILogger<TransactionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTransactionCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Create Transaction failed:" + e.Message);
                throw;
            }
           
        }

        [HttpGet("export", Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEvents(Guid accountId)
        {
            try
            {
                var fileDto = await _mediator.Send(new GetTransactionExportQuery(){Id = accountId});

                return File(fileDto.Data, fileDto.ContentType, fileDto.TransactionExportFileName);
            }
            catch (Exception e)
            {
                _logger.LogError("Export failed: " + e.Message );
                throw;
            }
           
        }

        [HttpGet("getpaged", Name = "GetPagedTransactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedTransactionsVm>> GetPagedTransactions(Guid accountId, int page, int size)
        {
            try
            {
                var getTransactionsQuery = new GetTransactionsQuery() { AccountId = accountId, Page = page, Size = size };
                var dtos = await _mediator.Send(getTransactionsQuery);

                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError("Get transactions failed:" + e.Message);
                throw;
            }
            
        }
    }
}
