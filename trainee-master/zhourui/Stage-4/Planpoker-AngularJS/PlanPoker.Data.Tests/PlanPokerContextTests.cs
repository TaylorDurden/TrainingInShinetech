using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using AutoMoq;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;

namespace PlanPoker.Data.Tests
{
    [TestFixture]
    public class PlanPokerContextTests
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
        public void OnModelCreating_()
        {
        }
    }
}