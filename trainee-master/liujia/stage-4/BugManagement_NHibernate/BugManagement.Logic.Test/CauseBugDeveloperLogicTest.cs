using System.Collections.Generic;
using System.Linq;
using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.Logic;
using BugManagement.Logic.Models;
using Moq;
using NUnit.Framework;

namespace BugManagement.Logic.Test
{
    [TestFixture]
    public class CauseBugDeveloperLogicTest
    {
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactotyMock;
        private Mock<ICauseBugDeveloperRepository> _causeBugDeveloperRepositoryMock;
        private CauseBugDeveloperLogic _causeBugDeveloperLogic;
        private Mock<IUnitOfWork> _uniteOfWorkMock;

        [SetUp]
        public void Init()
        {
            _unitOfWorkFactotyMock = new Mock<IUnitOfWorkFactory>();
            _causeBugDeveloperRepositoryMock = new Mock<ICauseBugDeveloperRepository>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();

            _causeBugDeveloperLogic = new CauseBugDeveloperLogic(_causeBugDeveloperRepositoryMock.Object, _unitOfWorkFactotyMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _unitOfWorkFactotyMock = null;
            _causeBugDeveloperRepositoryMock = null;
            _causeBugDeveloperLogic = null;
            _uniteOfWorkMock = null;
        }

        [Test]
        public void Create_should_call_create_method_of_causeBugDeveloper_repository_once()
        {
            //Arange
            var causeBugDeveloperLogicModel = new CauseBugDeveloperLogicModel()
            {
                Id = 1,
                BugId = 2,
                DeveloperId=3
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _causeBugDeveloperRepositoryMock.Setup(n => n.Create(It.IsAny<CauseBugDeveloper>()));

            //Act
            _causeBugDeveloperLogic.Create(causeBugDeveloperLogicModel);

            //Assert
            _causeBugDeveloperRepositoryMock.Verify(n=>n.Create(It.IsAny<CauseBugDeveloper>()),Times.Once);
        }

        [Test]
        public void Edit_should_call_edit_method_of_causeBugDeveloper_repository_once()
        {
            //Arange
            var causeBugDeveloperLogicModel = new CauseBugDeveloperLogicModel()
            {
                Id = 1,
                BugId = 2,
                DeveloperId = 3
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _causeBugDeveloperRepositoryMock.Setup(n => n.Edit(It.IsAny<CauseBugDeveloper>()));

            //Act
            _causeBugDeveloperLogic.Edit(causeBugDeveloperLogicModel);

            //Assert
            _causeBugDeveloperRepositoryMock.Verify(n => n.Edit(It.IsAny<CauseBugDeveloper>()), Times.Once);
        }

        [Test]
        public void Delete_should_call_delete_method_of_causeBugDeveloper_repository_once()
        {
            //Arange
            var id = 1;
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _causeBugDeveloperRepositoryMock.Setup(n => n.Delete(It.IsAny<int>()));

            //Act
            _causeBugDeveloperLogic.Delete(id);

            //Assert
            _causeBugDeveloperRepositoryMock.Verify(n => n.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_menthod_of_causeBugDeveloper_repository_once_return_causeBugDeveloperLogicModel()
        {
            //Arange
            var id = 1;
            var causeBugDeveloper = new CauseBugDeveloper()
            {
                Id = 1,
                BugId = 2,
                DeveloperId = 3
            };
            _causeBugDeveloperRepositoryMock.Setup(n => n.Get(It.IsAny<int>())).Returns(causeBugDeveloper);

            //Act
            var result = _causeBugDeveloperLogic.Get(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.BugId);
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_causeBugDeveloper_repository_once_return_causeBugDeveloperLogicModel_list()
        {
            //Arange
            var causeBugDeveloper = new CauseBugDeveloper()
            {
                Id = 1,
                BugId = 2,
                DeveloperId = 3
            };
            var causeBugDevelopers = new List<CauseBugDeveloper>() { causeBugDeveloper };
            _causeBugDeveloperRepositoryMock.Setup(n => n.Query()).Returns(causeBugDevelopers.AsQueryable());

            //Act
            var result = _causeBugDeveloperLogic.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetCauseBugDeveloperByBugId_should_call_get_causeBugDeveloper_by_bugId_method_of_causeBugDeveloper_repository_once_return_causeBugDeveloperLogicModel_list()
        {
            //Arange
            var bugId = 2;
            var causeBugDeveloper = new CauseBugDeveloper()
            {
                Id = 1,
                BugId = 2,
                DeveloperId = 3
            };
            var causeBugDevelopers = new List<CauseBugDeveloper>() { causeBugDeveloper };
            _causeBugDeveloperRepositoryMock.Setup(n => n.Query()).Returns(causeBugDevelopers.AsQueryable());

            //Act
            var result = _causeBugDeveloperLogic.GetCauseBugDeveloperByBugId(bugId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
    }
}
