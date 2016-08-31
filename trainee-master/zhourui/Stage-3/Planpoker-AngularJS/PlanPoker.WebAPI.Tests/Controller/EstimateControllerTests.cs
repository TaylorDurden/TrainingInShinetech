using AutoMoq;
using NUnit.Framework;
using PlanPoker.Common;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;
using System.Collections.Generic;
using System.Timers;
using FluentAssertions;
using Moq;

namespace PlanPoker.WebAPI.Tests.Controller
{
    [TestFixture]
    public class EstimateControllerTests
    {
        private AutoMoqer _mocker;
        private EstimateController _estimateController;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _estimateController = _mocker.Create<EstimateController>();
            //_estimateController = _mocker.Resolve<EstimateController>();
        }

        [Test]
        public void Insert_when_estimate_not_exist_should_be_2()
        {
            // Arrange
            //_estimateController = _mocker.Create<EstimateController>();
            var cacheManager = _mocker.GetMock<ICacheManager>();
            cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(false);
            var estimates = new List<Estimate> { new Estimate { ProjectId = "1", SelectedPoker = "1", UserId = 1 } };
            var estimate1 = new Estimate { ProjectId = "1", SelectedPoker = "2", UserId = 2 };
            
            //cacheManager.Setup(x => x.Get<List<Estimate>>("1")).Returns(estimates);
            //cacheManager.Setup(x => x.Add("1", estimate1));

            // Act
            _estimateController.Insert(estimate1);

            // Assert
            cacheManager.Verify(x=>x.Add(It.IsAny<string>(),It.IsAny<object>()),Times.Once());
            //_mocker.GetMock<ICacheManager>().Setup(x => x.Add("1", estimate1), Times.on);
            //estimates.Count.Should().Be(1);

            //var mocker = new AutoMoqer();

            //mocker.GetMock<IDataDependency>()
            //   .Setup(x => x.GetData())
            //   .Returns("TEST DATA");

            //var classToTest = mocker.Resolve<ClassToTest>();

            //classToTest.DoSomething();

            //mocker.GetMock<IDependencyToCheck>()
            //   .Setup(x => x.CallMe("TEST"), Times.Once());
        }

        [Test]
        public void Delete_when_key_exist_should_excute_once()
        {
            // Arrange
            var cacheManager = _mocker.GetMock<ICacheManager>();
            cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            cacheManager.Setup(x => x.Remove(It.IsAny<string>()));
            // Act
            _estimateController.Delete(It.IsAny<string>());
            // Assert
            cacheManager.Verify(x => x.Remove(It.IsAny<string>()), Times.Once());
        }
        [Test]
        public void Delete_when_key_not_exist_should_never_excute()
        {
            // Arrange
            var cacheManager = _mocker.GetMock<ICacheManager>();
            cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(false);
            cacheManager.Setup(x => x.Remove(It.IsAny<string>()));
            // Act
            _estimateController.Delete(It.IsAny<string>());
            // Assert
            cacheManager.Verify(x => x.Remove(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void ShowCard_when_key_exist_should_excute_once_and_IsShow_should_be_true()
        {
            // Arrange
            var cacheManager = _mocker.GetMock<ICacheManager>();
            cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            var estimateList = new List<Estimate> {
                new Estimate {ProjectId = "1", SelectedPoker = "1", UserId = 1 }
            };
            var estimates = new Estimates { EstimateList = estimateList, IsShow = false };
            cacheManager.Setup(x => x.Get<Estimates>(It.IsAny<string>())).Returns(estimates);
            // Act
            _estimateController.ShowCard(It.IsAny<string>());
            // Assert
            cacheManager.Verify(x => x.Get<Estimates>(It.IsAny<string>()), Times.Once());
            estimates.IsShow.Should().Be(true);
        }
        [Test]
        public void ShowCard_when_key_not_exist_should_never_excute_and_IsShow_should_be_false()
        {
            // Arrange
            var cacheManager = _mocker.GetMock<ICacheManager>();
            cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(false);
            var estimateList = new List<Estimate> {
                new Estimate {ProjectId = "1", SelectedPoker = "1", UserId = 1 }
            };
            var estimates = new Estimates { EstimateList = estimateList, IsShow = false };
            cacheManager.Setup(x => x.Get<Estimates>(It.IsAny<string>())).Returns(estimates);
            // Act
            _estimateController.ShowCard(It.IsAny<string>());
            // Assert
            cacheManager.Verify(x => x.Get<Estimates>(It.IsAny<string>()), Times.Never());
            estimates.IsShow.Should().Be(false);
        }
    }
}
