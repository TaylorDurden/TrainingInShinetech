using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebAPI.Tests.Controller
{
    [TestFixture]
    public class ProjectControllerTests
    {
        private AutoMoqer _mocker;
        private ProjectController _projectController;

        [SetUp]
        public void Initialize()
        {
            _mocker=new AutoMoqer();
            _projectController = _mocker.Create<ProjectController>();
        }

        [TearDown]
        public void Dispose()
        {
            _mocker = null;
            _projectController.Dispose();
        }

        
        [Test]
        public void Create_when_projectLogicModel_is_null_should_never_execute_and_return_is_null()
        {
            // Arrange
            var projectLogic = _mocker.GetMock<IProjectLogic>();
            // Act
            var result = _projectController.Create(null);
            // Assert
            projectLogic.Verify(x => x.Create(null), Times.Never());
            Assert.IsNull(result);
        }

        [Test]
        public void Create_when_projectLogicModel_is_not_null_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1,
                Name = "Project1",
                Message = ""
            };
            var projectLogic = _mocker.GetMock<IProjectLogic>();
            projectLogic.Setup(x => x.Create(It.IsAny<ProjectLogicModel>())).Returns(projectLogicModel);
            // Act
            var result = _projectController.Create(projectLogicModel);
            // Assert
            projectLogic.Verify(x => x.Create(projectLogicModel), Times.Once());
            Assert.AreEqual(typeof(ProjectViewModel),result.GetType());
            Assert.AreEqual(projectLogicModel.Id,result.Id);
            Assert.AreEqual(projectLogicModel.Name, result.Name);
            Assert.AreEqual(projectLogicModel.Message, result.Message);
        }
        

        
        [Test]
        public void Delete_should_execute_once()
        {
            // Arrange
            var projectLogic = _mocker.GetMock<IProjectLogic>();
            // Act
            _projectController.Delete(It.IsAny<int>());
            // Assert
            projectLogic.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }
        

        

        [Test]
        public void Edit_when_projectLogicModel_not_null_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1,
                Name = "Project1",
                Message = ""
            };
            var projectLogic = _mocker.GetMock<IProjectLogic>();
            projectLogic.Setup(x => x.Edit(projectLogicModel)).Returns(projectLogicModel);
            // Act
            var result = _projectController.Edit(projectLogicModel);
            // Assert
            projectLogic.Verify(x => x.Edit(projectLogicModel), Times.Once());
            Assert.AreEqual(typeof(ProjectViewModel), result.GetType());
            Assert.AreEqual(projectLogicModel.Id, result.Id);
            Assert.AreEqual(projectLogicModel.Name, result.Name);
            Assert.AreEqual(projectLogicModel.Message, result.Message);
        }

        [Test]
        public void Edit_when_projectLogicModel_is_null_should_return_null()
        {
            // Arrange
            var projectLogic = _mocker.GetMock<IProjectLogic>();
            // Act
            var result = _projectController.Edit(null);
            // Assert
            projectLogic.Verify(x => x.Edit(null), Times.Once());
            Assert.IsNull(result);
        }
        

        
        [Test]
        public void GetAllProjects_should_execute_once_and_return_projects_count()
        {
            // Arrange
            var projects = new List<ProjectLogicModel>
            {
                new ProjectLogicModel()
                {
                    Id = 1,
                    Name = "Project1",
                    Message = ""
                }
            };
            var projectLogic = _mocker.GetMock<IProjectLogic>();
            projectLogic.Setup(x => x.GetAll()).Returns(projects);
            // Act
            var result = _projectController.GetAllProjects();

            // Assert
            projectLogic.Verify(x => x.GetAll(), Times.Once());
            result.Count().Should().Be(projects.Count);
        }

        [Test]
        public void GetProjectById_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var projectLogicModel = new ProjectLogicModel()
            {
                Id = 1,
                Name = "Project1",
                Message = ""
            };
            var projectLogic = _mocker.GetMock<IProjectLogic>();
            projectLogic.Setup(x => x.Get(It.IsAny<int>())).Returns(projectLogicModel);
            // Act
            var result = _projectController.GetProjectById(It.IsAny<int>());
            // Assert
            projectLogic.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
            Assert.AreEqual(typeof(ProjectViewModel), result.GetType());
            Assert.AreEqual(projectLogicModel.Id, result.Id);
            Assert.AreEqual(projectLogicModel.Name, result.Name);
            Assert.AreEqual(projectLogicModel.Message, result.Message);
        }

        [Test]
        public void GetProjectByName_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var projectLogicModel = new ProjectLogicModel()
            {
                Id = 1,
                Name = "Project1",
                Message = ""
            };
            var projectLogic = _mocker.GetMock<IProjectLogic>();
            projectLogic.Setup(x => x.Get(It.IsAny<string>())).Returns(projectLogicModel);
            // Act
            var result = _projectController.GetProjectByName(It.IsAny<string>());
            // Assert
            projectLogic.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(typeof(ProjectViewModel), result.GetType());
            Assert.AreEqual(projectLogicModel.Id, result.Id);
            Assert.AreEqual(projectLogicModel.Name, result.Name);
            Assert.AreEqual(projectLogicModel.Message, result.Message);
        }
        
    }
}
