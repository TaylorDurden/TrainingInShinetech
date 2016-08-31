using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using NUnit.Framework;
using PlanPoker.WebAPI.Controllers;
using Moq;

namespace PlanPokerWebAPITest
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private Mock<Project> _projectMock;
        private Mock<IProjectLogic> _projectLogicMock;
        private ProjectController _projectController;
        private int _id;
        private Guid _guid;
        private string _name;

        [SetUp]
        public void setUp()
        {
            _id = 1;
            _guid = new Guid();
            _name = "Bill";
            _projectMock = new Mock<Project>();
            _projectLogicMock = new Mock<IProjectLogic>();
            _projectController = new ProjectController(_projectLogicMock.Object);


        }

        [TearDown]
        public void tearDown()
        {
            

        }

        [Test]
        public void Create_should_create_project() {
            //Arrange

            //Act
            _projectController.Create(_projectMock.Object);
            //Assert
            _projectLogicMock.Verify(x => x.Create(_projectMock.Object), Times.Once);
        }

        [Test]
        public void Delete_should_delete_project()
        {
            //Arrange

            //Act
            _projectController.Delete(_id);
            //Assert
            _projectLogicMock.Verify(x => x.Delete(_id), Times.Once);
        }

        [Test]
        public void Edit_should_edit_user()
        {
            //Arrange

            //Act
            _projectController.Edit(_projectMock.Object);
            //Assert
            _projectLogicMock.Verify(x => x.Edit(_projectMock.Object), Times.Once);
        }



        [Test]
        public void GetAll_should_get_projects()
        {
            //Arrange

            //Act
            _projectController.GetAll();
            //Assert
            _projectLogicMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetProjectById_should_get_project() {
            //Arrange

            //Act
            _projectController.GetProjectById(_id);
            //Assert
            _projectLogicMock.Verify(x => x.Get(_id), Times.Once);
        }

        [Test]
        public void GetProjectUrlById_should_get_projecturl() {
            //Arrange

            //Act
            //_projectController.GetProjectUrlById(_id);
            //Assert
            //_projectLogicMock.Verify(x=>x.Get(_id)),Times.Once);
        }

        [Test]
        public void GetProjectByName_should_get_project()
        {
            //Arrange

            //Act
            _projectController.GetProjectByName(_name);
            //Assert
            _projectLogicMock.Verify(x => x.Get(_name), Times.Once);
        }


        [Test]
        public void CheckExist_should_exist_project()
        {
            //Arrange

            //Act
            _projectController.CheckExist(_name);
            //Assert
            _projectLogicMock.Verify(x => x.CheckExist(_name), Times.Once);
        }

        [Test]
        public void GetProjectGuidById_should_get_projectId() {
            //Arrange

            //Act
            //_projectController.GetProjectGuidById(_guid);
            //Assert
        }

        [Test]
        public void GetProjectByGuid_should_get_project() {
            //Arrange

            //Act

            //Assert
        }
    }
}
