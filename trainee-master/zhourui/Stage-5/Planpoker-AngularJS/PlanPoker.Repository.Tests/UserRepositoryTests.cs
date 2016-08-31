using System;
using System.Collections.Generic;
using AutoMoq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PlanPoker.Repository.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private AutoMoqer _mocker;
        private UserRepository _userRepository;

        [SetUp]
        public void Initialize()
        {
            _mocker = new AutoMoqer();
            _userRepository = _mocker.Create<UserRepository>();
        }

        [TearDown]
        public void Dispose()
        {
            _mocker = null;
            _userRepository = null;
        }

        [Test]
        public void Get_should_return_1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
