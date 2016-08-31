using System;
using Moq;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.WebAPI.Controllers;

namespace PlanPoker.WebApi.Test
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private int _projectId;
        private string _projectName;
        private string _projectGuid;
        private Mock<IProjectLogic> _projectLogicMock;
        private ProjectController _projectController;

        [SetUp]
        public void Init()
        {
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";
            _projectLogicMock = new Mock<IProjectLogic>();
            _projectController = new ProjectController(_projectLogicMock.Object);
        }

        [Test]
        public void Create_should_call_create_method_of_project_logic_once()
        {
            //Range
            var project = new Project
            {
                Name = _projectName
            };
            //Act
            _projectController.Create(project);
            //Assert
            _projectLogicMock.Verify(x => x.Create(project), Times.Once);
        }

        [Test]
        public void Delete_should_call_delete_method_of_project_logic_once()
        {
            //Range
            var project = new Project
            {
                Name = _projectName
            };
            //Act
            _projectController.Create(project);
            //Assert
            _projectLogicMock.Verify(x => x.Create(project), Times.Once);
        }

        [Test]
        public void Edit_should_call_edit_method_of_project_logic_once()
        {
            //Range
            var project = new Project
            {
                Name = _projectName,
                Id = _projectId
            };
            //Act
            _projectController.Edit(project);
            //Assert
            _projectLogicMock.Verify(x => x.Edit(project), Times.Once);
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_project_logic_once()
        {
            //Range

            //Act
            _projectController.GetAll();
            //Assert
            _projectLogicMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetProjectById_should_call_get_project_by_id_method_of_project_logic_once()
        {
            //Range

            //Act
            _projectController.GetProjectById(_projectId);
            //Assert
            _projectLogicMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetProjectUrlById_should_call_get_project_url_by_id_method_of_project_logic_once()
        {
            //Range

            //Act
            _projectController.GetProjectUrlById(_projectId);
            //Assert
            _projectLogicMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetProjectByName_should_call_get_project_by_name_method_of_project_logic_once()
        {
            //Range

            //Act
            _projectController.GetProjectByName(_projectName);
            //Assert
            _projectLogicMock.Verify(x => x.Get(_projectName), Times.Once);
        }

        [Test]
        public void CheckExist_should_call_check_exist_method_of_project_logic_once()
        {
            //Range

            //Act
            _projectController.CheckExist(_projectName);
            //Assert
            _projectLogicMock.Verify(x => x.CheckExist(_projectName), Times.Once);
        }

        [Test]
        public void GetProjectByGuid_should_get_project_by_Guid_method_of_project_logic_once()
        {
            //Range

            //Act
            _projectController.GetProjectByGuid(Guid.Parse(_projectGuid));
            //Assert
            _projectLogicMock.Verify(x => x.GetAll(), Times.Once);
        }
        

    }
    
}
