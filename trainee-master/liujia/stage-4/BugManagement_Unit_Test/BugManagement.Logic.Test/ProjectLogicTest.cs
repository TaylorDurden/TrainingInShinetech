using System;
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
    public class ProjectLogicTest
    {
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactotyMock;
        private ProjectLogic _projectLogic;
        private Mock<IUnitOfWork> _uniteOfWorkMock;

        [SetUp]
        public void Init()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _unitOfWorkFactotyMock = new Mock<IUnitOfWorkFactory>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();

            _projectLogic = new ProjectLogic(_projectRepositoryMock.Object, _unitOfWorkFactotyMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _projectRepositoryMock = null;
            _unitOfWorkFactotyMock = null;
            _uniteOfWorkMock = null;
            _projectLogic = null;
        }

        [Test]
        public void Create_should_call_create_method_of_project_repository_once()
        {
            //Arange
            var projectLogicModel = new ProjectLogicModel()
            {
                ProjectId=1,
                ContactEmail="contact email",
                Description="description",
                MainContact="main contact",
                ProjectName="project name",
                StartTime=DateTime.Now
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _projectRepositoryMock.Setup(n => n.Create(It.IsAny<Project>()));

            //Act
            _projectLogic.Create(projectLogicModel);

            //Assert
            _projectRepositoryMock.Verify(n => n.Create(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void Edit_should_call_edit_method_of_project_repository_once()
        {
            //Arange
            var projectLogicModel = new ProjectLogicModel()
            {
                ProjectId = 1,
                ContactEmail = "contact email",
                Description = "description",
                MainContact = "main contact",
                ProjectName = "project name",
                StartTime = DateTime.Now
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _projectRepositoryMock.Setup(n => n.Edit(It.IsAny<Project>()));

            //Act
            _projectLogic.Edit(projectLogicModel);

            //Assert
            _projectRepositoryMock.Verify(n => n.Edit(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void Delete_should_call_delete_method_of_project_repository_once()
        {
            //Arange
            var projectId = 1;
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _projectRepositoryMock.Setup(n => n.Delete(It.IsAny<int>()));

            //Act
            _projectLogic.Delete(projectId);

            //Assert
            _projectRepositoryMock.Verify(n => n.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_menthod_of_project_repository_once_return_projectLogicModel()
        {
            //Arange
            var projectId = 1;
            var project = new Project()
            {
                ProjectId = 1,
                ContactEmail = "contact email",
                Description = "description",
                MainContact = "main contact",
                ProjectName = "project name",
                StartTime = DateTime.Now
            };
            _projectRepositoryMock.Setup(n => n.Get(It.IsAny<int>())).Returns(project);

            //Act
            var result = _projectLogic.Get(projectId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("contact email", result.ContactEmail);
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_project_repository_once_return_projectLogicModel_list()
        {
            //Arange
            var project = new Project()
            {
                ProjectId = 1,
                ContactEmail = "contact email",
                Description = "description",
                MainContact = "main contact",
                ProjectName = "project name",
                StartTime = DateTime.Now
            };
            var projects = new List<Project>() { project };
            _projectRepositoryMock.Setup(n => n.Query()).Returns(projects.AsQueryable());

            //Act
            var result = _projectLogic.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void CheckExist_should_call_check_exist_method_of_project_repository_once_return_false_when_condition_is_not_exist()
        {
            //Arange
            var strProjectName = "";
            var strProjectId = "";
            var projects = new List<Project>();
            _projectRepositoryMock.Setup(n => n.Query()).Returns(projects.AsQueryable());

            //Acrt
            var result = _projectLogic.CheckExist(strProjectName, strProjectId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckExist_should_call_check_exist_method_of_project_repository_once_return_true_when_condition_is_exist()
        {
            //Arange
            var strProjectName = "project name";
            var strProjectId = "2";
            var project = new Project()
            {
                ProjectId = 1,
                ContactEmail = "contact email",
                Description = "description",
                MainContact = "main contact",
                ProjectName = "project name",
                StartTime = DateTime.Now
            };
            var projects = new List<Project>() { project };
            _projectRepositoryMock.Setup(n => n.Query()).Returns(projects.AsQueryable());

            //Acrt
            var result = _projectLogic.CheckExist(strProjectName, strProjectId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetPageCountByCondition_should_call_get_pageCount_by_condition_method_of_project_repository_once_return_pageCount()
        {
            //Arange
            var strSerchCondition = "";
            var project = new Project()
            {
                ProjectId = 1,
                ContactEmail = "contact email",
                Description = "description",
                MainContact = "main contact",
                ProjectName = "project name",
                StartTime = DateTime.Now
            };
            var projects = new List<Project>() { project };
            _projectRepositoryMock.Setup(n => n.Query()).Returns(projects.AsQueryable());

            //Act
            var result = _projectLogic.GetPageCountByCondition(strSerchCondition);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetProjectLogicModelsByCondition_should_call_get_projectLogicModels_by_condition_method_of_project_repository_once_return_projectLogicModel_list()
        {
            //Arange
            var strSerchCondition = "";
            var pageIndex = 5;
            var pageSize = 3;
            var count = 6;
            var project = new Project()
            {
                ProjectId = 1,
                ContactEmail = "contact email",
                Description = "description",
                MainContact = "main contact",
                ProjectName = "project name",
                StartTime = DateTime.Now
            };
            var projects = new List<Project>() { project, project, project, project, project, project, project };
            _projectRepositoryMock.Setup(n => n.Query()).Returns(projects.AsQueryable());

            //Act
            var result = _projectLogic.GetProjectLogicModelsByCondition(strSerchCondition, pageIndex, pageSize, count);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

    }
}
