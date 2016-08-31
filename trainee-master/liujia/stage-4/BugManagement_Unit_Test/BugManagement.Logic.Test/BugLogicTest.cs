using System;
using System.Collections.Generic;
using System.Linq;
using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.Logic;
using BugManagement.Logic.Models;
using Moq;
using NUnit.Framework;

namespace BugManagement.Logic.Test
{
    [TestFixture]
    public class BugLogicTest
    {
        private Mock<IBugRepository> _bugRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactotyMock;
        private Mock<ICauseBugDeveloperRepository> _causeBugDeveloperRepositoryMock;
        private Mock<IDocumentRepository> _documentRepositorymMock;
        private BugLogic _bugLogic;
        private Mock<IUnitOfWork> _uniteOfWorkMock;

        [SetUp]
        public void Init()
        {
            _bugRepositoryMock = new Mock<IBugRepository>();
            _unitOfWorkFactotyMock = new Mock<IUnitOfWorkFactory>();
            _causeBugDeveloperRepositoryMock = new Mock<ICauseBugDeveloperRepository>();
            _documentRepositorymMock = new Mock<IDocumentRepository>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();

            _bugLogic = new BugLogic(_bugRepositoryMock.Object, _unitOfWorkFactotyMock.Object, _causeBugDeveloperRepositoryMock.Object, _documentRepositorymMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _bugRepositoryMock = null;
            _unitOfWorkFactotyMock = null;
            _causeBugDeveloperRepositoryMock = null;
            _documentRepositorymMock = null;
            _bugLogic = null;
            _uniteOfWorkMock = null;
        }

        [Test]
        public void Create_should_call_create_method_of_bug_repository_once()
        {
            //Arange
            var bugLogicModel = new BugLogicModel()
            {
                BugId=1,
                Creator="test",
                Description="description",
                DeveloperId=1,
                ProjectId=1,
                Smmary="smmary",
                Status="status",
                Title="title",
                Type=1,
                Createtime=DateTime.Now
            };
            var developerLogicModel = new DeveloperLogicModel()
            {
                DeveloperId=1,
                FristName="fristName",
                LastName="lastName",
                Status="status",
                Email="email",
                UserId=1
            };
            var developerLogicModels = new List<DeveloperLogicModel>() { developerLogicModel };
            var strDocuments = "1,2,3";
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);

            //Act
            _bugLogic.Create(bugLogicModel, developerLogicModels, strDocuments);

            //Assert
            _bugRepositoryMock.Verify(n => n.Create(It.IsAny<Bug>()),Times.Once);
            _uniteOfWorkMock.Verify(n=>n.Commit(),Times.Once);
            
        }

        [Test]
        public void Edit_should_call_edit_method_of_bug_repository_once()
        {
            //Arange
            var bugLogicModel = new BugLogicModel()
            {
                BugId = 1,
                Creator = "test",
                Description = "description",
                DeveloperId = 1,
                ProjectId = 1,
                Smmary = "smmary",
                Status = "status",
                Title = "title",
                Type = 1,
                Createtime = DateTime.Now
            };
            var developerLogicModel = new DeveloperLogicModel()
            {
                DeveloperId = 1,
                FristName = "fristName",
                LastName = "lastName",
                Status = "status",
                Email = "email",
                UserId = 1
            };
            var developerLogicModels = new List<DeveloperLogicModel>() { developerLogicModel };
            var strDocuments = "1,2,3";
            var causeBugDeveloper = new CauseBugDeveloper()
            {
                BugId=1,
                Id=1,
                DeveloperId=1
            };
            var causeBugDeveloperDBs = new List<CauseBugDeveloper>() { causeBugDeveloper, causeBugDeveloper };
            var document = new Document()
            {
                BugId=1,
                DocumentId=1,
                Path="path"
            };
            var documentDBs = new List<Document>() { document, document, document };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _causeBugDeveloperRepositoryMock.Setup(n => n.Query()).Returns(causeBugDeveloperDBs.AsQueryable());
            _documentRepositorymMock.Setup(n => n.Query()).Returns(documentDBs.AsQueryable());

            //Act
            _bugLogic.Edit(bugLogicModel, developerLogicModels, strDocuments);

            //Assert
            _bugRepositoryMock.Verify(n => n.Edit(It.IsAny<Bug>()), Times.Once);
            _causeBugDeveloperRepositoryMock.Verify(n=>n.Delete(It.IsAny<int>()),Times.AtLeastOnce);
            _documentRepositorymMock.Verify(n=>n.Delete(It.IsAny<int>()),Times.AtLeastOnce);
            _uniteOfWorkMock.Verify(n => n.Commit(), Times.Once);
            

        }

