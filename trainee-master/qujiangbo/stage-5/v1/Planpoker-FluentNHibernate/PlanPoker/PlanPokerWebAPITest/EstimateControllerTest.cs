using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.WebAPI.Models;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.Common;

namespace PlanPokerWebAPITest
{
    [TestFixture]
    public class EstimateControllerTest
    {
        private Mock<IUserLogic> _iuserLogicMock;
        private Mock<ICacheManager>  _icacheManager;

        [SetUp]
        public void SetUp()
        {
            _iuserLogicMock = new Mock<IUserLogic>();
            _icacheManager = new Mock<ICacheManager>();
        }

        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public void Insert_should_when_key_is_no_exist()
        {
            // Arrange
            var projectId = "10";
            var estimateModel = new Estimate
            {
                ProjectId = "11",
                UserId = 10,
                SelectedPoker = "2"
            };
            var estimates = new Estimates();
            estimates.EstimateList.Add(estimateModel);
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(false);
            // Act
            estimateController.Insert(estimateModel);
            // Assert
            _icacheManager.Verify(x => x.Add(It.IsAny<string>(), estimates), Times.Never);
        }

        [Test]
        public void Insert_should_when_key_is_exist_and_estimates_EstimateList_is_null()
        {
            //Arange
            var estimates = new List<Estimate>();
            var estimateModel = new Estimate
            {
                ProjectId = "1",
                UserId = 10,
                SelectedPoker = "2"
            };
            var estimateModelOther = new Estimate
            {
                ProjectId = "1",
                UserId = 11,
                SelectedPoker = "2"
            };
            var estimateModels = new Estimates
            {
                EstimateList = estimates,
                IsShow = false
            };
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(estimateModel.ProjectId)).Returns(true);
            _icacheManager.Setup(x => x.Get<Estimates>(estimateModel.ProjectId)).Returns(estimateModels);
            //Act
            estimateController.Insert(estimateModelOther);
            //Assert
            Assert.AreEqual(1, estimateModels.EstimateList.Count);
        }

