using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PlanPoker.Common;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;


namespace PlanPoker.WebAPI.Tests
{
    [TestFixture]
    public class EstimateControllerTests
    {
        private Mock<ICacheManager> _cacheManagerMock;
        private Mock<IUserLogic> _userLogicMock;
        private EstimateController _estimateController;

        [SetUp]
        public void Init()
        {
            _cacheManagerMock = new Mock<ICacheManager>();
            _userLogicMock = new Mock<IUserLogic>();
            _estimateController = new EstimateController(_cacheManagerMock.Object, _userLogicMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _cacheManagerMock = null;
            _userLogicMock = null;
            _estimateController = null;
        }

        [Test]
        public void Insert_should_execute_cache_manager_add_once_when_estimate_key_not_exists()
        {
            //Arrange
            _cacheManagerMock.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(false);
            var estimates = new Estimates();
            var estimate = new Estimate
            {
                ProjectId = "123", SelectedPoker = "5", UserId = "10"
            };
            estimates.EstimateList.Add(estimate);

            _cacheManagerMock.Setup(m => m.Add(estimate.ProjectId, It.IsAny<Estimates>())).Verifiable();

            //Act
            _estimateController.Insert(estimate);

            //Assert
            Assert.AreEqual(estimates.EstimateList[0], estimate);
            _cacheManagerMock.Verify(c => c.Add(It.IsAny<string>(), It.IsAny<Estimates>()), Times.AtLeast(1));
        }

        [Test]
        public void Insert_should_add_an_estimate_when_estimate_key_exists_and_user_not_exists()
        {
            //Arrange
            _cacheManagerMock.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(true);
            var estimate = new Estimate
            {
                ProjectId = "123",
                SelectedPoker = "5",
                UserId = "10"
            };
            var estimates = new Estimates();
            estimates.EstimateList.Add(estimate);
            var notExistedUser=new Estimate() {UserId = "100"};
            _cacheManagerMock.Setup(c => c.Get<Estimates>(It.IsAny<string>())).Returns(estimates);

            //Act
            _estimateController.Insert(notExistedUser);

            //Assert
            Assert.AreEqual(_estimateController.Estimates.EstimateList[1].UserId, notExistedUser.UserId);
        }

        [Test]
        public void Insert_should_modify_selected_poker_when_estimate_key_exists_and_user_exists()
        {
            //Arrange
            _cacheManagerMock.Setup(x => x.KeyExists(It.IsAny<string>())).Returns(true);
            var estimate = new Estimate
            {
                ProjectId = "123",
                SelectedPoker = "5",
                UserId = "10"
            };
            var estimates = new Estimates();
            estimates.EstimateList.Add(estimate);
            var exsitedUser = new Estimate { UserId = "10", SelectedPoker = "10" };
            _cacheManagerMock.Setup(c => c.Get<Estimates>(It.IsAny<string>())).Returns(estimates);

            //Act
            _estimateController.Insert(exsitedUser);

            //Assert
            Assert.IsNotEmpty(_estimateController.Estimates.EstimateList[0].SelectedPoker, exsitedUser.SelectedPoker);
        }

        [Test]
        public void Delete_should_remove_an_estimates_in_cache_of_project_id_when_project_id_exists_in_estimates_cache()
        {

            //Arrange
            _cacheManagerMock.Setup(k => k.KeyExists(It.IsAny<string>())).Returns(true);
            var estimate = new Estimate
            {
                ProjectId = "123",
                SelectedPoker = "5",
                UserId = "10"
            };
            var estimates = new Estimates();
            estimates.EstimateList.Add(estimate);

            string projectId = "123";
            _cacheManagerMock.Setup(s => s.Add(projectId, estimates));

            //Act
            _estimateController.Delete(projectId);

            //Assert
            _cacheManagerMock.Verify(c => c.Remove(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(_estimateController.Estimates.EstimateList.Count, 0);
        }

        [Test]
        public void Get_should_return_empty_estimates_when_project_id_not_exists_in_cache()
        {
            //Arrange
            string projectId = "10";
            _cacheManagerMock.Setup(k => k.KeyExists(It.IsAny<string>())).Returns(false);
            //Act
            var actual = _estimateController.Get(projectId);

            //Assert
            Assert.AreEqual(0, actual.EstimateViewModel.Count);
        }

        [Test]
        public void Get_should_return_estimates_view_model_when_there_are_estimate_in_this_project()
        {
            //Arrange
            _cacheManagerMock.Setup(k => k.KeyExists(It.IsAny<string>())).Returns(true);
            var estimates = new Estimates {IsShow = true};
            estimates.EstimateList.Add(new Estimate
            {
                ProjectId = "123",
                SelectedPoker = "5",
                UserId = "10"
            });
            
            string projectId = "123";

            var estimatesViewModel = new EstimatesViewModel();
            estimatesViewModel.IsShow = true;
            var estimateViewModel = new EstimateViewModel()
            {
                ProjectId = "123",
                SelectedPoker = "5",
                UserImage = "sdsafdasdfas",
                UserName = "lee"
            };

            var userLogicModel = new UserLogicModel
            {
                Email = "12321@qq.com",
                Image = estimateViewModel.UserImage,
                Password = "123123",
                UserId = "10",
                UserName = estimateViewModel.UserName
            };
            var userLogicModels = new List<UserLogicModel> { userLogicModel };
            estimatesViewModel.EstimateViewModel.Add(estimateViewModel);
            _userLogicMock.Setup(u => u.GetByIds(It.IsAny<int[]>())).Returns(userLogicModels);

            _cacheManagerMock.Setup(c => c.Add(projectId, It.IsAny<Estimates>()));
            _cacheManagerMock.Setup(k => k.Get<Estimates>(It.IsAny<string>())).Returns(estimates);

            //Act
            var actual = _estimateController.Get(projectId);

            //Assert
            Assert.AreEqual(estimates.EstimateList.Count, 1);
            Assert.AreEqual(actual.EstimateViewModel.Count, 1);
            Assert.AreEqual(estimates.IsShow, true);
            Assert.AreEqual(actual.IsShow, true);
            
        }

        [Test]
        public void ShowCard_should_make_IsShow_true()
        {
            //Arrange
            string projectId = "100";
            _cacheManagerMock.Setup(k => k.KeyExists(It.IsAny<string>())).Returns(true);
            Estimates estimates = new Estimates();
            estimates.EstimateList.Add(new Estimate
            {
                ProjectId = "100",
                SelectedPoker = "5",
                UserId = "10"
            });
            _cacheManagerMock.Setup(c => c.Get<Estimates>(It.IsAny<string>())).Returns(estimates);

            //Act
            _estimateController.ShowCard(projectId);

            //Assert
            Assert.AreEqual(estimates.IsShow, true);
        }

        [Test]
        public void IsCleared_should_return_true_when_there_is_no_estimates_in_this_project()
        {
            //Arrange
            string projectId = "10";
            _cacheManagerMock.Setup(k => k.KeyExists(It.IsAny<string>())).Returns(false);
            //Act
            var actual = _estimateController.IsKeyCleared(projectId);
            //Assert
            Assert.AreEqual(actual, true);
        }
    }
}
