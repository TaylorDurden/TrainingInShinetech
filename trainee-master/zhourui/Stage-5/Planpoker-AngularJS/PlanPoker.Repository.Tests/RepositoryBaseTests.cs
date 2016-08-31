using System;
using System.Collections.Generic;
using AutoMoq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using PlanPoker.Data;
using PlanPoker.Data.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Castle.Windsor;
using PlanPoker.IRepository;

namespace PlanPoker.Repository.Tests
{
    [TestFixture]
    public class RepositoryBaseTests
    {
        private AutoMoqer _mocker;
        

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
        }

        [Test]
        public void Get_should_return_1()
        {
            // Arrange
            

            // Act
            
            // Assert
            
        }
    }
}
