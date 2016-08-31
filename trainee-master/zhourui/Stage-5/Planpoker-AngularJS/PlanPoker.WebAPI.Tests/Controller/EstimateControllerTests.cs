using AutoMoq;
using NUnit.Framework;
using PlanPoker.Common;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using PlanPoker.ILogic;

namespace PlanPoker.WebAPI.Tests.Controller
{
    [TestFixture]
    public class EstimateControllerTests
    {
        private AutoMoqer _mocker;
        private EstimateController _estimateController;

        [SetUp]
        public void Initialize()
        {
            _mocker = new AutoMoqer();
            _estimateController = _mocker.Create<EstimateController>();
        }

        [TearDown]
        public void Dispose()
        {
            _mocker = null;
            _estimateController.Dispose();
        }

        #region "Insert Testing"
        [Test]
        public void Insert_when_estimate_is_null_should_never_execute_and_return_null()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(false);
            // Act
            var result = _estimateController.Insert(null);
            // Assert
            memoryCacheManager.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
            Assert.IsNull(result);
        }

        [Test]
        public void Insert_when_estimate_projectid_is_empty_or_null_should_never_execute_and_return_null()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            var estimateViewModel = new EstimateViewModel
            {
                ProjectId = "",
                UserId = 3,
                UserName = "zhang3",
                UserImage = "dfasd",
                SelectedPoker = "3"
            };
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(false);
            // Act
            var result = _estimateController.Insert(estimateViewModel);
            // Assert
            memoryCacheManager.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
            Assert.IsNull(result);
        }

        [Test]
        public void Insert_when_projectid_not_exist_should_execute_once()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            var estimateViewModel = new EstimateViewModel
            {
                ProjectId = "1",
                UserId = 2,
                UserName = "zhourui",
                UserImage = "",
                SelectedPoker = "2"
            };
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(false);
            // Act
            var result = _estimateController.Insert(estimateViewModel);
            // Assert
            memoryCacheManager.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>()), Times.Once());
        }

        [Test]
        public void Insert_when_projectid_exist_but_userid_not_exist_should_execute_once_and_count_should_be_2()
        {
            // Arrange
            var userLogic = _mocker.GetMock<IUserLogic>();
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            var estimatesViewModel = new EstimatesViewModel();
            var estimateViewModel1 = new EstimateViewModel
            {
                ProjectId = "1",
                UserId = 1,
                UserName = "zhourui1",
                UserImage = "1",
                SelectedPoker = "1"
            };
            var estimateViewModel3 = new EstimateViewModel
            {
                ProjectId = "3",
                UserId = 3,
                UserName = "zhourui3",
                UserImage = "3",
                SelectedPoker = "3"
            };
            estimatesViewModel.Estimates.Add(estimateViewModel1);
            estimatesViewModel.IsShow = true;
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(true);
            memoryCacheManager.Setup(x => x.Get<EstimatesViewModel>(It.IsAny<string>())).Returns(estimatesViewModel);
            // Act
            var result = _estimateController.Insert(estimateViewModel3);
            // Assert
            userLogic.Verify(x => x.GetUserImage(estimateViewModel3.UserName), Times.Once());
            Assert.AreEqual(typeof (EstimatesViewModel), result.GetType());
            Assert.AreEqual(estimatesViewModel.Estimates.Count,result.Estimates.Count);
        }

        [Test]
        public void Insert_when_estimate_not_null_and_userid_exist_selectedpoker_should_be_3()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            var estimatesViewModel = new EstimatesViewModel();
            var estimateViewModel1 = new EstimateViewModel
            {
                ProjectId = "1",
                UserId = 1,
                UserName = "zhourui",
                UserImage = "1",
                SelectedPoker = "1"
            };
            var estimateViewModel3 = new EstimateViewModel
            {
                ProjectId = "1",
                UserId = 1,
                UserName = "zhourui",
                UserImage = "3",
                SelectedPoker = "3"
            };
            estimatesViewModel.Estimates.Add(estimateViewModel1);
            estimatesViewModel.IsShow = true;
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(true);
            memoryCacheManager.Setup(x => x.Get<EstimatesViewModel>(It.IsAny<string>())).Returns(estimatesViewModel);
            // Act
            var result = _estimateController.Insert(estimateViewModel3);
            // Assert
            Assert.AreEqual(typeof (EstimatesViewModel), result.GetType());
            Assert.AreEqual(estimatesViewModel.Estimates[0].SelectedPoker,result.Estimates[0].SelectedPoker);
        }
        #endregion

        #region "Delete Testing"
        [Test]
        public void Delete_when_key_exist_should_excute_once()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(true);
            memoryCacheManager.Setup(x => x.Remove(It.IsAny<string>()));
            // Act
            _estimateController.Delete(It.IsAny<string>());
            // Assert
            memoryCacheManager.Verify(x => x.Remove(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Delete_when_key_not_exist_should_never_excute()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(false);
            memoryCacheManager.Setup(x => x.Remove(It.IsAny<string>()));
            // Act
            _estimateController.Delete(It.IsAny<string>());
            // Assert
            memoryCacheManager.Verify(x => x.Remove(It.IsAny<string>()), Times.Never());
        }
        #endregion

        #region "Get Testing"
        [Test]
        public void Get_when_projectId_exist_should_return_null()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(false);
            // Act
            var result = _estimateController.GetEstimatesById(It.IsAny<string>());
            // Assert
            result.Should().Be(null);
        }

        [Test]
        public void Get_when_project_not_exist_should_return_estimatesViewModel()
        {
            // Arrange
            var estimateViewModel = new EstimateViewModel
            {
                ProjectId = "1",
                UserId = 2,
                UserName = "zhourui",
                UserImage = "123",
                SelectedPoker = "5"
            };
            var estimatesViewModel = new EstimatesViewModel();
            estimatesViewModel.Estimates.Add(estimateViewModel);
            estimatesViewModel.IsShow = true;
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(true);
            memoryCacheManager.Setup(x => x.Get<EstimatesViewModel>("1")).Returns(estimatesViewModel);
            // Act
            var result = _estimateController.GetEstimatesById("1");
            // Assert
            result.Should().Be(estimatesViewModel);
        }
        #endregion

        #region "SetEstimatesIsShow Testing"
        [Test]
        public void SetEstimatesIsShow_when_key_exist_should_excute_once_and_IsShow_should_be_true()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(true);
            var estimateList = new List<EstimateViewModel> {
                new EstimateViewModel {ProjectId = "1", SelectedPoker = "1", UserId = 1 }
            };
            var estimates = new EstimatesViewModel { Estimates = estimateList, IsShow = false };
            memoryCacheManager.Setup(x => x.Get<EstimatesViewModel>(It.IsAny<string>())).Returns(estimates);
            // Act
            _estimateController.SetEstimatesIsShow(It.IsAny<string>());
            // Assert
            memoryCacheManager.Verify(x => x.Get<EstimatesViewModel>(It.IsAny<string>()), Times.Once());
            estimates.IsShow.Should().Be(true);
        }

        [Test]
        public void ShowCard_when_key_not_exist_should_never_excute_and_IsShow_should_be_false()
        {
            // Arrange
            var memoryCacheManager = _mocker.GetMock<IMemoryCacheManager>();
            memoryCacheManager.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(false);
            var estimateList = new List<EstimateViewModel> {
                new EstimateViewModel {ProjectId = "1", SelectedPoker = "1", UserId = 1 }
            };
            var estimates = new EstimatesViewModel { Estimates = estimateList, IsShow = false };
            memoryCacheManager.Setup(x => x.Get<EstimatesViewModel>(It.IsAny<string>())).Returns(estimates);
            // Act
            _estimateController.SetEstimatesIsShow(It.IsAny<string>());
            // Assert
            memoryCacheManager.Verify(x => x.Get<EstimatesViewModel>(It.IsAny<string>()), Times.Never());
            estimates.IsShow.Should().Be(false);
        }
        #endregion
    }
}
