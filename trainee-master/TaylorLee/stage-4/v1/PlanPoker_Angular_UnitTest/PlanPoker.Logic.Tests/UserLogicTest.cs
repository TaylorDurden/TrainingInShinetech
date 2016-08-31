using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic.Tests
{
    [TestFixture]
    public class UserLogicTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock;
        private Mock<IUnitOfWork> _unitOfWork;
        private UserLogic _userLogic;
        

        [SetUp]
        public void Init()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _userLogic = new UserLogic(_userRepositoryMock.Object, _unitOfWorkFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _userRepositoryMock = null;
            _unitOfWorkFactoryMock = null;
            _userLogic = null;
        }

        [Test]
        public void Create_should_return_registered_info_when_user_name_exists()
        {
            //Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = "10",
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var user = new User
            {
                UserId = 10,
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var users=new List<User>
            {
                user
            };
            _userRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());

            var actual = _userLogic.Create(userLogicModel);

            //Assert
            Assert.AreEqual("Sorry, this username was registered.", actual);
        }

        [Test]
        public void Create_should_return_empty_info_when_user_name_not_exists()
        {
            //Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = "10",
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var user = new User
            {
                UserId = 10,
                UserName = "lee123",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };

            _userRepositoryMock.Setup(x => x.Query()).Returns(new List<User>().AsQueryable());
            _unitOfWorkFactoryMock.Setup(u => u.GetCurrentUnitOfWork()).Returns(_unitOfWork.Object);
            _userRepositoryMock.Setup(x => x.Create(userLogicModel.ConvertToModel())).Returns(user);

            var actual = _userLogic.Create(userLogicModel);

            //Assert
            Assert.AreEqual("", actual);
        }

        [Test]
        public void Edit_should_execute_once()
        {
            //Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = "10",
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            _unitOfWorkFactoryMock.Setup(u => u.GetCurrentUnitOfWork()).Returns(_unitOfWork.Object);
            _userRepositoryMock.Setup(r => r.Edit(userLogicModel.ConvertToModel()));

            //Act
            _userLogic.Edit(userLogicModel);

            //Assert
            _userRepositoryMock.Verify(x => x.Edit(It.IsAny<User>()), Times.Once);

        }

        [Test]
        public void Delete_should_execute_once()
        {
            //Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = "10",
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            
            _unitOfWorkFactoryMock.Setup(u => u.GetCurrentUnitOfWork()).Returns(_unitOfWork.Object);
            _userRepositoryMock.Setup(r => r.Delete(int.Parse(userLogicModel.UserId)));

            //Act
            _userLogic.Delete(int.Parse(userLogicModel.UserId));

            //Assert
            _userRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);

        }

        [Test]
        public void Login_should_return_registed_user_name_info_when_the_user_name_not_exists()
        {
            //Arrange
            var user = new User
            {
                UserId = 10,
                UserName = "lee",
                Email = "123@qq.com",
                Password = "1243",
                Image = "qwer"
            };
            string userName = "lee";
            string password = "123";

            _userRepositoryMock.Setup(x => x.Get(userName)).Returns(user);

            //Act
            var actual = _userLogic.Login(userName, password);

            //Assert
            Assert.AreEqual("The password is invalid.", actual);
        }

        [Test]
        public void Login_should_return_password_invalid_info_when_the_password_is_wrong()
        {
            //Arrange
            string userName = "lee";
            string password = "123";

            _userRepositoryMock.Setup(x => x.Get(userName)).Returns((User) null);

            //Act
            var actual = _userLogic.Login(userName, password);

            //Assert
            Assert.AreEqual("The user name is not registered.", actual);
        }

        [Test]
        public void Login_should_return_user_id_when_log_in_successfully()
        {
            //Arrange
            var user = new User
            {
                UserId = 10,
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            string userName = "lee";
            string password = "123";

            _userRepositoryMock.Setup(x => x.Get(userName)).Returns(user);

            //Act
            var actual = _userLogic.Login(userName, password);

            //Assert
            Assert.AreEqual(user.UserId.ToString(), actual);
        }

        [Test]
        public void GetAll_return_all_users_when_there_are_users_info()
        {
            //Arrange
            var user = new User
            {
                UserId = 10,
                UserName = "lee123",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var users = new List<User>
            {
                user
            };
            
            var userLogicModel = new UserLogicModel
            {
                UserId = "10",
                UserName = "lee123",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var userLogicModels = new List<UserLogicModel>
            {
                userLogicModel
            };
            _userRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());

            //Act
            _userLogic.GetAll();
            //Assert
            Assert.AreEqual(userLogicModels.Count, users.Count);
        }

        [Test]
        public void GetAll_return_empty_when_there_are_no_users_info()
        {
            //Arrange
            var users = new List<User>();

            _userRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());
            //Act
            _userLogic.GetAll();
            //Assert
            Assert.AreEqual(users.Count, 0);
        }

        [Test]
        public void Get_should_return_a_user_info_when_the_user_exists()
        {
            //Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = "10",
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var user = new User
            {
                UserId = 10,
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            _userRepositoryMock.Setup(x => x.Get(int.Parse(userLogicModel.UserId))).Returns(user);
            //Act
            var actual = _userLogic.Get(int.Parse(userLogicModel.UserId));
            //Assert
            Assert.AreEqual(userLogicModel.UserId, actual.UserId);
            Assert.AreEqual(userLogicModel.UserName, actual.UserName);
            Assert.AreEqual(userLogicModel.Email, actual.Email);
            Assert.AreEqual(userLogicModel.Password, actual.Password);
            Assert.AreEqual(userLogicModel.Image, actual.Image);
        }

        [Test]
        public void CheckUserByName_should_return_true_when_user_name_exists()
        {
            //Arrange
            var user = new User
            {
                UserId = 10,
                UserName = "lee123",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var users = new List<User>
            {
                user
            };
            _userRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());
            //Act
            var actual = _userLogic.CheckUserByName(user.UserName);
            //Assert
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void QueryByName_should_return_empty_when_user_name_not_exists()
        {
            //Arrange
            string userName = "lee";
            _userRepositoryMock.Setup(x => x.Query()).Returns(new List<User>().AsQueryable());
            //Act
            var actual = _userLogic.QueryByName(userName);
            //Assert
            Assert.AreEqual(0, actual.Count());            
        }

        [Test]
        public void QueryByName_should_return_user_logic_models_when_user_name_exists()
        {
            //Arrange
            var user = new User
            {
                UserId = 10,
                UserName = "lee123",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var users = new List<User>
            {
                user
            };

            var userLogicModel = new UserLogicModel
            {
                UserId = "10",
                UserName = "lee123",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var userLogicModels = new List<UserLogicModel>
            {
                userLogicModel
            };
            _userRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());

            //Act
            var actual = _userLogic.QueryByName(userLogicModel.UserName);
            //Assert
            Assert.AreEqual(userLogicModels.Count, actual.Count());
        }

        [Test]
        public void GetUserImage_should_return_image_data_with_the_format_of_string()
        {
            //Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = "10",
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            var user = new User
            {
                UserId = 10,
                UserName = "lee",
                Email = "123@qq.com",
                Password = "123",
                Image = "qwer"
            };
            _userRepositoryMock.Setup(x => x.Get(userLogicModel.UserName)).Returns(user);
            //Act
            var actual = _userLogic.GetUserImage(userLogicModel.UserName);
            //Assert
            Assert.AreEqual(userLogicModel.Image, actual);
        }
    }
}
