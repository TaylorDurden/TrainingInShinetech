using System;
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
    public class BugControllerTest
    {
        private Mock<IBugLogic> _bugLogicMock;
        private BugController _bugController;
        private BugLogicModel _bugLogicModel;
        private List<BugLogicModel> _bugLogicModels;


        [SetUp]
        public void Init()
        {
            _bugLogicMock = new Mock<IBugLogic>();
            _bugController = new BugController(_bugLogicMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _bugLogicModel = null;
            _bugLogicModels = null;
            _bugLogicMock = null;
            _bugController = null;
        }
        
        [Test]
        public void CreateBug_should_call_create_bug_method_of_bug_logic_once()
        {
            //Arange
            var bugCommand = new BugCommand
            {
                BugViewModel = new BugViewModel(),
                DeveloperViewModels = new List<DeveloperViewModel>()
            };

            //Act
            _bugController.CreateBug(bugCommand);

            //Assert
            _bugLogicMock.Verify(n => n.Create(It.IsAny<BugLogicModel>(), It.IsAny<List<DeveloperLogicModel>>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void UpdateBug_should_call_update_bug_method_of_bug_logic_once()
        {
            //Arange
            var bugCommand = new BugCommand
            {
                BugViewModel = new BugViewModel(),
                DeveloperViewModels = new List<DeveloperViewModel>()
            };

            //Act
            _bugController.UpdateBug(bugCommand);

            //Assert
            _bugLogicMock.Verify(n => n.Edit(It.IsAny<BugLogicModel>(), It.IsAny<List<DeveloperLogicModel>>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void GetBugListViewModelByCondition_should_call_get_bugListViewModel_by_condition_method_of_bug_logic_once_return_bugListViewModel_if_condition_is_not_exist()
        {
            //Arange
            var strCondition = "aa";
            var strPageIndex = "1";
            var count = 0;
            _bugLogicMock.Setup(n => n.GetPageCountByCondition(It.IsAny<string>())).Returns(count);

            //Act
            var result = _bugController.GetBugListViewModelByCondition(strCondition, strPageIndex);

            //Assert
            Assert.IsNull(result.Models);
            Assert.IsNull(result.Pages);
        }
        [Test]
        public void GetBugListViewModelByCondition_should_call_get_bugListViewModel_by_condition_method_of_bug_logic_once_return_bugListViewModel_if_condition_is_exist()
        {
            //Arange
            var strCondition = "aa";
            var strPageIndex = "1";
            var count = 1;
            _bugLogicModel = new BugLogicModel()
            {
                BugId = 1,
                Createtime = DateTime.Now,
                Creator = "test liu",
                Description = "test",
                DeveloperId = 1,
                ProjectId = 1,
                Smmary = "test",
                Status = "InTest",
                StrDevelopers = "",
                StrDocuments = "",
                Title = "test title",
                Type = 1
            };
            _bugLogicModels = new List<BugLogicModel> { _bugLogicModel };
            _bugLogicMock.Setup(n => n.GetPageCountByCondition(It.IsAny<string>())).Returns(count);
            _bugLogicMock.Setup(n => n.GetBugLogicModelsByCondition(It.IsAny<string>(),It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>())).Returns(_bugLogicModels);

            //Act
            var result = _bugController.GetBugListViewModelByCondition(strCondition, strPageIndex);

            //Assert
            Assert.IsNotNull(result.Models);
            Assert.IsNotNull(result.Pages);
            Assert.AreEqual(1,result.Models.Count);
        }
        
        [Test]
        public void GetDashboardViewModelByCondition_should_call_get_dashboardViewModel_by_condition_method_of_bug_logic_noce_return_dashboardViewModel_if_condition_is_not_exist()
        {
            //Arange
            var strCondition = "aa";

            //Act
            var result = _bugController.GetDashboardViewModelByCondition(strCondition);

            //Assert
            Assert.IsNull(result.InTestBugList);
            Assert.IsNull(result.AssignedBugList);

        }

        [Test]
        public void GetDashboardViewModelByCondition_should_call_get_dashboardViewModel_by_condition_method_of_bug_logic_noce_return_dashboardViewModel_if_condition_is_exist()
        {
            //Arange
            var strCondition = "test";
            _bugLogicModel = new BugLogicModel()
            {
                BugId = 1,
                Createtime = DateTime.Now,
                Creator = "test liu",
                Description = "test",
                DeveloperId = 1,
                ProjectId = 1,
                Smmary = "test",
                Status = "InTest",
                StrDevelopers = "",
                StrDocuments = "",
                Title = "test title",
                Type = 1
            };
            _bugLogicModels = new List<BugLogicModel> { _bugLogicModel };
            _bugLogicMock.Setup(n => n.GetBugLogicModelsBySerchCondition(It.IsAny<string>())).Returns(_bugLogicModels);

            //Act
            var result = _bugController.GetDashboardViewModelByCondition(strCondition);

            //Assert
            _bugLogicMock.Verify(n => n.GetBugLogicModelsBySerchCondition(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result.InTestBugList);
            Assert.AreEqual(1, result.InTestBugList.Count);
            Assert.IsNull(result.AssignedBugList);
        }
        
        [Test]
        public void UpdateBugStatus_should_call_update_bugStatus_method_of_bug_logic_once_return_false_if_bugId_or_status_is_empty()
        {
            //Arange
            var strBugId = "";
            var status = "";

            //Act
            var result = _bugController.UpdateBugStatus(strBugId, status);

            //Assert
            Assert.IsFalse(result);
            _bugLogicMock.Verify(n => n.UpdateBugStatus(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void UpdateBugStatus_should_call_update_bugStatus_method_of_bug_logic_once_return_false_if_bugId_is_not_numeric_type()
        {
            //Arange
            var strBugId = "aaa";
            var status = "aaa";

            //Act
            var result = _bugController.UpdateBugStatus(strBugId, status);

            //Assert
            Assert.IsFalse(result);
            _bugLogicMock.Verify(n => n.UpdateBugStatus(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void UpdateBugStatus_should_call_update_bugStatus_method_of_bug_logic_once_return_true()
        {
            //Arange
            var strBugId = "1";
            var status = "test";

            //Act
            var result = _bugController.UpdateBugStatus(strBugId, status);

            //Assert
            Assert.IsTrue(result);
            _bugLogicMock.Verify(n=>n.UpdateBugStatus(It.IsAny<int>(),It.IsAny<string>()),Times.Once);
        }
        
        [Test]
        public void GetBugById_should_call_get_bug_by_Id_method_of_bug_logic_once_return_bugViewModel()
        {
            //Arange
            _bugLogicModel = new BugLogicModel()
            {
                BugId = 1,
                Createtime = DateTime.Now,
                Creator = "test liu",
                Description = "test",
                DeveloperId = 1,
                ProjectId = 1,
                Smmary = "test",
                Status = "test",
                StrDevelopers = "",
                StrDocuments = "",
                Title = "test title",
                Type = 1
            };
            _bugLogicMock.Setup(n => n.Get(_bugLogicModel.BugId)).Returns(_bugLogicModel);

            //Act
            var result = _bugController.GetBugById(_bugLogicModel.BugId);

            //Assert
            _bugLogicMock.Verify(n => n.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreNotSame(_bugLogicModel, result);
            Assert.AreEqual(1, result.BugId);
            Assert.AreEqual("test", result.Status);
        }
        
        [Test]
        public void DeleteBugById_should_call_delete_bug_by_Id_method_of_bug_logic_once_()
        {
            //Arange
            var bugId = 1;

            //Act
            _bugController.DeleteBugById(bugId);

            //Assert
            _bugLogicMock.Verify(n => n.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
