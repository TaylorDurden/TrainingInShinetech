using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Common;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.WebAPI.Controllers;

namespace PlanPoker.WebApi.Test
{
    [TestFixture]
    public class UserControllerTest
    {
        private string _username;
        private string _password;
        private string _email;
        private int _userId;
        private Mock<IUserLogic> _userLogicMock;
        private UserController _userController;

        [SetUp]
        public void Init()
        {
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userLogicMock = new Mock<IUserLogic>();
            _userController = new UserController(_userLogicMock.Object);
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_user_logic_once()
        {
            //Range

            //Act
            _userController.GetAll();
            //Assert
            _userLogicMock.Verify(x=>x.GetAll(),Times.Once);
        }

        [Test]
        public void Delete_should_call_delete_method_of_user_logic_once()
        {
            //Range

            //Act
            _userController.Delete(_userId);
            //Assert
            _userLogicMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_method_of_user_logic_once()
        {
            //Range

            //Act
            _userController.GetById(_userId);
            //Assert
            _userLogicMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Create_should_create_method_of_user_logic_once()
        {
            //Range
            var user = new User
            {
                UserName = _username,
                Password = _password,
                Email = _email
            };
            //Act
            _userController.Create(user);
            //Assert
            _userLogicMock.Verify(x => x.Create(user), Times.Once);
        }

        [Test]
        public void Edit_should_edit_method_of_user_logic_once()
        {
            //Range
            var user = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
            //Act
            _userController.Edit(user);
            //Assert
            _userLogicMock.Verify(x => x.Edit(user), Times.Once);
        }

        [Test]
        public void Login_should_login_method_of_user_logic_once()
        {
            //Range

            //Act
            _userController.Login(_username,_password);
            //Assert
            _userLogicMock.Verify(x => x.Login(_username, _password), Times.Once);
        }

        [Test]
        public void QueryByName_should_queryByName_method_of_user_logic_once()
        {
            //Range

            //Act
            _userController.QueryByName(_username);
            //Assert
            _userLogicMock.Verify(x => x.QueryByName(_username), Times.Once);
        }
        

    }
}
