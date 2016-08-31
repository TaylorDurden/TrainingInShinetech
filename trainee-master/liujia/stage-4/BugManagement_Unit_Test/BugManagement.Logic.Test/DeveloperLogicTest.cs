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
    public class DeveloperLogicTest
    {
        private Mock<IDeveloperRepository> _developerRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactotyMock;
        private DeveloperLogic _developerLogic;
        private Mock<IUnitOfWork> _uniteOfWorkMock;

        [SetUp]
        public void Init()
        {
            _developerRepositoryMock = new Mock<IDeveloperRepository>();
            _unitOfWorkFactotyMock = new Mock<IUnitOfWorkFactory>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();

            _developerLogic = new DeveloperLogic(_developerRepositoryMock.Object, _unitOfWorkFactotyMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _developerRepositoryMock = null;
            _unitOfWorkFactotyMock = null;
            _uniteOfWorkMock = null;
            _developerLogic = null;
        }

        [Test]
        public void Create_should_call_create_method_of_developer_repository_once()
        {
            //Arange
            var developerLogicModel = new DeveloperLogicModel()
            {
                DeveloperId = 1,
                Email="email",
                FristName="fristname",
                LastName="lastname",
                Status="status",
                UserId=2
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _developerRepositoryMock.Setup(n => n.Create(It.IsAny<Developer>()));

            //Act
            _developerLogic.Create(developerLogicModel);

            //Assert
            _developerRepositoryMock.Verify(n => n.Create(It.IsAny<Developer>()), Times.Once);
        }

        [Test]
        public void Edit_should_call_edit_method_of_developer_repository_once()
        {
            //Arange
            var developerLogicModel = new DeveloperLogicModel()
            {
                DeveloperId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Status = "status",
                UserId = 2
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _developerRepositoryMock.Setup(n => n.Edit(It.IsAny<Developer>()));

            //Act
            _developerLogic.Edit(developerLogicModel);

            //Assert
            _developerRepositoryMock.Verify(n => n.Edit(It.IsAny<Developer>()), Times.Once);
        }

        [Test]
        public void Delete_should_call_delete_method_of_developer_repository_once()
        {
            //Arange
            var developerId = 1;
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _developerRepositoryMock.Setup(n => n.Delete(It.IsAny<int>()));

            //Act
            _developerLogic.Delete(developerId);

            //Assert
            _developerRepositoryMock.Verify(n => n.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_menthod_of_developer_repository_once_return_developerLogicModel()
        {
            //Arange
            var developerId = 1;
            var developer = new Developer()
            {
                DeveloperId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Status = "status",
                UserId = 2
            };
            _developerRepositoryMock.Setup(n => n.Get(It.IsAny<int>())).Returns(developer);

            //Act
            var result = _developerLogic.Get(developerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("email", result.Email);
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_developer_repository_once_return_developerLogicModel_list()
        {
            //Arange
            var developer = new Developer()
            {
                DeveloperId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Status = "status",
                UserId = 2
            };
            var developers = new List<Developer>() { developer };
            _developerRepositoryMock.Setup(n => n.Query()).Returns(developers.AsQueryable());

            //Act
            var result = _developerLogic.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetPageCountByCondition_should_call_get_pageCount_by_condition_method_of_developer_repository_once_return_pageCount()
        {
            //Arange
            var strSerchCondition = "";
            var developer = new Developer()
            {
                DeveloperId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Status = "status",
                UserId = 2
            };
            var developers = new List<Developer>() { developer };
            _developerRepositoryMock.Setup(n => n.Query()).Returns(developers.AsQueryable());

            //Act
            var result = _developerLogic.GetPageCountByCondition(strSerchCondition);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetDeveloperLogicModelsByCondition_should_call_get_developerLogicModels_by_condition_method_of_developer_repository_once_return_developerLogicModel_list()
        {
            //Arange
            var strSerchCondition = "";
            var pageIndex = 5;
            var pageSize = 3;
            var count = 6;
            var developer = new Developer()
            {
                DeveloperId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Status = "status",
                UserId = 2
            };
            var developers = new List<Developer>() { developer, developer, developer, developer, developer, developer, developer };
            _developerRepositoryMock.Setup(n => n.Query()).Returns(developers.AsQueryable());

            //Act
            var result = _developerLogic.GetDeveloperLogicModelsByCondition(strSerchCondition, pageIndex, pageSize, count);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }
    }
}
