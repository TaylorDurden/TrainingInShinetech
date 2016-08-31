using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PlanPoker.ILogic;
using PlanPoker.ILogic.LogicModel;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;

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
        private List<ProjectLogicModel> _projectLogicModels;
        private ProjectLogicModel _projectLogicModel;

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
        }
        
        [Test]
        public void Create_should_call_create_method_of_project_logic_once()
        {
            //Arange
            _projectName = "TestProject3";
            var projectViewModel = new ProjectViewModel
            {
                Name = _projectName
            };

            //Act
            _projectController.Create(projectViewModel);

            //Assert
            _projectLogicMock.Verify(x => x.Create(It.IsAny<ProjectLogicModel>()), Times.Once);
        }

        [Test]
        public void Delete_should_call_delete_method_of_project_logic_once()
        {
            //Arange
            _projectId = 76;

            //Act
            _projectController.Delete(_projectId);

            //Assert
            _projectLogicMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Edit_should_call_edit_method_of_project_logic_once()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            var projectViewModel = new ProjectViewModel
            {
                Name = _projectName,
                Id = _projectId
            };

            //Act
            _projectController.Edit(projectViewModel);

            //Assert
            _projectLogicMock.Verify(x => x.Edit(It.IsAny<ProjectLogicModel>()), Times.Once);
        }
        
        [Test]
        public void GetAll_should_call_GetAll_method_of_project_logic_once_return_all_projects()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";
            _projectLogicModels = new List<ProjectLogicModel>();
            _projectLogicModel = new ProjectLogicModel
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectLogicModels.Add(_projectLogicModel);
            _projectLogicMock.Setup(x => x.GetAll()).Returns(_projectLogicModels);

            //Act
            var result = _projectController.GetAll();

            //Assert
            _projectLogicMock.Verify(x => x.GetAll(), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void GetProjectById_should_call_Get_method_of_project_logic_once_return_project_search_by_Id()
        {
            //Arange
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";
            _projectId = 76;
            _projectName = "TestProject3";
            _projectLogicModel = new ProjectLogicModel
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectLogicMock.Setup(x => x.Get(_projectId)).Returns(_projectLogicModel);

            //Act
            var result = _projectController.GetProjectById(_projectId);

            //Assert
            _projectLogicMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(_projectId, result.Id);
            Assert.AreEqual(_projectName, result.Name);
            Assert.AreEqual(_projectGuid, result.ProjectGuid.ToString());
        }

        [Test]
        public void GetProjectUrlById_should_call_Get_method_of_project_logic_once_and_return_project_guid_searched_by_Id()
        {
            //Arange
            _projectName = "TestProject3";
            _projectId = 76;
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";
            _projectLogicModel = new ProjectLogicModel
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectLogicMock.Setup(x => x.Get(It.IsAny<int>())).Returns(_projectLogicModel);

            //Act
            var result = _projectController.GetProjectUrlById(_projectId);

            //Assert
            _projectLogicMock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetProjectByName_should_call_Get_method_of_project_logic_once_return_projects_when_name_is_contained()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";
            _projectLogicModels = new List<ProjectLogicModel>();
            _projectLogicModel = new ProjectLogicModel
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectLogicModels.Add(_projectLogicModel);
            _projectLogicMock.Setup(x => x.Get(_projectName)).Returns(_projectLogicModels);

            //Act
            var result = _projectController.GetProjectByName(_projectName);

            //Assert
            _projectLogicMock.Verify(x => x.Get(_projectName), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void GetProjectByGuid_should_call_GetAll_method_of_project_logic_once_return_project_searched_by_projectGuid()
        {
            //Arange
            _projectId = 76;
            _projectName = "TestProject3";
            _projectGuid = "af1b61f5-72d1-4de5-bdcb-e3a64f4d2f08";
            _projectLogicModels = new List<ProjectLogicModel>();
            _projectLogicModel = new ProjectLogicModel
            {
                Id = _projectId,
                Name = _projectName,
                ProjectGuid = Guid.Parse(_projectGuid)
            };
            _projectLogicModels.Add(_projectLogicModel);
            _projectLogicMock.Setup(x => x.GetAll()).Returns(_projectLogicModels);

            //Act
            var result = _projectController.GetProjectByGuid(Guid.Parse(_projectGuid));

            //Assert
            _projectLogicMock.Verify(x => x.GetAll(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(_projectId, result.Id);
            Assert.AreEqual(_projectName, result.Name);
            Assert.AreEqual(_projectGuid, result.ProjectGuid.ToString());
        }



        
        [Test]
        public void CheckExist_should_call_check_exist_method_of_project_logic_once_return_true_if_projectName_is_exist()
        {
            //Arange
            _projectName = "TestProject3";
            _projectLogicMock.Setup(x => x.CheckExist(_projectName)).Returns(true);

            //Act
            var result = _projectController.CheckExist(_projectName);

            //Assert
            _projectLogicMock.Verify(x => x.CheckExist(_projectName), Times.Once);
            Assert.IsTrue(result);
        }

    }
}
