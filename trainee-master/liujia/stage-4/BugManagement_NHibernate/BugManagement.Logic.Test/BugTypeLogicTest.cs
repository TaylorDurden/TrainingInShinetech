using System.Collections.Generic;
using System.Linq;
using BugManagement.DAL;
using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using BugManagement.Logic.Logic;
using Moq;
using NUnit.Framework;

namespace BugManagement.Logic.Test
{
    [TestFixture]
    public class BugTypeLogicTest
    {
        private Mock<IBugTypeRepository> _bugTypeRepositoryMock;
        private BugTypeLogic _bugTypeLogic;

        [SetUp]
        public void Init()
        {
            _bugTypeRepositoryMock = new Mock<IBugTypeRepository>();

            _bugTypeLogic = new BugTypeLogic(_bugTypeRepositoryMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _bugTypeRepositoryMock = null;
            _bugTypeLogic = null;
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_bugType_repository_once_return_bugTypeLogicModel_list()
        {
            //Arange
            var bugType = new BugType()
            {
                BugTypeId=1,
                Name="name",
                Status="status"
            };
            var bugTypes = new List<BugType>() { bugType };
            _bugTypeRepositoryMock.Setup(n => n.Query()).Returns(bugTypes.AsQueryable());

            //Act
            var result = _bugTypeLogic.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }
}
