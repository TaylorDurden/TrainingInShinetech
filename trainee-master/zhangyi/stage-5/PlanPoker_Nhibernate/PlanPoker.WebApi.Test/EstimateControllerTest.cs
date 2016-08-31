using Moq;
using NUnit.Framework;
using PlanPoker.Common;
using PlanPoker.ILogic;
using PlanPoker.ILogic.LogicModel;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebApi.Test
{
    [TestFixture]
    public class EstimateControllerTest
    {
        private string _projectId;
        private string _selectedPoker;
        private int _userId;
        private Mock<ICacheManager> _cacheManager;
        private Mock<IUserLogic> _userLogicMock;
        private EstimateController _estimateController;
        private Estimates _estimates;
        private Estimate _estimate;

        [SetUp]
        public void Init()
        {

            _cacheManager = new Mock<ICacheManager>();
            _userLogicMock = new Mock<IUserLogic>();
            _estimateController = new EstimateController(_cacheManager.Object, _userLogicMock.Object);

        }

        [TearDown]
        public void Dispose()
        {
            _cacheManager = null;
            _userLogicMock = null;
            _estimateController = null;
        }
        
        [Test]
        public void Insert_should_call_InsertEstimate_method_of_cache_manager_when_projectId_is_not_exsit()
        {
            //Arange
            _projectId = "76";
            _selectedPoker = "5";
            _userId = 36;
            _estimates = new Estimates();
            _estimate = new Estimate
            {
                UserId = _userId,
                ProjectId = _projectId,
                SelectedPoker = _selectedPoker
            };
            _estimates.EstimateList.Add(_estimate);
            //Act
            _estimateController.Insert(_estimate);
            //Assert
            _cacheManager.Verify(x => x.Add(It.IsAny<string>(), _estimates), Times.Never);
        }

        [Test]
        public void Insert_should_call_UpdateEsitmate_of_cache_manager_once_when_projectId_is_exsit_and_user_is_not_existed()
        {

            //Arange
            _projectId = "76";
            _selectedPoker = "5";
            _userId = 36;
            _estimates = new Estimates();
            _estimate = new Estimate
            {
                UserId = _userId,
                ProjectId = _projectId,
                SelectedPoker = _selectedPoker
            };
            _estimates.EstimateList.Add(_estimate);
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            _cacheManager.Setup(x => x.Get<Estimates>(It.IsAny<string>())).Returns(new Estimates());

            _estimates.EstimateList.Add(_estimate);
            //Act
            _estimateController.Insert(_estimate);
            //Assert
            _cacheManager.Verify(x => x.Get<Estimates>(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Insert_should_call_UpdateEsitmate_of_cache_manager_once_when_projectId_is_exsit_and_uesr_is_exist()
        {
            //Arange
            _projectId = "76";
            _selectedPoker = "5";
            _userId = 36;
            _estimates = new Estimates();
            _estimate = new Estimate
            {
                UserId = _userId,
                ProjectId = _projectId,
                SelectedPoker = _selectedPoker
            };
            _estimates.EstimateList.Add(_estimate);
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            _cacheManager.Setup(x => x.Get<Estimates>(It.IsAny<string>())).Returns(_estimates);

            _estimates.EstimateList.Add(_estimate);
            //Act
            _estimateController.Insert(_estimate);
            //Assert
            _cacheManager.Verify(x => x.Get<Estimates>(It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void Delete_should_call_remove_method_of_cache_manager_once_when_projectId_is_exsit()
        {
            //Arange
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            //Act
            _estimateController.Delete(_projectId);
            //Assert
            _cacheManager.Verify(x => x.Remove(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Delete_should_never_call_delete_method_of_cache_manager_once_when_projectId_is_not_exsit()
        {
            //Arange
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(false);
            //Act
            _estimateController.Delete(_projectId);
            //Assert
            _cacheManager.Verify(x => x.Remove(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Get_should_call_get_method_when_projectId_is_exsit_and_estimates_larger_than_zero()
        {
            //Arange
            _projectId = "76";
            _selectedPoker = "5";
            _userId = 36;
            _estimates = new Estimates();
            _estimate = new Estimate
            {
                UserId = _userId,
                ProjectId = _projectId,
                SelectedPoker = _selectedPoker
            };
            _estimates.EstimateList.Add(_estimate);
            _estimates.IsShow = true;
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            _cacheManager.Setup(x => x.Get<Estimates>(_projectId)).Returns(_estimates);
            _userLogicMock.Setup(x => x.Get(_userId)).Returns(new UserLogicModel());
            //Act
            var result = _estimateController.Get(_projectId);
            //Assert
            _userLogicMock.Verify(x => x.Get(_userId), Times.Once);
            Assert.AreEqual(1, result.EstimateViewModel.Count);
        }

        [Test]
        public void Get_should_return_null_when_projectId_is_not_exsit()
        {
            //Arange
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(false);
            //Act
            var result = _estimateController.Get(It.IsAny<string>());
            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Get_should_call_get_method_when_projectId_is_exsit_and_return_estimate()
        {
            //Arange
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            _cacheManager.Setup(x => x.Get<Estimates>(_projectId)).Returns(new Estimates());
            //Act
            var result = _estimateController.Get(It.IsAny<string>());
            //Assert
            Assert.IsNotNull(result);
        }
        
        [Test]
        public void ShowCard_should_call_show_card_method_of_cache_manager_once_when_projectId_is_exsit()
        {
            //Arange
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            _cacheManager.Setup(x => x.Get<Estimates>(_projectId)).Returns(new Estimates());
            //Act
            _estimateController.ShowCard(_projectId);
            //Assert
            _cacheManager.Verify(x => x.Get<Estimates>(_projectId), Times.Once);
            Assert.IsNotNull(_cacheManager.Object.Get<Estimates>(_projectId));
        }

        [Test]
        public void ShowCard_should_return_when_projectId_is_not_exsit()
        {
            //Arange

            //Act
            _estimateController.ShowCard(_projectId);
            //Assert
            _cacheManager.Verify(x => x.Get<Estimates>(_projectId), Times.Never);
            Assert.IsNull(_cacheManager.Object.Get<Estimates>(_projectId));
        }

        [Test]
        public void IsCleared_should_call_is_cleared_method_of_cache_manager_once()
        {
            //Arange

            //Act
            _estimateController.IsCleared(_projectId);
            //Assert
            _cacheManager.Verify(x => x.KeyExist(_projectId), Times.Once);
        }

    }
}
