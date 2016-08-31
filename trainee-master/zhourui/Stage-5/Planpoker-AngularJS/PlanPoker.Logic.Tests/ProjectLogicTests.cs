﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AutoMoq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic.Tests
{
    [TestFixture]
    public class ProjectLogicTests
    {
        private AutoMoqer _mocker;
        private ProjectLogic _projectLogic;

        [SetUp]
        public void Initialize()
        {
            _mocker = new AutoMoqer();
            _projectLogic = _mocker.Create<ProjectLogic>();
        }

        [TearDown]
        public void Dispose()
        {
            _mocker = null;
            _projectLogic = null;
        }

        #region "Create Testing"
        [Test]
        public void Create_when_projectLogicModel_is_null_should_return_null()
        {
            // Arrange

            // Act
            var result = _projectLogic.Create(null);
            // Assert
            result.Should().Be(null);
        }

        [Test]
        public void Create_when_projectname_exist_should_return_specific_message()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "project1"
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x=>x.Get(It.IsAny<string>())).Returns(project);
            // Act
            var result = _projectLogic.Create(project.ConvertToProjectLogicModel(""));
            // Assert
            result.Message.Should().Be("the project name exist, please input a new project name.");
        }

        [Test]
        public void Create_when_projetname_not_exist_should_execute_once()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "project1"
            };
            var unitOfWorkFactory = _mocker.GetMock<IUnitOfWorkFactory>();
            var unitOfWork = _mocker.GetMock<IUnitOfWork>();
            unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(unitOfWork.Object);
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            // Act
            var result = _projectLogic.Create(project.ConvertToProjectLogicModel(""));
            // Assert
            projectRepository.Verify(x=>x.Create(It.IsAny<Project>()),Times.Once());
            unitOfWork.Verify(x=>x.Commit(),Times.Once());
            Assert.AreEqual(typeof(ProjectLogicModel),result.GetType());
        }
        #endregion

        #region "Edit Testing"
        [Test]
        public void Edit_when_projectLogicModel_is_null_should_return_null()
        {
            // Arrange

            // Act
            var result = _projectLogic.Edit(null);
            // Assert
            result.Should().Be(null);
        }

        [Test]
        public void Edit_when_projectname_exist_should_return_specific_message()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Project1"
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(project);
            // Act
            var result = _projectLogic.Edit(project.ConvertToProjectLogicModel(""));
            // Assert
            result.Message.Should().Be("the projectname exist, please select a new projectname to update.");
        }

        [Test]
        public void Edit_when_projectname_not_exist_should_execute_once()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Project1"
            };
            var unitOfWorkFactory = _mocker.GetMock<IUnitOfWorkFactory>();
            var unitOfWork = _mocker.GetMock<IUnitOfWork>();
            unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(unitOfWork.Object);
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            // Act
            var result = _projectLogic.Edit(project.ConvertToProjectLogicModel(""));
            // Assert
            projectRepository.Verify(x => x.Edit(It.IsAny<Project>()), Times.Once());
            unitOfWork.Verify(x => x.Commit(), Times.Once());
            Assert.AreEqual(typeof(ProjectLogicModel), result.GetType());
        }
        #endregion

        #region "Delete Testing"
        [Test]
        public void Delete_()
        {
            // Arrange
            var unitOfWorkFactory = _mocker.GetMock<IUnitOfWorkFactory>();
            var unitOfWork = _mocker.GetMock<IUnitOfWork>();
            unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(unitOfWork.Object);
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            // Act
            _projectLogic.Delete(It.IsAny<int>());
            // Assert
            projectRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
            unitOfWork.Verify(x => x.Commit(), Times.Once());
        }
        #endregion

        #region "Gets Testing"
        [Test]
        public void GetAll_should_execute_once_and_return_1()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Name = "Project1"
                }
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x => x.Query()).Returns(projects.AsQueryable());
            // Act
            var result = _projectLogic.GetAll();
            // Assert
            projectRepository.Verify(x => x.Query(), Times.Once());
            result.Count().Should().Be(1);
        }

        [Test]
        public void Get_when_id_is_valid_should_return_right_type()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Project1"
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(project);
            // Act
            var result = _projectLogic.Get(1);
            // Assert
            projectRepository.Verify(x => x.Get(1), Times.Once());
            Assert.AreEqual(typeof(ProjectLogicModel), result.GetType());
            result.Name.Should().Be("Project1");
        }

        [Test]
        public void Get_when_id_not_valid_should_return_specific_message()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Project1"
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(project);
            // Act
            var result = _projectLogic.Get(It.IsAny<int>());
            // Assert
            projectRepository.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
            Assert.AreEqual(typeof(ProjectLogicModel), result.GetType());
            result.Message.Should().Be("project id should greater than zero.");
        }

        [Test]
        public void Get_when_projectname_not_empty_should_return_right_type()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Project1"
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(project);
            // Act
            var result = _projectLogic.Get("Project1");
            // Assert
            projectRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(typeof(ProjectLogicModel), result.GetType());
            result.Id.Should().Be(1);
            result.Message.Should().Be("");
        }

        [Test]
        public void Get_when_projectname_is_empty_should_return_specific_message()
        {
            // Arrange
            var project = new Project
            {
                Id = 1,
                Name = "Project1"
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(project);
            // Act
            var result = _projectLogic.Get(It.IsAny<string>());
            // Assert
            projectRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(typeof(ProjectLogicModel), result.GetType());
            result.Message.Should().Be("project name cannot be empty.");
        }
        #endregion

        #region "Check Testing"
        [Test]
        public void CheckIfProjectnameExist_when_projectname_not_exist_should_return_false()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Name = "Project1"
                }
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x => x.Query()).Returns(projects.AsQueryable());
            // Act
            var result = _projectLogic.CheckIfProjectnameExists(It.IsAny<string>());
            // Assert
            result.Should().Be(false);
        }

        [Test]
        public void CheckIfProjectnameExist_when_projectname_exist_should_return_true()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Name = "Project1"
                }
            };
            var projectRepository = _mocker.GetMock<IProjectRepository>();
            projectRepository.Setup(x => x.Query()).Returns(projects.AsQueryable());
            // Act
            var result = _projectLogic.CheckIfProjectnameExists("Project1");
            // Assert
            result.Should().Be(true);
        }
        #endregion
    }
}
