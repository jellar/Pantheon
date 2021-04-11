using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using PantheonTest.Application.Contracts.Infrastructure;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Application.Features.Transaction.Commands;
using PantheonTest.Application.Profiles;
using PantheonTest.Domain.Entities;
using Shouldly;

namespace PantheonTest.ApplicationTests
{
    public class CreateTransactionCommandHandlerTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ITransactionRepository> _mockTransactionRepository;
        private readonly Mock<ICurrencyConvertService> _mockCurrencyConvertService;
        private readonly IMapper _mapper;
        private readonly CreateTransactionCommandHandler _createTransactionCommandHandler;
        private Guid _userId = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
        public CreateTransactionCommandHandlerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                var mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockCurrencyConvertService = new Mock<ICurrencyConvertService>();
            _createTransactionCommandHandler = new CreateTransactionCommandHandler(_mockAccountRepository.Object, 
                _mockTransactionRepository.Object, _mapper, _mockCurrencyConvertService.Object);
        }

        [Test]
        public void InvalidAccountDepositTest()
        {
            var testAccount = TestData.MockAccount(_userId);
            _mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());
            _mockTransactionRepository
                .Setup(x => x.Add(It.IsAny<Transaction>())).ReturnsAsync(new Guid());

            var createTransactionCommand = new CreateTransactionCommand()
            {
                AccountId = new Guid(),
                Amount = 50m,
                Reference = "Test Reference",
                TransactionType =  TransactionType.Deposit,
                CurrencyType = "GBP"
            };

            Assert.ThrowsAsync<Exception>(()=> _createTransactionCommandHandler.Handle(createTransactionCommand, CancellationToken.None));
        }

        [TestCase(0, 150)]
        [TestCase(1, 50)]
        public async Task ValidAccountTransactionsTest(int transactionType, decimal expected)
        {
            var testAccount = TestData.MockAccount(_userId);
            _mockAccountRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testAccount);
            _mockTransactionRepository
                .Setup(x => x.Add(It.IsAny<Transaction>())).ReturnsAsync(new Guid());

            var createTransactionCommand = new CreateTransactionCommand()
            {
                AccountId = testAccount.Id,
                Amount = 50m,
                Reference = "Test Reference",
                TransactionType = transactionType == 0 ? TransactionType.Deposit : TransactionType.Withdraw,
                CurrencyType = "GBP"
            };

            var result = await _createTransactionCommandHandler.Handle(createTransactionCommand, CancellationToken.None);

            result.Balance.ShouldBe(expected);
        }

       
    }
}