        [Test]
        public void Delete_should_call_delete_method_of_bug_repository_once()
        {
            //Arange
            var bugId = 1;
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);

            //Act
            _bugLogic.Delete(bugId);

            //Assert
            _bugRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_menthod_of_bug_repository_once_return_bugLogicModel()
        {
            //Arange
            var bugId = 1;
            var bug = new Bug()
            {
                BugId=1,
                Createtime=DateTime.Now,
                Creator="test",
                Description="test",
                ProjectId=1,
                Smmary="smmary",
                Status="status",
                Title="title",
                Type=1
            };
            _bugRepositoryMock.Setup(n => n.Get(It.IsAny<int>())).Returns(bug);

            //Act
            var result = _bugLogic.Get(bugId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.Type);
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_bug_repository_once_return_bugLogicModel_list()
        {
            //Arange
            var bug = new Bug()
            {
                BugId = 1,
                Createtime = DateTime.Now,
                Creator = "test",
                Description = "test",
                ProjectId = 1,
                Smmary = "smmary",
                Status = "status",
                Title = "title",
                Type = 1
            };
            var bugs = new List<Bug>() {bug};
            _bugRepositoryMock.Setup(n => n.Query()).Returns(bugs.AsQueryable());

            //Act
            var result = _bugLogic.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.Count);
        }

        [Test]
        public void GetBugLogicModelsBySerchCondition_should_call_get_bugLogicModels_by_serch_condition_method_of_bug_repository_once_return_bugLogicModel_list()
        {
            //Arange
            var strSerchCondition = "";
            var bug = new Bug()
            {
                BugId = 1,
                Createtime = DateTime.Now,
                Creator = "test",
                Description = "test",
                ProjectId = 1,
                Smmary = "smmary",
                Status = "status",
                Title = "title",
                Type = 1
            };
            var bugs = new List<Bug>() { bug };
            _bugRepositoryMock.Setup(n => n.Query()).Returns(bugs.AsQueryable());

            //Act
            var result = _bugLogic.GetBugLogicModelsBySerchCondition(strSerchCondition);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetPageCountByCondition_should_call_get_pageCount_by_condition_method_of_bug_repository_once_return_pageCount()
        {
            //Arange
            var strSerchCondition = "";
            var bug = new Bug()
            {
                BugId = 1,
                Createtime = DateTime.Now,
                Creator = "test",
                Description = "test",
                ProjectId = 1,
                Smmary = "smmary",
                Status = "status",
                Title = "title",
                Type = 1
            };
            var bugs = new List<Bug>() { bug };
            _bugRepositoryMock.Setup(n => n.Query()).Returns(bugs.AsQueryable());

            //Act
            var result = _bugLogic.GetPageCountByCondition(strSerchCondition);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result);
        }

        [Test]
        public void GetBugLogicModelsByCondition_should_call_get_bugLogicModels_by_condition_method_of_bug_repository_once_return_bugLogicModel_list()
        {
            //Arange
            var strSerchCondition = "";
            var pageIndex = 5;
            var pageSize = 3;
            var count = 6;
            var bug = new Bug()
            {
                BugId = 1,
                Createtime = DateTime.Now,
                Creator = "test",
                Description = "test",
                ProjectId = 1,
                Smmary = "smmary",
                Status = "status",
                Title = "title",
                Type = 1
            };
            var bugs = new List<Bug>() { bug, bug, bug, bug, bug, bug, bug };
            _bugRepositoryMock.Setup(n => n.Query()).Returns(bugs.AsQueryable());

            //Act
            var result = _bugLogic.GetBugLogicModelsByCondition(strSerchCondition,pageIndex,pageSize,count);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void UpdateBugStatus_should_call_update_bugStatus_method_of_bug_repository_once()
        {
            //Arange
            var userId = 1;
            var stauts = "status";
            var bug = new Bug()
            {
                BugId = 1,
                Createtime = DateTime.Now,
                Creator = "test",
                Description = "test",
                ProjectId = 1,
                Smmary = "smmary",
                Status = "status",
                Title = "title",
                Type = 1
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _bugRepositoryMock.Setup(n => n.Get(It.IsAny<int>())).Returns(bug);
            _bugRepositoryMock.Setup(n => n.Edit(It.IsAny<Bug>()));

            //Act
            _bugLogic.UpdateBugStatus(userId,stauts);

            //Assert
            _bugRepositoryMock.Verify(n=>n.Get(It.IsAny<int>()),Times.Once);
            _bugRepositoryMock.Verify(n => n.Edit(It.IsAny<Bug>()), Times.Once);
        }
    }
}
