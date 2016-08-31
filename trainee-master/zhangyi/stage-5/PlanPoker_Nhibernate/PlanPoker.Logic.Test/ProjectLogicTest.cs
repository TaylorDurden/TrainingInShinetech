using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.LogicModel;
using PlanPoker.IRepository;
using RepositoryNhibernate.UnitOfWork;

namespace PlanPoker.Logic.Test
{
    [TestFixture]
    public class ProjectLogicTest
    {
        private int _projectId;
        private string _projectName;
        private string _projectGuid;
        private Mock<IUnitOfWorkFactory> _userUnitOfWorkFactoryMock;
        private Mock<IUnitOfWork> _uniteOfWorkMock;
        private Mock<IProjectRepository> _projectRepositoryMock;
        private ProjectLogic _projectLogic;
        private Project _projectEntity;
        private List<Project> _projectEntities;

        [SetUp]
        public void Init()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _userUnitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
            _projectLogic = new ProjectLogic(_projectRepositoryMock.Object, _userUnitOfWorkFactoryMock.Object);
            _uniteOfWorkMock = new Mock<IUnitOfWork>();
        }

        [TearDown]
        public void Dispose()
        {
            _projectRepositoryMock = null;
            _userUnitOfWorkFactoryMock = null;
            _projectLogic = null;
            _uniteOfWorkMock = null;
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_project_repository_once_return_all_prjects()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            _projectEntity = new Project
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectEntities = new List<Project> { _projectEntity };
            _projectRepositoryMock.Setup(x => x.Query()).Returns(_projectEntities.AsQueryable());

            //Act
            var result = _projectLogic.GetAll();

            //Assert
            _projectRepositoryMock.Verify(x => x.Query(), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void Delete_should_call_delete_method_of_project_repository_once()
        {
            //Arange
            _projectId = 76;
            _userUnitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);

            //Act
            _projectLogic.Delete(_projectId);

            //Assert
            _projectRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_method_of_project_repository_once_return_project()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            _projectEntity = new Project
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(_projectEntity);

            //Act
            var result = _projectLogic.Get(_projectId);

            //Assert
            _projectRepositoryMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(_projectId, result.Id);
            Assert.AreEqual(_projectName, result.Name);
            Assert.AreEqual(_projectGuid, result.ProjectGuid.ToString());
        }

        [Test]
        public void Create_should_call_create_method_of_project_repository_once()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            _projectEntity = new Project
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            var projectLogicModel = new ProjectLogicModel
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _userUnitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork())
                .Returns(_uniteOfWorkMock.Object);
            _projectRepositoryMock.Setup(x => x.Create(_projectEntity));

            //Act
            _projectLogic.Create(projectLogicModel);

            //Assert
            _projectRepositoryMock.Verify(x => x.Create(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void Edit_should_call_edit_method_of_project_repository_once()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";

            _projectEntity = new Project
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            var projectLogicModel = new ProjectLogicModel
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _userUnitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _projectRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(_projectEntity);

            //Act
            _projectLogic.Edit(projectLogicModel);

            //Assert
            _projectRepositoryMock.Verify(x => x.Edit(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_method_of_project_repository_once_return_projects()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";
            _projectEntity = new Project
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectEntities = new List<Project> { _projectEntity };
            _projectRepositoryMock.Setup(x => x.Query()).Returns(_projectEntities.AsQueryable());

            //Act
            var result = _projectLogic.Get(_projectName);

            //Assert
            _projectRepositoryMock.Verify(x => x.Query(), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void CheckIfExist_should_call_query_method_of_project_repository_and_return_true_when_user_is_exist()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";
            _projectEntity = new Project
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectEntities = new List<Project> { _projectEntity };
            _projectRepositoryMock.Setup(x => x.Query()).Returns(_projectEntities.AsQueryable());

            //Act
            var result = _projectLogic.CheckExist(_projectName);

            //Assert
            _projectRepositoryMock.Verify(x => x.Query(), Times.Once);
            Assert.IsTrue(result);
        }
    }
}