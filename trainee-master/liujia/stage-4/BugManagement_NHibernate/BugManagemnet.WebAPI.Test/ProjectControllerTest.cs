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
    public class ProjectControllerTest
    {
        private Mock<IProjectLogic> _projectLogicMock;
        private ProjectController _projectController;
        private ProjectLogicModel _projectLogicModel;
        private List<ProjectLogicModel> _projectLogicModels;

        [SetUp]
        public void Init()
        {
            _projectLogicMock = new Mock<IProjectLogic>();
            _projectController = new ProjectController(_projectLogicMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _projectLogicMock = null;
            _projectController = null;
            _projectLogicModel = null;
            _projectLogicModels = null;
        }
        
        [Test]
        public void GetProjectListViewModelByCondition_should_call_get_projectListViewModel_by_condition_method_of_project_logic_once_return_projectListViewModel_if_condition_is_not_exist()
        {
            //Arange
            var strCondition = "aa";
            var strPageIndex = "1";
            var count = 0;
            _projectLogicMock.Setup(n => n.GetPageCountByCondition(It.IsAny<string>())).Returns(count);

            //Act
            var result = _projectController.GetProjectListViewModelByCondition(strCondition, strPageIndex);

            //Assert
            Assert.IsNull(result.Models);
            Assert.IsNull(result.Pages);
        }
        [Test]
        public void GetProjectListViewModelByCondition_should_call_get_projectListViewModel_by_condition_method_of_project_logic_once_return_projectListViewModel_if_condition_is_exist()
        {
            //Arange
            var strCondition = "test";
            var strPageIndex = "1";
            var count = 1;
            _projectLogicModel = new ProjectLogicModel()
            {
                ProjectId = 1,
                Description = "test",
                ProjectName = "test ProjectName",
                ContactEmail = "Email@qq.com",
                MainContact = "test",
                StartTime = DateTime.Now
            };
            _projectLogicModels = new List<ProjectLogicModel> { _projectLogicModel };
            _projectLogicMock.Setup(n => n.GetPageCountByCondition(It.IsAny<string>())).Returns(count);
            _projectLogicMock.Setup(n => n.GetProjectLogicModelsByCondition(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(_projectLogicModels);

            //Act
            var result = _projectController.GetProjectListViewModelByCondition(strCondition, strPageIndex);

            //Assert
            Assert.IsNotNull(result.Models);
            Assert.AreEqual(1, result.Models.Count);
            Assert.IsNotNull(result.Pages);
        }
        
        [Test]
        public void GetProjects_should_call_get_all_projects_method_of_project_logic_once_return_projectListViewModel()
        {
            //Arange
            _projectLogicModel = new ProjectLogicModel()
            {
                ProjectId = 1,
                Description = "test",
                ProjectName = "test ProjectName",
                ContactEmail = "Email@qq.com",
                MainContact = "test",
                StartTime = DateTime.Now
            };
            _projectLogicModels = new List<ProjectLogicModel> { _projectLogicModel };
            _projectLogicMock.Setup(n => n.GetAll()).Returns(_projectLogicModels);

            //Act
            var result = _projectController.GetProjects();

            //Assert
            _projectLogicMock.Verify(n => n.GetAll(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
        
        [Test]
        public void GetProjectById_should_call_get_project_by_Id_method_of_project_logic_once_return_projectViewModel()
        {
            //Arange
            var projectId = 1;
            _projectLogicModel = new ProjectLogicModel()
            {
                ProjectId = 1,
                Description = "test",
                ProjectName = "test ProjectName",
                ContactEmail = "Email@qq.com",
                MainContact = "test",
                StartTime = DateTime.Now
            };
            _projectLogicMock.Setup(n => n.Get(It.IsAny<int>())).Returns(_projectLogicModel);

            //Act
            var result = _projectController.GetProjectById(projectId);

            //Assert
            _projectLogicMock.Verify(n => n.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Description);
            Assert.AreEqual("test", result.MainContact);
        }
        
        [Test]
        public void DeleteProjectById_should_call_delete_project_by_Id_method_of_project_logic_once_()
        {
            //Arange
            var projectId = 1;

            //Act
            _projectController.DeleteProjectById(projectId);

            //Assert
            _projectLogicMock.Verify(n => n.Delete(It.IsAny<int>()), Times.Once);
        }
        
        [Test]
        public void UpdateProject_should_call_update_project_method_of_project_logic_once()
        {
            //Arange
            var projectViewModel = new ProjectViewModel()
            {
                ProjectId = 1,
                Description = "test",
                ProjectName = "test ProjectName",
                ContactEmail = "Email@qq.com",
                MainContact = "test",
                StartTime = DateTime.Now
            };

            //Act
            _projectController.UpdateProject(projectViewModel);

            //Assert
            _projectLogicMock.Verify(n => n.Edit(It.IsAny<ProjectLogicModel>()), Times.Once);
        }
        
        [Test]
        public void CreateProject_should_call_create_project_method_of_project_logic_once()
        {
            //Arange
            var projectViewModel = new ProjectViewModel()
            {
                ProjectId = 1,
                Description = "test",
                ProjectName = "test ProjectName",
                ContactEmail = "Email@qq.com",
                MainContact = "test",
                StartTime = DateTime.Now
            };

            //Act
            _projectController.CreateProject(projectViewModel);

            //Assert
            _projectLogicMock.Verify(n => n.Create(It.IsAny<ProjectLogicModel>()), Times.Once);
        }
        
        [Test]
        public void CheckProjectNameIsExist_should_call_check_projectName_is_exist_method_of_project_logic_once_return_false_if_projectName_is_not_exist()
        {
            //Arange
            var strProjectName = "";
            var strProjectId = "";
            _projectLogicMock.Setup(n => n.CheckExist(It.IsAny<string>(),It.IsAny<string>())).Returns(false);

            //Act
            var result = _projectController.CheckProjectNameIsExist(strProjectName, strProjectId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckProjectNameIsExist_should_call_check_projectName_is_exist_method_of_project_logic_once_return_true_if_projectName_is_exist()
        {
            //Arange
            var strProjectName = "test ProjectName";
            var strProjectId = "";

            _projectLogicMock.Setup(n => n.CheckExist(It.IsAny<string>(),It.IsAny<string>())).Returns(true);

            //Act
            var result = _projectController.CheckProjectNameIsExist(strProjectName, strProjectId);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
