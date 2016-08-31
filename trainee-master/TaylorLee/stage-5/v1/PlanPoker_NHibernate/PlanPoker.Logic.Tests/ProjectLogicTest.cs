using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic.Tests
{
    [TestFixture]
    public class ProjectLogicTest
    {
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock;
        private Mock<IUnitOfWork> _unitOfWork;
        private ProjectLogic _projectLogic;


        [SetUp]
        public void Init()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _projectLogic = new ProjectLogic(_projectRepositoryMock.Object, _unitOfWorkFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _projectRepositoryMock = null;
            _unitOfWorkFactoryMock = null;
            _projectLogic = null;
        }

        [Test]
        public void Create_should_return_a_project_logic_model_when_creating_successfully()
        {
            //Arange
            string projectId = "76";
            string projectName = "TestProject3";
            string projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            var project = new Project
            {
                Id = Int32.Parse(projectId),
                Name = projectName,
                ProjectGuid = Guid.Parse(projectGuid)
            };
            var projectLogicModel = new ProjectLogicModel
            {
                Id = projectId,
                Name = projectName,
                ProjectGuid = projectGuid
            };

            _unitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork())
                .Returns(_unitOfWork.Object);
            _projectRepositoryMock.Setup(x => x.Create(It.IsAny<Project>())).Returns(project);

            //Act
            var actual = _projectLogic.Create(projectLogicModel);

            //Assert
            _projectRepositoryMock.Verify(x => x.Create(It.IsAny<Project>()), Times.Once);
            Assert.AreEqual(projectLogicModel.Id, actual.Id);
            Assert.AreEqual(projectLogicModel.Name, actual.Name);
            Assert.AreEqual(projectLogicModel.ProjectGuid, actual.ProjectGuid);
        }

        [Test]
        public void Edit_should_execute_once()
        {
            //Arange
            string projectId = "76";
            string projectName = "TestProject3";
            string projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            var projectLogicModel = new ProjectLogicModel
            {
                Id = projectId,
                Name = projectName,
                ProjectGuid = projectGuid
            };

            _unitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork())
                .Returns(_unitOfWork.Object);
            _projectRepositoryMock.Setup(x => x.Edit(It.IsAny<Project>()));

            //Act
            _projectLogic.Edit(projectLogicModel);

            //Assert
            _projectRepositoryMock.Verify(x => x.Edit(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void Delete_should_execute_once()
        {
            //Arange
            int projectId = 76;

            _unitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork())
                .Returns(_unitOfWork.Object);
            _projectRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));

            //Act
            _projectLogic.Delete(projectId);

            //Assert
            _projectRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_return_a_project_logic_model()
        {
            //Arange
            int projectId = 76;
            
            string projectName = "TestProject3";
            string projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            var project = new Project
            {
                Id = projectId,
                Name = projectName,
                ProjectGuid = Guid.Parse(projectGuid)
            };
            var projectLogicModel = new ProjectLogicModel
            {
                Id = projectId.ToString(),
                Name = projectName,
                ProjectGuid = projectGuid
            };

            _unitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork())
                .Returns(_unitOfWork.Object);
            _projectRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(project);

            //Act
            var actual = _projectLogic.Get(projectId);

            //Assert
            _projectRepositoryMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(projectLogicModel.Id, actual.Id);
            Assert.AreEqual(projectLogicModel.Name, actual.Name);
            Assert.AreEqual(projectLogicModel.ProjectGuid, actual.ProjectGuid);
        }

        [Test]
        public void GetAll_should_return_a_list_of_project_logic_model()
        {
            //Arange
            string projectId = "76";
            string projectName = "TestProject3";
            string projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            var project = new Project
            {
                Id = Int32.Parse(projectId),
                Name = projectName,
                ProjectGuid = Guid.Parse(projectGuid)
            };
            var projects = new List<Project>() { project };

            _unitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork())
                .Returns(_unitOfWork.Object);
            _projectRepositoryMock.Setup(x => x.Query()).Returns(projects.AsQueryable());

            //Act
            var actual = _projectLogic.GetAll();

            //Assert
            Assert.AreEqual(1, actual.Count());
            
        }

        [Test]
        public void Get_should_return_a_list_of_project_logic_model()
        {
            //Arange
            string projectId = "76";
            string projectName = "TestProject3";
            string projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            var project = new Project
            {
                Id = Int32.Parse(projectId),
                Name = projectName,
                ProjectGuid = Guid.Parse(projectGuid)
            };
            var projects = new List<Project>() { project };

            _unitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork())
                .Returns(_unitOfWork.Object);
            _projectRepositoryMock.Setup(x => x.Query()).Returns(projects.AsQueryable());

            //Act
            var actual = _projectLogic.Get(projectName);
            
            //Assert
            var projectLogicModels = actual as ProjectLogicModel[] ?? actual.ToArray();
            Assert.AreEqual(1, projectLogicModels.Count());
            Assert.AreEqual(projectName, projectLogicModels.ElementAt(0).Name);
        }

        [Test]
        public void CheckExist_should_return_true_when_project_name_exists()
        {
            //Arrange
            string projectId = "76";
            string projectName = "TestProject3";
            string projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            var project = new Project
            {
                Id = Int32.Parse(projectId),
                Name = projectName,
                ProjectGuid = Guid.Parse(projectGuid)
            };
            var projects = new List<Project>() { project };
            _projectRepositoryMock.Setup(x => x.Query()).Returns(projects.AsQueryable());

            //Act
            var actual = _projectLogic.CheckExist(projectName);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void CheckExist_should_return_false_when_project_name_exists()
        {
            //Arrange
            string projectName = "TestProject3";
            _projectRepositoryMock.Setup(x => x.Query()).Returns(new List<Project>().AsQueryable());

            //Act
            var actual = _projectLogic.CheckExist(projectName);

            //Assert
            Assert.AreEqual(false, actual);
        }
    }
}
