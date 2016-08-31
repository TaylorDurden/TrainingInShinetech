using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebAPI.Tests
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private Mock<IProjectLogic> _projectLogicMock;
        private ProjectController _projectController;
        private ProjectViewModel _projectViewModel;
        private ProjectLogicModel _projectLogicModel;

        [SetUp]
        public void Init()
        {
            _projectLogicMock = new Mock<IProjectLogic>();
            _projectController = new ProjectController(_projectLogicMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _projectLogicMock = null;
            _projectController = null;
        }

        [Test]
        public void Create_should_return_logic_model_that_equals_to_view_model()
        {
            //Arrange
            _projectViewModel = new ProjectViewModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };
            _projectLogicModel = new ProjectLogicModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };

            _projectLogicMock.Setup(p => p.Create(It.IsAny<ProjectLogicModel>())).Returns(_projectLogicModel).Verifiable();
            //Act
            var actual = _projectController.Create(_projectViewModel);
            //Assert
            Assert.AreEqual(_projectLogicModel.Id, actual.Id);
            Assert.AreEqual(_projectLogicModel.Name, actual.Name);
            Assert.AreEqual(_projectLogicModel.ProjectGuid, actual.ProjectGuid);
        }

        [Test]
        public void Delete_should_execute_once()
        {
            //Arrange
            int id = 10;
            //Act
            _projectController.Delete(id);
            //Assert
            _projectLogicMock.Verify(p => p.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Edit_should_execute_once()
        {
            //Arrange
            _projectViewModel = new ProjectViewModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };
            _projectLogicModel = new ProjectLogicModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };
            _projectLogicMock.Setup(p => p.Edit(It.IsAny<ProjectLogicModel>()));
            //Act
            _projectController.Edit(_projectViewModel);
            //Assert
            _projectLogicMock.Verify(p => p.Edit(It.IsAny<ProjectLogicModel>()), Times.Once);
        }

        [Test]
        public void GetAll_should_return_same_count()
        {
            //Arrange
            _projectLogicModel = new ProjectLogicModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };
            List<ProjectLogicModel> projectLogicModels = new List<ProjectLogicModel> {_projectLogicModel};

            _projectLogicMock.Setup(p => p.GetAll()).Returns(projectLogicModels).Verifiable();
            //Act
            var actual = _projectController.GetAll();
            //Assert
            Assert.AreEqual(projectLogicModels.Count(), actual.Count());
        }

        [Test]
        public void GetProjectById_should_return_a_ProjectViewModel()
        {
            //Arrange
            _projectViewModel = new ProjectViewModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };
            _projectLogicModel = new ProjectLogicModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };

            int id = 10;

            _projectLogicMock.Setup(p => p.Get(It.IsAny<int>())).Returns(_projectLogicModel);
            //Act
            var actual = _projectController.GetProjectById(id);
            //Assert
            Assert.AreEqual(typeof(ProjectViewModel), actual.GetType());
        }

        [Test]
        public void GetProjectUrlById_should_return_an_url_by_id()
        {
            //Arrange
            _projectLogicModel = new ProjectLogicModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };
            string url = "/views/ProjectEstimate.html?presenter=1&projectGuid=";
            _projectLogicMock.Setup(p => p.Get(It.IsAny<int>())).Returns(_projectLogicModel);

            string expectedUrl = url + _projectLogicModel.ProjectGuid;
            //Act
            var actual = _projectController.GetProjectUrlById(10);
            //Assert
            Assert.AreEqual(expectedUrl, actual);
        }

        [Test]
        public void GetProjectByName_should_return_same_count_as_expected()
        {
            //Arrange
            _projectLogicModel = new ProjectLogicModel()
            {
                Id = "12",
                Name = "123",
                ProjectGuid = Guid.NewGuid().ToString()
            };

            List<ProjectLogicModel> projectLogicModels = new List<ProjectLogicModel> { _projectLogicModel };

            _projectLogicMock.Setup(p => p.Get(It.IsAny<string>())).Returns(projectLogicModels);
            //Act
            var actual = _projectController.GetProjectByName("123");
            //Assert
            Assert.AreEqual(projectLogicModels.Count(), actual.Count());
        }

        [Test]
        public void CheckExist_should_return_bool()
        {
            //Arrange
            string projectName = "123";

            _projectLogicMock.Setup(p => p.CheckExist(It.IsAny<string>())).Returns(true);
            //Act
            var actual = _projectController.CheckExist(projectName);
            //Assert
            Assert.AreEqual(true, actual);
        }
    }
}
