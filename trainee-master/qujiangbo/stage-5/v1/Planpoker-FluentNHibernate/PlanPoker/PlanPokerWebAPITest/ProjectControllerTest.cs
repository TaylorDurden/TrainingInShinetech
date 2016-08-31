using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.WebAPI.Models;
using PlanPoker.WebAPI.Controllers;

namespace PlanPokerWebAPITest
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private Mock<IProjectLogic> _iprojectLogicMock;

        [SetUp]
        public void SetUp()
        {
            _iprojectLogicMock = new Mock<IProjectLogic>();


        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Create_should_create_project()
        {
            //Arrange
            var projectWebModel = new ProjectWebModel
            {
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            //Act
            projectController.Create(projectWebModel);
            //Assert
            _iprojectLogicMock.Verify(x => x.Create(It.IsAny<ProjectLogicModel>()), Times.Once);
        }

        [Test]
        public void Delete_should_delete_project()
        {
            //Arrange
            var id = 1;
            var projectController = new ProjectController(_iprojectLogicMock.Object);            
            //Act
            projectController.Delete(id);
            //Assert
            _iprojectLogicMock.Verify(x => x.Delete(id), Times.Once);
        }

        [Test]
        public void Edit_should_edit_user()
        {
            //Arrange
            var projectWebModelMock = new ProjectWebModel
            {
                Id = 10,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            var projectLogicModel = projectWebModelMock.CreateConvert();
            _iprojectLogicMock.Setup(x => x.Create(projectLogicModel));
            //Act
            projectController.Edit(projectWebModelMock);
            //Assert
            _iprojectLogicMock.Verify(x => x.Edit(It.IsAny<ProjectLogicModel>()), Times.Once);
        }

        [Test]
        public void GetAll_should_get_projects()
        {
            //Arrange
            var projectLogicModes = new List<ProjectLogicModel>();
            var projectLogicModel = new ProjectLogicModel {
                Id = 1,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            projectLogicModes.Add(projectLogicModel);
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            _iprojectLogicMock.Setup(x => x.GetAll()).Returns(projectLogicModes);
            //Act
            var result=projectController.GetAll();
            //Assert
            _iprojectLogicMock.Verify(x => x.GetAll(), Times.Once);
            Assert.AreEqual(1,result.Count);
        }

        [Test]
        public void GetProjectById_should_get_project()
        {
            //Arrange
            var id = 1;
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            _iprojectLogicMock.Setup(x => x.Get(id)).Returns(projectLogicModel);
            //Act
            var result=projectController.GetProjectById(id);
            //Assert
            _iprojectLogicMock.Verify(x => x.Get(id), Times.Once);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetProjectUrlById_should_get_projecturl()
        {
            //Arrange
            var id = 1;
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            _iprojectLogicMock.Setup(x => x.Get(id)).Returns(projectLogicModel);
            //Act
            var result=projectController.GetProjectUrlById(id);
            //Assert
            _iprojectLogicMock.Verify(x=>x.Get(id),Times.Once);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetProjectByName_should_get_project()
        {
            //Arrange
            var name = "Plan Poker";
            var projectLogicModels = new List<ProjectLogicModel>();
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            projectLogicModels.Add(projectLogicModel);
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            _iprojectLogicMock.Setup(x => x.Get(name)).Returns(projectLogicModels);
            //Act
            var result = projectController.GetProjectByName(name);
            //Assert
            _iprojectLogicMock.Verify(x => x.Get(name), Times.Once);
            Assert.AreEqual(1,result.Count);
        }

        [Test]
        public void CheckExist_should_return_true_when_exist_project()
        {
            //Arrange
            var name = "Plan Poker";
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            _iprojectLogicMock.Setup(x => x.CheckExist(name)).Returns(true);
            //Act
            var result= projectController.CheckExist(name);
            //Assert
            _iprojectLogicMock.Verify(x => x.CheckExist(name), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckExist_should_return_false_when_no_exist_project()
        {
            //Arrange
            var name = "Plan Poker";
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            _iprojectLogicMock.Setup(x => x.CheckExist(name)).Returns(false);
            //Act
            var result = projectController.CheckExist(name);
            //Assert
            _iprojectLogicMock.Verify(x => x.CheckExist(name), Times.Once);
            Assert.IsFalse(result);
        }

        [Test]
        public void GetProjectGuidById_should_get_projectId()
        {
            //Arrange
            var guid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f");
            var projectLogicModels = new List<ProjectLogicModel>();
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            projectLogicModels.Add(projectLogicModel);
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            _iprojectLogicMock.Setup(x => x.GetAll()).Returns(projectLogicModels);
            //Act
            var result = projectController.GetProjectGuidById(guid);
            //Assert
            _iprojectLogicMock.Verify(x => x.GetAll(), Times.Once);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetProjectByGuid_should_get_project()
        {
            //Arrange
            var guid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f");
            var projectLogicModels = new List<ProjectLogicModel>();
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            projectLogicModels.Add(projectLogicModel);
            var projectController = new ProjectController(_iprojectLogicMock.Object);
            _iprojectLogicMock.Setup(x => x.GetAll()).Returns(projectLogicModels);
            //Act
            var result = projectController.GetProjectByGuid(guid);
            //Assert
            _iprojectLogicMock.Verify(x => x.GetAll(), Times.Once);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Plan Poker",result.Name);
            Assert.AreEqual(Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f"), result.ProjectGuid);
            Assert.IsNotNull(result);
        }
    }
}
