using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AutoMoq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.IRepository;

namespace PlanPoker.Logic.Tests.Installer
{
    [TestFixture]
    public class LogicInstallerTests
    {
        private AutoMoqer _mocker;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
        }

        [Test]
        public void Install_()
        {
            // Arrange
            
            // Act
            
            // Assert

        }
    }
}
