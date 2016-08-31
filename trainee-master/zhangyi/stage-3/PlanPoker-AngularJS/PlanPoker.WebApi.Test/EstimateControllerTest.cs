using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Common;
using PlanPoker.Common;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
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
        private Mock<ICacheManager>  _cacheManager;
        private Mock<IUserLogic> _userLogicMock;
        private EstimateController _estimateController;

        [SetUp]
        public void Init()
        {
            _projectId = "76";
            _selectedPoker = "5";
            _userId = 36;
            _cacheManager = new Mock<ICacheManager>();
            _userLogicMock=new Mock<IUserLogic>();
            _estimateController = new EstimateController(_cacheManager.Object, _userLogicMock.Object);
        }

        [Test]
        public void Insert_should_call_add_method_of_cache_manager_once_when_projectId_is_not_exsit()
        {
            //Range
            var estimates=new Estimates();
            var estimate =new Estimate
            {
                UserId = _userId,
                ProjectId = _projectId,
                SelectedPoker = _selectedPoker
            };
            estimates.EstimateList.Add(estimate);
            //Act
            _estimateController.Insert(estimate);
            //Assert
            _cacheManager.Verify(x => x.Add(estimate.ProjectId, estimates), Times.Never);
        }

        [Test]
        public void Insert_should_add_estimate_of_cache_manager_once_when_projectId_is_exsit()
        {
            //Range
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            _cacheManager.Setup(x => x.Get<Estimates>(_projectId)).Returns(new Estimates());
            var estimates = new Estimates();
            var estimate = new Estimate
            {
                UserId = _userId,
                ProjectId = _projectId,
                SelectedPoker = _selectedPoker
            };
            estimates.EstimateList.Add(estimate);
            //Act
            _estimateController.Insert(estimate);
            //Assert
            _cacheManager.Verify(x => x.Get<Estimates>(estimate.ProjectId), Times.Once);
        }

        [Test]
        public void Delete_should_call_remove_method_of_cache_manager_once_when_projectId_is_exsit()
        {
            //Range
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            //Act
            _estimateController.Delete(_projectId);
            //Assert
            _cacheManager.Verify(x => x.Remove(_projectId), Times.Once);
        }

        [Test]
        public void Delete_should_never_call_delete_method_of_cache_manager_once_when_projectId_is_not_exsit()
        {
            //Range
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(false);
            //Act
            _estimateController.Delete(_projectId);
            //Assert
            _cacheManager.Verify(x => x.Remove(_projectId), Times.Never);
        }

        [Test]
        public void Get_should_return_null_when_projectId_is_not_exsit()
        {
            //Range
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(false);
            //Act
            var result=_estimateController.Get(_projectId);
            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ShowCard_should_call_show_card_method_of_cache_manager_once_when_projectId_is_exsit()
        {
            //Range
            _cacheManager.Setup(x => x.KeyExist(It.IsAny<string>())).Returns(true);
            _cacheManager.Setup(x => x.Get<Estimates>(_projectId)).Returns(new Estimates());
            //Act
            _estimateController.ShowCard(_projectId);
            //Assert
            _cacheManager.Verify(x => x.Get<Estimates>(_projectId), Times.Once);
        }

        [Test]
        public void ShowCard_should_do_nothing_when_projectId_is_not_exsit()
        {
            //Range

            //Act
            _estimateController.ShowCard(_projectId);
            //Assert
            _cacheManager.Verify(x => x.Get<Estimates>(_projectId), Times.Never);
        }

        [Test]
        public void IsCleared_should_call_is_cleared_method_of_cache_manager_once()
        {
            //Range

            //Act
            _estimateController.IsCleared(_projectId);
            //Assert
            _cacheManager.Verify(x => x.KeyExist(_projectId), Times.Once);
        }
        

    }
}
