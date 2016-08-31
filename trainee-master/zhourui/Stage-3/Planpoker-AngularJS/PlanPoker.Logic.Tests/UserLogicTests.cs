using System;
using AutoMoq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.IRepository;
using PlanPoker.Repository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic.Tests
{
    [TestFixture]
    public class UserLogicTests
    {
        //private readonly IUnitOfWorkFactory _unitOfWork;
        //private readonly IUserRepository _userRepository;

        private AutoMoqer _mocker;
        private UserLogic _userLogic;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _userLogic = _mocker.Create<UserLogic>();
        }

        [Test]
        public void Create()
        {
            // Arrange
            var user = new User {UserId = 0, UserName = "abc", Password = "123", Email = "abc@abc.com", Image = ""};
            var unitOfWork = _mocker.GetMock<IUnitOfWork>();
            var userRepository = _mocker.GetMock<IUserRepository>();
            var userLogic = _mocker.GetMock<IUserLogic>();
            
            
            //userLogic.Setup(x => x.CheckIfExist(It.IsAny<string>())).Returns(true);

            // Act
            //_userLogic.Create(user);
            var result = _userLogic.Create(user);
            // Assert
            //userRepository.Verify(x => x.Create(user), Times.Once());
            //unitOfWork.Verify(x => x.Commit(), Times.Once());
            result.Should().Be("");
        }

        [Test]
        public void Edit()
        {
            
        }
    }
}
