using System;
using System.Collections.Generic;
using AutoMoq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using PlanPoker.Data;
using PlanPoker.Data.Models;

namespace PlanPoker.Repository.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private AutoMoqer _mocker;
        private UserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _userRepository = _mocker.Create<UserRepository>();
        }

        [Test]
        public void Get_should_return_1()
        {
            
        }
    }
}