        [Test]
        public void Insert_should_when_estimate_is_exist_and_estimates_EstimateList_is_not_null()
        {
            //Arange
            var estimates = new List<Estimate>();
            var estimateModel = new Estimate
            {
                ProjectId = "1",
                UserId = 10,
                SelectedPoker = "2"
            };
            estimates.Add(estimateModel);
            var estimateModelOther = new Estimate
            {
                ProjectId = "1",
                UserId = 10,
                SelectedPoker = "3"
            };
            var estimateModels = new Estimates
            {
                EstimateList = estimates,
                IsShow = false
            };
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(estimateModel.ProjectId)).Returns(true);
            _icacheManager.Setup(x => x.Get<Estimates>(estimateModel.ProjectId)).Returns(estimateModels);
            //Act
            estimateController.Insert(estimateModelOther);
            //Assert
            _icacheManager.Verify(x => x.Get<Estimates>(estimateModel.ProjectId), Times.Once);
            Assert.AreEqual(estimateModels.EstimateList[0].SelectedPoker, estimateModelOther.SelectedPoker);
        }

        [Test]
        public void Delete_should_when_cacheManager_exist() {
            //Arange
            var projectId = "1";
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(true);
            //Act
            estimateController.Delete(projectId);
            //Assert
            _icacheManager.Verify(x => x.Remove(projectId), Times.Once);
        }

        [Test]
        public void Delete_should_when_cacheManager_is_no_exist()
        {
            //Arange
            var projectId = "1";
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(false);
            //Act
            estimateController.Delete(projectId);
            //Assert
            _icacheManager.Verify(x => x.Remove(projectId), Times.Never);
        }

        [Test]
        public void Get_should_return_null_when_key_is_no_exist() {
            //Arange
            var projectId = "1";
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(false);
            //Act
            var result=estimateController.Get(projectId);
            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Get_should_return_value_when_key_is_exist_estimates_have_no_value()
        {
            //Arange
            var projectId = "1";
            var estimates = new List<Estimate>();
            var estimateModels = new Estimates
            {
                EstimateList = estimates,
                IsShow = false
            };
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(true);
            _icacheManager.Setup(x => x.Get<Estimates>(projectId)).Returns(estimateModels);
            //Act
            var result = estimateController.Get(projectId);
            //Assert
            _icacheManager.Verify(x => x.Get<Estimates>(projectId), Times.Once);
            Assert.AreEqual(0, result.EstimateViewModel.Count);
        }

        [Test]
        public void Get_should_return_value_when_key_is_exist_estimates_have_value()
        {
            //Arange
            var projectId = "1";
            var userId = 10;
            var estimates = new List<Estimate>();
            var estimateModel = new Estimate
            {
                ProjectId = "1",
                UserId = 10,
                SelectedPoker = "2"
            };
            estimates.Add(estimateModel);
            var estimateModels = new Estimates
            {
                EstimateList = estimates,
                IsShow = false
            };
            var userLogicModelMock = new UserLogicModel
            {
                UserId=10,
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/=="
            };
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(true);
            _icacheManager.Setup(x => x.Get<Estimates>(projectId)).Returns(estimateModels);
            _iuserLogicMock.Setup(x => x.Get(userId)).Returns(userLogicModelMock);
            //Act
            var result =estimateController.Get(projectId);
            //Assert
            _icacheManager.Verify(x => x.Get<Estimates>(projectId), Times.Once);
            _iuserLogicMock.Verify(x => x.Get(userId), Times.Once);
            Assert.AreEqual(1,result.EstimateViewModel.Count);            
        }

        [Test]
        public void ShowCard_should_return_true_and_object_when_key_is_exist()
        {
            //Arange
            var projectId = "1";
            var estimates = new List<Estimate>();
            var estimateModel = new Estimate
            {
                ProjectId = "1",
                UserId = 10,
                SelectedPoker = "2"
            };
            estimates.Add(estimateModel);
            var estimateModels = new Estimates
            {
                EstimateList = estimates,
                IsShow = false
            };
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(true);
            _icacheManager.Setup(x => x.Get<Estimates>(projectId)).Returns(estimateModels);
            //Act
            estimateController.ShowCard(projectId);
            //Assert
            _icacheManager.Verify(x => x.Get<Estimates>(projectId), Times.Once);
            Assert.AreEqual(1, _icacheManager.Object.Get<Estimates>(projectId).EstimateList.Count);
        }

        [Test]
        public void ShowCard_should_return_false_and_null_when_key_is_no_exist()
        {
            //Arange
            var projectId = "10";
            var estimates = new List<Estimate>();
            var estimateModel = new Estimate
            {
                ProjectId = "1",
                UserId = 10,
                SelectedPoker = "2"
            };
            estimates.Add(estimateModel);
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(false);
            //Act
            estimateController.ShowCard(projectId);
            //Assert
            Assert.IsFalse(_icacheManager.Object.KeyExist(projectId));
        }

        [Test]
        public void IsCleared_should_return_false_when_key_is_exist()
        {
            //Arange
            var projectId = "100";
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(true);
            //Act
            var result = estimateController.IsCleared(projectId);
            //Assert
            _icacheManager.Verify(x => x.KeyExist(projectId), Times.Once);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsCleared_should_return_true_when_key_is_no_exist()
        {
            //Arange
            var projectId = "100";
            var estimateController = new EstimateController(_icacheManager.Object, _iuserLogicMock.Object);
            _icacheManager.Setup(x => x.KeyExist(projectId)).Returns(false);
            //Act
            var result = estimateController.IsCleared(projectId);
            //Assert
            _icacheManager.Verify(x => x.KeyExist(projectId), Times.Once);
            Assert.IsTrue(result);
        }
    }
}
