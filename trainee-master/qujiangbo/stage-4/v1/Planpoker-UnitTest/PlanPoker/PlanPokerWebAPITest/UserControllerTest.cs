using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.WebAPI.Models;
using PlanPoker.WebAPI.Controllers;

namespace PlanPokerWebAPITest
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserLogic> _iuserLogicMock;

        [SetUp]
        public void SetUp()
        {
            _iuserLogicMock = new Mock<IUserLogic>();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Create_should_create_user()
        {
            //Arrange
            var userWebModelMock = new UserWebModel
            {
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD"
            };
            var userController = new UserController(_iuserLogicMock.Object);
            var userLogicModel = userWebModelMock.CreateConvert();
            _iuserLogicMock.Setup(x => x.Create(userLogicModel)).Returns("100");
            //Act
            var result=userController.Create(userWebModelMock);
            //Assert
            _iuserLogicMock.Verify(x => x.Create(It.IsAny<UserLogicModel>()), Times.Once);
            Assert.AreEqual(null, result);
        }

        [Test]
        public void Delete_should_delete_user()
        {
            //Arrange
            var id = 1;
            var userController = new UserController(_iuserLogicMock.Object);
            //Act
            userController.Delete(id);
            //Assert
            _iuserLogicMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Edit_should_edit_user()
        {
            //Arrange
            var userWebModelMock = new UserWebModel
            {
                UserId = 100,
                UserName = "Jimbo",
                Password = "123456",
                Email = "11@qq.com",
                Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/2wBDAAUDBAQEAwUEBAQFBQUGBwwIBwcHBw8LCwkMEQ8SEhEPERETFhwXExQaFR"
            };
            var userController = new UserController(_iuserLogicMock.Object);
            //Act
            userController.Edit(userWebModelMock);
            //Assert
            _iuserLogicMock.Verify(x => x.Edit(It.IsAny<UserLogicModel>()), Times.Once);
        }

        [Test]
        public void GetAll_should_get_users()
        {
            //Arrange
            var userLogicModels = new List<UserLogicModel>();
            var userLogicModel = new UserLogicModel
            {
                UserId = 1,
                UserName = "Jimbo",
                Password = "123456",
                Email = "111@qq.com"
            };
            userLogicModels.Add(userLogicModel);
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x => x.GetAll()).Returns(userLogicModels);
            //Act
            var result=userController.GetAll();
            //Assert
            _iuserLogicMock.Verify(x => x.GetAll(), Times.Once);
            Assert.AreEqual(1,result.Count);
        }

        [Test]
        public void Login_should_get_user()
        {
            //Arrange
            var username = "Jimbo";
            var password = "123456";
            var userLogicModel = new UserLogicModel
            {
                UserId = 1,
                UserName = "Jimbo",
                Password = "123456",
                Email = "111@qq.com"
            };
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x => x.Login(username, password)).Returns(userLogicModel);
            //Act
            var result = userController.Login(username,password);
            //Assert
            _iuserLogicMock.Verify(x => x.Login(username, password), Times.Once);
            Assert.AreEqual(username,result.UserName);
            Assert.AreEqual(password,result.Password);
            Assert.IsNotNull(result);
        }

        [Test]
        public void QueryByName_should_get_user()
        {
            //Arrange
            var name = "Jimbo";
            var userLogicModels = new List<UserLogicModel>();
            var userLogicModel = new UserLogicModel
            {
                UserId = 1,
                UserName = "Jimbo",
                Password = "123456",
                Email = "111@qq.com"
            };
            userLogicModels.Add(userLogicModel);
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x=> x.QueryByName(name)).Returns(userLogicModels);  
            //Act
            var result=userController.QueryByName(name);
            //Assert
            _iuserLogicMock.Verify(x => x.QueryByName(name), Times.Once);
            Assert.AreEqual(1,result.Count);
        }

        [Test]
        public void CheckExist_should_return_true_when_exist_user()
        {
            //Arrang
            var name = "Jimbo";
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x => x.CheckExist(name)).Returns(true);
            //Act
            var result=userController.CheckExist(name);
            //Assert
            _iuserLogicMock.Verify(x => x.CheckExist(name), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckExist_should_return_false_when_no_exist_user()
        {
            //Arrang
            var name = "Jimbo";
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x => x.CheckExist(name)).Returns(false);
            //Act
            var result=userController.CheckExist(name);
            //Assert
            _iuserLogicMock.Verify(x => x.CheckExist(name), Times.Once);
            Assert.IsFalse(result);
        }

        [Test]
        public void GetById_should_get_user()
        {
            //Arrange
            var id = 1;
            var userLogicModel = new UserLogicModel {
                UserId = 1,
                UserName = "Jimbo",
                Password = "123456",
                Email = "111@qq.com"
            };
            var userController = new UserController(_iuserLogicMock.Object);
            _iuserLogicMock.Setup(x => x.Get(id)).Returns(userLogicModel);
            //Act
            var result=userController.GetUserById(id);
            //Assert
            _iuserLogicMock.Verify(x => x.Get(id), Times.Once);
            Assert.AreEqual(1,result.UserId);
            Assert.AreEqual("Jimbo", result.UserName);
            Assert.AreEqual("123456", result.Password);
            Assert.AreEqual("111@qq.com", result.Email);
            Assert.IsNotNull(result);
        }
    }
}
