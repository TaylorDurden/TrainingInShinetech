using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using AutoMoq;
using PlanPoker.Common;
using System.Collections.Generic;
using FluentAssertions;
using Moq;

namespace PlanPoker.Common.Tests
{
    [TestFixture]
    public class MemoryCacheManagerTests
    {
        private AutoMoqer _mocker;
        private MemoryCacheManager _memoryCacheManager;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();
            _memoryCacheManager = _mocker.Create<MemoryCacheManager>();
        }

        [Test]
        public void Add_should_execute_once()
        {
            
        }
    }
}
