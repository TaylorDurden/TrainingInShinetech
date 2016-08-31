using System.Collections.Generic;
using BugManagement.Logic.ILogic;
using BugManagement.Logic.Models;
using BugManagemnet.WebAPI.Controllers;
using BugManagemnet.WebAPI.Models;
using Moq;
using NUnit.Framework;

namespace BugManagemnet.WebAPI.Test
{
    [TestFixture]
    public class DeveloperControllerTest
    {
        private Mock<IDeveloperLogic> _developerLogicMock;
        private Mock<ICauseBugDeveloperLogic> _causeBugDeveloperLogicMock;
        private DeveloperController _developerController;
        private DeveloperLogicModel _developerLogicModel;
        private List<DeveloperLogicModel> _developerLogicModels;

        [SetUp]
        public void Init()
        {
            _developerLogicMock = new Mock<IDeveloperLogic>();
            _causeBugDeveloperLogicMock = new Mock<ICauseBugDeveloperLogic>();
            _developerController = new DeveloperController(_developerLogicMock.Object, _causeBugDeveloperLogicMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _developerLogicMock = null;
            _causeBugDeveloperLogicMock = null;
            _developerController = null;
            _developerLogicModel = null;
            _developerLogicModels = null;
        }
        
        [Test]
        public void GetDeveloperListViewModelByCondition_should_call_get_developerListViewModel_by_condition_method_of_developer_logic_once_return_developerListViewModel_if_condition_is_not_exist()
        {
            //Arange
            var strCondition = "aa";
            var strPageIndex = "1";
            var count = 0;
            _developerLogicMock.Setup(n => n.GetPageCountByCondition(It.IsAny<string>())).Returns(count);

            //Act
            var result = _developerController.GetDeveloperListViewModelByCondition(strCondition, strPageIndex);

            //Assert
            _developerLogicMock.Verify(n => n.GetPageCountByCondition(It.IsAny<string>()), Times.Once);
            Assert.IsNull(result.Models);
            Assert.IsNull(result.Pages);
        }

        [Test]
        public void GetDeveloperListViewModelByCondition_should_call_get_developerListViewModel_by_condition_method_of_developer_logic_once_return_developerListViewModel_if_condition_is_exist()
        {
            //Arange
            var strCondition = "Name";
            var strPageIndex = "1";
            var count = 1;
            _developerLogicModel = new DeveloperLogicModel()
            {
                DeveloperId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastName",
                Status = "status",
                UserId = 1
            };
            _developerLogicModels = new List<DeveloperLogicModel>() { _developerLogicModel };
            _developerLogicMock.Setup(n => n.GetPageCountByCondition(It.IsAny<string>())).Returns(count);
            _developerLogicMock.Setup(n => n.GetDeveloperLogicModelsByCondition(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(_developerLogicModels);

            //Act
            var result = _developerController.GetDeveloperListViewModelByCondition(strCondition, strPageIndex);

            //Assert
            _developerLogicMock.Verify(n => n.GetPageCountByCondition(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result.Models);
            Assert.IsNotNull(result.Pages);
            Assert.AreEqual(1, result.Models.Count);
        }
        
        [Test]
        public void GetDeveloperViewModel_should_call_get_developerViewModel_method_of_developer_logic_once_return_developerListViewModel()
        {
            //Arange
            _developerLogicModel = new DeveloperLogicModel()
            {
                DeveloperId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastName",
                Status = "status",
                UserId = 1
            };
            _developerLogicModels = new List<DeveloperLogicModel>() { _developerLogicModel };
            _developerLogicMock.Setup(n => n.GetAll()).Returns(_developerLogicModels);

            //Act
            var result = _developerController.GetDeveloperViewModel();

            //Assert
            _developerLogicMock.Verify(n => n.GetAll(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
        
        [Test]
        public void GetDeveloperViewModelByBugId_should_call_get_developerViewModel_by_bugId_method_of_developer_logic_once_return_developerListViewModel()
        {
            //Arange
            var strBugId = "3";
            _developerLogicModel = new DeveloperLogicModel()
            {
                DeveloperId = 2,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastName",
                Status = "status",
                UserId = 1
            };
            var causeBugDeveloperLogicModel = new CauseBugDeveloperLogicModel()
            {
                Id = 1,
                DeveloperId = 2,
                BugId =3
            };
            var causeBugDeveloperLogicModels = new List<CauseBugDeveloperLogicModel>() {causeBugDeveloperLogicModel};
            _causeBugDeveloperLogicMock.Setup(n => n.GetCauseBugDeveloperByBugId(It.IsAny<int>())).Returns(causeBugDeveloperLogicModels);
            _developerLogicMock.Setup(n => n.Get(It.IsAny<int>())).Returns(_developerLogicModel);
            
            //Act
            var result = _developerController.GetDeveloperViewModelByBugId(strBugId);

            //Assert
            _causeBugDeveloperLogicMock.Verify(n => n.GetCauseBugDeveloperByBugId(It.IsAny<int>()),Times.Once);
            _developerLogicMock.Verify(n => n.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
        
        [Test]
        public void GetDeveloperById_should_call_get_developer_by_Id_method_of_developer_logic_once_return_developerViewModel()
        {
            //Arange
            var developerId = 1;
            _developerLogicModel = new DeveloperLogicModel()
            {
                DeveloperId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastName",
                Status = "status",
                UserId = 1
            };

            _developerLogicMock.Setup(n => n.Get(It.IsAny<int>())).Returns(_developerLogicModel);

            //Act
            var result = _developerController.GetDeveloperById(developerId);

            //Assert
            _developerLogicMock.Verify(n => n.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Email@qq.com", result.Email);
            Assert.AreEqual("status", result.Status);
        }
        
        [Test]
        public void DeleteDeveloperById_should_call_delete_developer_by_Id_method_of_developer_logic_once()
        {
            //Arange
            var developerId = 1;

            //Act
            _developerController.DeleteDeveloperById(developerId);

            //Assert
            _developerLogicMock.Verify(n => n.Delete(It.IsAny<int>()), Times.Once);
        }
        
        [Test]
        public void UpdateDeveloper_should_call_update_developer_method_of_developer_logic_once()
        {
            //Arange
            var developerViewModel = new DeveloperViewModel()
            {
                DeveloperId = 1,
                UserId = 1,
                FristName = "fristName",
                LastName = "lastName",
                UserName = "userName",
                Email = "Email@qq.com",
                Status = "status"
            };

            //Act
            _developerController.UpdateDeveloper(developerViewModel);

            //Assert
            _developerLogicMock.Verify(n => n.Edit(It.IsAny<DeveloperLogicModel>()), Times.Once);
        }
        
        [Test]
        public void CreateDeveloper_should_call_create_developer_method_of_developer_logic_once()
        {
            //Arange
            var developerViewModel = new DeveloperViewModel()
            {
                DeveloperId = 1,
                UserId = 1,
                FristName = "fristName",
                LastName = "lastName",
                UserName = "userName",
                Email = "Email@qq.com",
                Status = "status"
            };

            //Act
            _developerController.CreateDeveloper(developerViewModel);

            //Assert
            _developerLogicMock.Verify(n => n.Create(It.IsAny<DeveloperLogicModel>()), Times.Once);
        }
    }
}
