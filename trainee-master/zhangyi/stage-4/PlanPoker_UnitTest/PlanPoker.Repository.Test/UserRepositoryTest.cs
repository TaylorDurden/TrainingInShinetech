using Moq;
using NUnit.Framework;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using PlanPoker.IRepository;

namespace PlanPoker.Repository.Test
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private Mock<IDbFactory> _dbFactoryMock;
        private Mock<IRepository<User>>  _userRepositoryMock;
        private RepositoryBase<User> _userRepositoryBase;
        private UserRepository _userRepository;
        private string _username;
        private string _password;
        private string _email;
        private int _userId;
        private User _user;
        [SetUp]
        public void Init()
        {
            _dbFactoryMock = new Mock<IDbFactory>();
            _userRepositoryBase = new RepositoryBase<User>(_dbFactoryMock.Object);
            _userRepository=new UserRepository(_dbFactoryMock.Object);
            _userRepositoryMock =new Mock<IRepository<User>>();
            _userId = 5;
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _user = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
        }

        [Test]
        public void Get_should_call_get_method_of_user_repository_once()
        {
            //Range
            //_userRepositoryMock.Setup(x => x.Get(_userId)).Returns(_user);
            //_userLogicMock.Setup(x => x.GetAll()).Returns(_userLogicModels);
            //Act
            //var result = _userRepository.Get(_userId);
            //Assert
            //_userRepositoryMock.Verify(x=>x.Get(It.IsAny<int>()),Times.Once);
        }
    }
}

