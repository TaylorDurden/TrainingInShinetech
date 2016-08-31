using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic.Tests
{
    [TestFixture]
    public class UserLogicTests
    {
        private AutoMoqer _mocker;
        private UserLogic _userLogic;

        [SetUp]
        public void Initialize()
        {
            _mocker = new AutoMoqer();
            _userLogic = _mocker.Create<UserLogic>();
        }

        [TearDown]
        public void Dispose()
        {
            _mocker = null;
            _userLogic = null;
        }

        #region "Create Testing"
        [Test]
        public void Create_when_userLogicModel_is_null_should_return_null()
        {
            // Arrange

            // Act
            var result = _userLogic.Create(null);
            // Assert
            result.Should().Be(null);
        }

        [Test]
        public void Create_when_username_exist_should_return_specific_message()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(user);
            // Act
            var result = _userLogic.Create(user.ConvertToUserLogicModel(""));
            // Assert
            result.Message.Should().Be("the username was registered, please select a new username to register.");
        }

        [Test]
        public void Create_when_email_exist_should_return_specific_message()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var users = new List<User> {user};
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Query()).Returns(users.AsQueryable());
            // Act
            var result = _userLogic.Create(user.ConvertToUserLogicModel(""));
            // Assert
            result.Message.Should().Be("the email was registered, please use another email to register.");
        }

        [Test]
        public void Create_when_username_and_email_not_exist_should_execute_once()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var unitOfWorkFactory = _mocker.GetMock<IUnitOfWorkFactory>();
            var unitOfWork = _mocker.GetMock<IUnitOfWork>();
            unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(unitOfWork.Object);
            var userRepository = _mocker.GetMock<IUserRepository>();
            // Act
            var result = _userLogic.Create(user.ConvertToUserLogicModel(""));
            // Assert
            userRepository.Verify(x=>x.Create(It.IsAny<User>()),Times.Once());
            unitOfWork.Verify(x=>x.Commit(),Times.Once());
            Assert.AreEqual(typeof(UserLogicModel),result.GetType());
        }
        #endregion

        #region "Edit Testing"
        [Test]
        public void Edit_when_userLogicModel_is_null_should_return_null()
        {
            // Arrange

            // Act
            var result = _userLogic.Edit(null);
            // Assert
            result.Should().Be(null);
        }

        [Test]
        public void Edit_when_email_exist_and_email_not_equals_original_email_should_return_specific_message()
        {
            // Arrange
            var originalUser1 = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@163.com",
                Image = "111"
            };
            var originalUser2 = new User
            {
                UserId = 1,
                UserName = "wang8",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = "222"
            };
            var updatedUser = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "321",
                Email = "zhourui@qq.com",
                Image = "333"
            };
            var users = new List<User> {originalUser1, originalUser2};
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Query()).Returns(users.AsQueryable());
            userRepository.Setup(x => x.Get("zhourui")).Returns(originalUser1);
            // Act
            var result = _userLogic.Edit(updatedUser.ConvertToUserLogicModel(""));
            // Assert
            result.Message.Should().Be("the email was registered, please use another email to update.");
        }

        [Test]
        public void Edit_when_username_and_email_not_exist_should_execute_once()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var unitOfWorkFactory = _mocker.GetMock<IUnitOfWorkFactory>();
            var unitOfWork = _mocker.GetMock<IUnitOfWork>();
            unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(unitOfWork.Object);
            var userRepository = _mocker.GetMock<IUserRepository>();
            // Act
            var result = _userLogic.Edit(user.ConvertToUserLogicModel(""));
            // Assert
            userRepository.Verify(x => x.Edit(It.IsAny<User>()), Times.Once());
            unitOfWork.Verify(x => x.Commit(), Times.Once());
            Assert.AreEqual(typeof(UserLogicModel), result.GetType());
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
            var userRepository = _mocker.GetMock<IUserRepository>();
            // Act
            _userLogic.Delete(It.IsAny<int>());
            // Assert
            userRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
            unitOfWork.Verify(x => x.Commit(), Times.Once());
        }
        #endregion

        #region "Login Testing"
        [Test]
        public void Login_when_username_not_exist_should_return_sepcific_string()
        {
            // Arrange
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<string>())).Returns((User) null);
            // Act
            var result = _userLogic.Login(It.IsAny<string>(), It.IsAny<string>());
            // Assert
            result.Message.Should().Be("the username is not register.");
            result.UserId.Should().Be(0);
        }

        [Test]
        public void Login_when_username_exist_and_password_right_should_return_user()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(user);
            // Act
            var result = _userLogic.Login(It.IsAny<string>(), user.Password);
            // Assert
            result.Message.Should().Be("");
            result.UserId.Should().Be(1);
        }

        [Test]
        public void Login_when_username_exist_but_password_not_right_should_return_sepcific_string()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(user);
            // Act
            var result = _userLogic.Login(It.IsAny<string>(), It.IsAny<string>());
            // Assert
            result.Message.Should().Be("the password is error.");
            result.UserId.Should().Be(0);
        }
        #endregion

        #region "Gets Testing"
        [Test]
        public void GetAll_should_execute_once_and_return_1()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123",
                    Email = "zhourui@qq.com",
                    Image = ""
                }

            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Query()).Returns(users.AsQueryable());
            // Act
            var result = _userLogic.GetAll();
            // Assert
            userRepository.Verify(x => x.Query(), Times.Once());
            result.Count().Should().Be(1);
        }

        [Test]
        public void Get_when_id_is_valid_should_return_right_type()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(user);
            // Act
            var result = _userLogic.Get(1);
            // Assert
            userRepository.Verify(x=>x.Get(1),Times.Once());
            Assert.AreEqual(typeof(UserLogicModel),result.GetType());
            result.UserName.Should().Be("zhourui");
        }

        [Test]
        public void Get_when_id_not_valid_should_return_specific_message()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(user);
            // Act
            var result = _userLogic.Get(It.IsAny<int>());
            // Assert
            userRepository.Verify(x=>x.Get(It.IsAny<int>()),Times.Once());
            Assert.AreEqual(typeof(UserLogicModel),result.GetType());
            result.Message.Should().Be("user id should greater than zero.");
        }

        [Test]
        public void Get_when_username_not_empty_should_return_right_type()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(user);
            // Act
            var result = _userLogic.Get("zhourui");
            // Assert
            userRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(typeof(UserLogicModel), result.GetType());
            result.UserId.Should().Be(1);
            result.Message.Should().Be("");
        }

        [Test]
        public void Get_when_username_is_empty_should_return_specific_message()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = ""
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(user);
            // Act
            var result = _userLogic.Get(It.IsAny<string>());
            // Assert
            userRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(typeof(UserLogicModel), result.GetType());
            result.Message.Should().Be("user name cannot be empty.");
        }

        [Test]
        public void GetUserImage_should_execute_once_and_return_image_value()
        {
            //return _userRepository.Get(userName).Image;
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123",
                Email = "zhourui@qq.com",
                Image = "123"
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(user);
            // Act
            var result = _userLogic.GetUserImage(It.IsAny<string>());
            // Assert
            userRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            result.Should().Be("123");
        }
        #endregion

        #region "Check Testing"
        [Test]
        public void CheckIfUsernameExist_when_username_not_exist_should_return_false()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123",
                    Email = "zhourui@qq.com",
                    Image = ""
                }
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Query()).Returns(users.AsQueryable());
            // Act
            var result = _userLogic.CheckIfUsernameExists(It.IsAny<string>());
            // Assert
            result.Should().Be(false);
        }

        [Test]
        public void CheckIfUsernameExist_when_username_exist_should_return_true()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123",
                    Email = "zhourui@qq.com",
                    Image = ""
                }
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Query()).Returns(users.AsQueryable());
            // Act
            var result = _userLogic.CheckIfUsernameExists("zhourui");
            // Assert
            result.Should().Be(true);
        }

        [Test]
        public void CheckIfEmailExist_when_email_not_exist_should_return_false()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123",
                    Email = "zhourui@qq.com",
                    Image = ""
                }
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Query()).Returns(users.AsQueryable());
            // Act
            var result = _userLogic.CheckIfEmailExists(It.IsAny<string>());
            // Assert
            result.Should().Be(false);
        }

        [Test]
        public void CheckIfEmailExist_when_email_exist_should_return_true()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123",
                    Email = "zhourui@qq.com",
                    Image = ""
                }
            };
            var userRepository = _mocker.GetMock<IUserRepository>();
            userRepository.Setup(x => x.Query()).Returns(users.AsQueryable());
            // Act
            var result = _userLogic.CheckIfEmailExists("zhourui@qq.com");
            // Assert
            result.Should().Be(true);
        }
        #endregion
    }
}
