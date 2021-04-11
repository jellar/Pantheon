using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Application.Features.Account.Queries;
using PantheonTest.Application.Profiles;
using PantheonTest.Domain.Entities;
using Shouldly;

namespace PantheonTest.ApplicationTests
{
    public class GetAccountDetailsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly GetAccountDetailsQueryHandler _getAccountDetailsQueryHandler;

        private Guid _userId = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");

        public GetAccountDetailsQueryHandlerTests()
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
            _getAccountDetailsQueryHandler = new GetAccountDetailsQueryHandler(_mockAccountRepository.Object, _mapper);
        }

        [Test]
        public async Task InValidAccountTest()
        {
            _mockAccountRepository.Setup(x => x.GetUserAccount(It.IsAny<Guid>())).ReturnsAsync(new Account());

            var result = await _getAccountDetailsQueryHandler.Handle(new GetAccountDetailsQuery() {UserId = new Guid()},
                CancellationToken.None);

            
            result.Name.ShouldBeNull();
            
        }

        [Test]
        public async Task ValidAccountTest()
        {
            _mockAccountRepository.Setup(x => x.GetUserAccount(It.IsAny<Guid>())).ReturnsAsync(TestData.MockAccount(_userId));

            var result = await _getAccountDetailsQueryHandler.Handle(new GetAccountDetailsQuery() { UserId = _userId },
                CancellationToken.None);

            result.ShouldNotBeNull();
            result.Name.ShouldBe("Test Account");
        }

    }
}
