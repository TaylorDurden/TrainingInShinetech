using System.Collections.Generic;
using System.Linq;
using PlanPoker.Data.Models;
using NUnit.Framework;
using Moq;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic.Test
{
    [TestFixture]
    public class UserLogicTest
    {
        private Mock<IUserRepository> _iuserRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private Mock<IUnitOfWork> _uniteOfWorkMock;

        [SetUp]
        public void SetUp()
        {
            _iuserRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Create_should_return_string_when_create_username_is_exist()
        {
            //Arrange
            var userName = "Jimbo";
            var userLogicModel = new UserLogicModel
            {
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERE"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Get(userName)).Returns(user);
            //Act
            var result = userLogic.Create(userLogicModel);
            //Assert
            Assert.AreEqual("the username was registered, please select a new username to register.", result);
        }

        [Test]
        public void Create_should_return_string_when_create_username_is_no_exist()
        {
            //Arrange
            var userName = "Jimbo";
            var userLogicModel = new UserLogicModel();
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Get(userName)).Returns(user);
            //Act
            var result = userLogic.Create(userLogicModel);
            //Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void Delete_should_delete_user()
        {
            //Arrange
            var _id = 1;
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            //Act
            userLogic.Delete(_id);
            //Assert
            _iuserRepositoryMock.Verify(x => x.Delete(_id), Times.Once);
        }

        [Test]
        public void Edit_should_edit_user()
        {
            //Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = 100,
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERE"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            //Act
            userLogic.Edit(userLogicModel);
            //Assert
            _iuserRepositoryMock.Verify(x => x.Edit(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Login_should_return_string_when_login_username_is_no_exist()
        {
            //Arrange
            var username = "Jimbo";
            var password = "123456";
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Get(username)).Returns((User) null);
            //Act
            var result = userLogic.Login(username, password);
            //Assert
            Assert.AreEqual("the username is not register",result.Message);            
        }

        [Test]
        public void Login_should_return_string_when_login_username_is_exist_but_password_is_wrong()
        {
            //Arrange
            var _username = "Jimbo";
            var _password = "1234567";
            var userLogicModel = new UserLogicModel
            {
                UserId = 100,
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERE"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Get(_username)).Returns(user);
            //Act
            var result = userLogic.Login(_username, _password);
            //Assert
            Assert.AreEqual("the password error", result.Message);
        }

        [Test]
        public void Login_should_return_string_when_login_username_is_exist_and_password_is_right()
        {
            //Arrange
            var _username = "Jimbo";
            var _password = "123456";
            var userLogicModel = new UserLogicModel
            {
                UserId = 100,
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERE"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Get(_username)).Returns(user);
            //Act
            var result = userLogic.Login(_username, _password);
            //Assert
            Assert.AreEqual(userLogicModel.UserId, result.UserId);
            Assert.AreEqual(userLogicModel.UserName,result.UserName);
            Assert.AreEqual(userLogicModel.Password,result.Password);
            Assert.AreEqual(userLogicModel.Email,result.Email);
            Assert.AreEqual(userLogicModel.Image,result.Image);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_should_get_users() {
            //Arrange
            var userModels = new List<User>();
            var userLogicModel = new UserLogicModel
            {
                UserId = 1,
                UserName = "Jimbo",
                Password = "123456",
                Email = "111@qq.com"
            };
            var userLogicModelOther = new UserLogicModel
            {
                UserId = 2,
                UserName = "Jimbo2",
                Password = "123456",
                Email = "111@qq.com"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            userModels.Add(user);
            var userOther = userLogicModelOther.CreateConvert();
            userModels.Add(userOther);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x=>x.Query()).Returns(userModels.AsQueryable());
            //Act
            var result = userLogic.GetAll();
            //Assert            
            Assert.AreEqual(2,result.Count);
        }

        [Test]
        public void Get_should_get_user_by_id() {
            //Arrange
            var userId = 100;
            var userLogicModel = new UserLogicModel
            {
                UserId = 100,
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERE"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Get(userId)).Returns(user);
            //Act
            var result = userLogic.Get(userId);
            //Assert
            Assert.AreEqual(100, result.UserId);
            Assert.AreEqual("Jimbo", result.UserName);
            Assert.AreEqual("123456", result.Password);
            Assert.AreEqual("11@qq.com", result.Email);
            Assert.AreEqual("data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERE", result.Image);
        }

        [Test]
        public void QueryByName_should_return_count_when_username_is_no_exist() {
            //Arrange
            var userName = "Jimbo Test";
            var users = new List<User>();
            var userLogicModel = new UserLogicModel
            {
                UserId = 100,
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERE"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            users.Add(user);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());
            _iuserRepositoryMock.Setup(x => x.Get(userName)).Returns(user);
            //Act
            var result = userLogic.QueryByName(userName);
            //Assert
            Assert.AreEqual(0, result.Count);            
        }

        [Test]
        public void QueryByName_should_return_count_when_username_is_exist()
        {
            //Arrange
            var userName = "Jimbo";
            var users = new List<User>();
            var userLogicModel = new UserLogicModel
            {
                UserId = 100,
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERE"
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            users.Add(user);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());
            _iuserRepositoryMock.Setup(x => x.Get(userName)).Returns(user);
            //Act
            var result = userLogic.QueryByName(userName);
            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetUserImage_should_return_string()
        {
            //Arrange
            var _userName = "Jimbo";
            var userLogicModel = new UserLogicModel
            {
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,QtknA6d4P3p/2Q=="
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Get(_userName)).Returns(user);
            //Act
            var result = userLogic.GetUserImage(_userName);
            //Assert
            Assert.AreEqual("data:image/jpeg;base64,QtknA6d4P3p/2Q==",result);
        }

        [Test]
        public void CheckExist_should_return_false_when_username_is_no_exist()
        {
            //Arrange
            var userName = "Jimbo Test";
            var users = new List<User>();
            var userLogicModel = new UserLogicModel
            {
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,QtknA6d4P3p/2Q=="
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            users.Add(user);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x => x.Query()).Returns(users.AsQueryable());
            //Act
            var result = userLogic.CheckExist(userName);
            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckExist_should_return_true_when_username_is_exist()
        {
            //Arrange
            var userName = "Jimbo";
            var users = new List<User>();
            var userLogicModel = new UserLogicModel
            {
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,QtknA6d4P3p/2Q=="
            };
            var userLogic = new UserLogic(_iuserRepositoryMock.Object, _unitOfWorkFactory.Object);
            var user = userLogicModel.CreateConvert();
            users.Add(user);
            _unitOfWorkFactory.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _iuserRepositoryMock.Setup(x =>x.Query()).Returns(users.AsQueryable());
            //Act
            var result = userLogic.CheckExist(userName);
            //Assert
            Assert.IsTrue(result);
        }
    }
}
