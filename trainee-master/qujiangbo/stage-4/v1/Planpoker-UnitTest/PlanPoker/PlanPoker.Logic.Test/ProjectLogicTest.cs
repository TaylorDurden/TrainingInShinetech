using System;
using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;
using NUnit.Framework;
using Moq;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic.Test
{
    [TestFixture]
    public class ProjectLogicTest
    {
        private Mock<IProjectRepository> _iprojectRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactory; 
        private Mock<IUnitOfWork> _uniteOfWorkMock;

        [SetUp]
        public void SetUp()
        {
            _iprojectRepositoryMock = new Mock<IProjectRepository>();
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Create_should_create_project()
        {
            //Arrange
            var projectLogicModel = new ProjectLogicModel
            {
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            //Act
            projectLogic.Create(projectLogicModel);
            //Assert
            _iprojectRepositoryMock.Verify(x=>x.Create(It.IsAny<Project>()),Times.Once);
        }

        [Test]
        public void Delete_should_delete_project()
        {
            //Arrange
            var id = 1;
            var userLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            //Act
            userLogic.Delete(id);
            //Assert
            _iprojectRepositoryMock.Verify(x => x.Delete(id), Times.Once);
        }

        [Test]
        public void Edit_should_edit_project()
        {
            //Arrange
            var projectLogicModel = new ProjectLogicModel
            {
                Id=1047,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            //Act
            projectLogic.Edit(projectLogicModel);
            //Assert
            _iprojectRepositoryMock.Verify(x => x.Edit(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public void GetAll_should_get_projects()
        {
            //Arrange
            var projectLogicModels = new List<Project>();
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1047,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectLogicModelOther = new ProjectLogicModel
            {
                Id = 1048,
                Name = "Plan Poker Test",
                ProjectGuid = Guid.Parse("02294154-0785-4602-82db-715b32136dca")
            };
            var projectLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            var project = projectLogicModel.CreateConvert();
            projectLogicModels.Add(project);
            var projectOther = projectLogicModelOther.CreateConvert();
            projectLogicModels.Add(projectOther);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iprojectRepositoryMock.Setup(x => x.Query()).Returns(projectLogicModels.AsQueryable());
            //Act
            var result = projectLogic.GetAll();
            //Assert            
            Assert.AreEqual(2,result.Count);
        }

        [Test]
        public void Get_should_get_project_by_id()
        {
            //Arrange
            var id = 1047;
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1047,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            var project = projectLogicModel.CreateConvert();            
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iprojectRepositoryMock.Setup(x => x.Get(id)).Returns(project);
            //Act
            var result = projectLogic.Get(id);
            //Assert
            Assert.AreEqual(1047, result.Id);
            Assert.AreEqual("Plan Poker", result.Name);
            Assert.AreEqual("613d7a88-2290-463a-b91f-05efa5bd2b7f", result.ProjectGuid.ToString());
        }

        [Test]
        public void Get_should_get_project_by_name_when_name_is_exist()
        {
            //Arrange
            var _name = "Plan Poker";
            var projects = new List<Project>();
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1047,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            var project = projectLogicModel.CreateConvert();
            projects.Add(project);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iprojectRepositoryMock.Setup(x => x.Query()).Returns(projects.AsQueryable());
            //Act
            var result = projectLogic.Get(_name);
            //Assert
            Assert.AreEqual(1,result.Count);
        }

        [Test]
        public void Get_should_get_project_by_name_when_name_is_no_exist()
        {
            //Arrange
            var name = "Plan Poker Test";
            var projects = new List<Project>();
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1047,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            var project = projectLogicModel.CreateConvert();
            projects.Add(project);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iprojectRepositoryMock.Setup(x => x.Query()).Returns(projects.AsQueryable());
            //Act
            var result = projectLogic.Get(name);
            //Assert
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void CheckExist_should_return_true_when_name_is_exist() {
            //Arrange
            var name = "Plan Poker";
            var projects = new List<Project>();
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1047,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            var project = projectLogicModel.CreateConvert();
            projects.Add(project);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iprojectRepositoryMock.Setup(x=>x.Query()).Returns(projects.AsQueryable());
            //Act
            var result = projectLogic.CheckExist(name);
            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckExist_should_return_false_when_name_is_no_exist()
        {
            //Arrange
            var name = "Plan Poker Test";
            var projects = new List<Project>();
            var projectLogicModel = new ProjectLogicModel
            {
                Id = 1047,
                Name = "Plan Poker",
                ProjectGuid = Guid.Parse("613d7a88-2290-463a-b91f-05efa5bd2b7f")
            };
            var projectLogic = new ProjectLogic(_iprojectRepositoryMock.Object, _unitOfWorkFactory.Object);
            var project = projectLogicModel.CreateConvert();
            projects.Add(project);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iprojectRepositoryMock.Setup(x => x.Query()).Returns(projects.AsQueryable());
            //Act
            var result = projectLogic.CheckExist(name);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
