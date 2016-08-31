using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using NUnit.Framework;
using Moq;
using PlanPoker.WebAPI.Controllers;

namespace PlanPokerWebAPITest
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<User> _userMock;
        private Mock<IUserLogic> _userLogicMock;
        private UserController _userController;
        private int _id;
        private string _name;

        [SetUp]
        public void setUp()
        {
            _id = 1;
            _name = "Bill";
            _userMock = new Mock<User>();
            _userLogicMock = new Mock<IUserLogic>();
            _userController = new UserController(_userLogicMock.Object);

        }

        [TearDown]
        public void tearDown()
        {


        }

        [Test]
        public void Create_should_create_user() {
            //Arrange

            //Act
            //_userController.Create(_userMock.Object);
            //Assert
            //_userLogicMock.Verify(x=>x.Create(_userMock.Object),Times.Once);
        }

        [Test]
        public void Delete_should_delete_user() {
            //Arrange

            //Act
            _userController.Delete(_id);
            //Assert
            _userLogicMock.Verify(x=>x.Delete(_id), Times.Once);
        }

        [Test]
        public void Edit_should_edit_user()
        {
            //Arrange

            //Act
            _userController.Edit(_userMock.Object);
            //Assert
            _userLogicMock.Verify(x => x.Edit(_userMock.Object), Times.Once);
        }



        [Test]
        public void GetAll_should_get_users()
        {
            //Arrange

            //Act
            _userController.GetAll();
            //Assert
            _userLogicMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Login_should_get_user() {

        }
        [Test]
        public void QueryByName_should_get_user() {
            //Arrange

            //Act
            _userController.QueryByName(_name);
            //Assert
            _userLogicMock.Verify(x => x.QueryByName(_name), Times.Once);
        }

        [Test]
        public void CheckExist_should_exist_user() {
            //Arrang

            //Act
            _userController.CheckExist(_name);
            //Assert
            _userLogicMock.Verify(x=>x.CheckExist(_name),Times.Once);
        }

        [Test]
        public void GetById_should_get_user() {
            //Arrange

            //Act
            _userController.GetById(_id);
            //Assert
            _userLogicMock.Verify(x => x.Get(_id),Times.Once);
        }
    }
}
