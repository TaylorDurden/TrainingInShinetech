using System.Collections.Generic;
using BugManagement.Logic.ILogic;
using BugManagement.Logic.Models;
using BugManagemnet.WebAPI.Controllers;
using Moq;
using NUnit.Framework;

namespace BugManagemnet.WebAPI.Test
{
    [TestFixture]
    public class BugTypeControllerTest
    {
        private Mock<IBugTypeLogic> _bugTypeLogicMock;
        private BugTypeController _bugTypeController;

        [SetUp]
        public void Init()
        {
            _bugTypeLogicMock = new Mock<IBugTypeLogic>();
            _bugTypeController = new BugTypeController(_bugTypeLogicMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _bugTypeLogicMock = null;
            _bugTypeController = null;
        }

        [Test]
        public void GetAllBugType_should_call_get_all_bugType_method_of_bugType_logic_once_return_BugTypeViewModel_List()
        {
            //Arange
            var bugTypeLogicModel = new BugTypeLogicModel()
            {
                BugTypeId=1,
                Name="test",
                Status="test"
            };

            var bugTypeLogicModels = new List<BugTypeLogicModel> { bugTypeLogicModel };
            _bugTypeLogicMock.Setup(n => n.GetAll()).Returns(bugTypeLogicModels);

            //Act
            var result = _bugTypeController.GetAllBugType();

            //Assert
            _bugTypeLogicMock.Verify(n => n.GetAll(),Times.Once);
            Assert.IsNotNull(result);
        }
    }
}
