//using System.Linq;
//using AutoMoq;
//using FluentAssertions;
//using NUnit.Framework;
//using PlanPoker.Data.Models;
//using NHibernate;
//
//namespace PlanPoker.Repository.Tests
//{
//    [TestFixture]
//    public class UserRepositoryTests
//    {
//        private AutoMoqer _mocker;
//        private ISessionFactory _sessionFactory;
//        private ISession _session;
//        private NHiberanteRepositoryBase<Users> _nHiberanteRepository;
//
//        [SetUp]
//        public void Initialize()
//        {
//            _mocker = new AutoMoqer();
//
//            //_sessionFactory = DbFactory.CreateSessionFactoryForTest();
//            _session = _sessionFactory.OpenSession();
//            _nHiberanteRepository = _mocker.Create<NHiberanteRepositoryBase<Users>>();
//        }
//
//        [TearDown]
//        public void Dispose()
//        {
//            _mocker = null;
//        }
//
//        [Test]
//        public void GetByName_should_return_right_type()
//        {
//            // Arrange
//            var user = new Users
//            {
//                UserId = 0,
//                UserName = "zhao4",
//                Password = "123321",
//                Email = "zhao4@shinetechchina.com",
//                Image = "jpeg"
//            };
//            // Act
//            var result = _nHiberanteRepository.Create(user);
//            // Assert
//            result.Should().NotBe(0);
//        }
//    }
//}
