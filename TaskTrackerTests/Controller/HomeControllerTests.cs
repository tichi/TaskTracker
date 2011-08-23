﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NUnit.Framework;

using TaskTracker.Controllers;

namespace TaskTrackerTests.Controller
{
    [TestFixture]
    class HomeControllerTests
    {
        [Test]
        public void Index_Get_ReturnsIndexView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));

            ViewResult viewResult = result as ViewResult;

            Assert.That(viewResult.ViewName, Is.EqualTo("Index"));
        }
    }
}