using AutoMoq;
using Castle.Windsor;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace PlanPoker.Data.Tests
{
    [TestFixture]
    public class DataBaseFactoryTests
    {
        private AutoMoqer _mocker;

        [SetUp]
        public void Initialize()
        {
            _mocker = new AutoMoqer();
        }

        [TearDown]
        public void Dispose()
        {
            _mocker = null;
        }

        [Test]
        public void GetContext_()
        {
            // Arrange
            var databaseFactory = _mocker.Create<DataBaseFactory>();
            var dbContext = new PlanPokerContext();
            var container = _mocker.GetMock<IWindsorContainer>();
            container.Setup(x => x.Resolve<PlanPokerContext>()).Returns(dbContext);
            // Act
            var result = databaseFactory.GetContext();
            // Assert
            result.Should().Be(dbContext);
            container.Verify(x => x.Resolve<PlanPokerContext>(), Times.Once());
        }
    }
}