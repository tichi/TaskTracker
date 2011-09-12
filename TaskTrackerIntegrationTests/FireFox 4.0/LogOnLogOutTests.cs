﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TaskTrackerIntegrationTests.FireFox4
{
    /**
     * \brief Authentication integration tests for FireFox 4.0.
     * \author Katharine Gillis
     * \date 2011-09-10
     * 
     * Runs the authentication integration tests on the FireFox 4.0 browser.
     */
    [TestFixture]
    [Category("FireFox")]
    class LogOnLogOutTests : TaskTrackerIntegrationTests.Base.LogOnLogOutTests
    {
        IWebDriver driver;

        /**
         * \brief Sets the browser type.
         * 
         * This sets the browser type to FireFox 4.0 for use with all the tests in this test fixture.
         */
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.browserType = Base.BROWSER_TYPE.FireFox4;
        }

        /**
         * \brief Closes the browser.
         * 
         * This closes the browser driver once the test has been run.
         */
        [TearDown]
        public void TearDown()
        {
            this.driver.Quit();
        }

        /**
         * \brief Runs the valid log in and log out test.
         * 
         * This runs the valid log in and log out test on the FireFox 4.0 browser.
         */
        [Test]
        public void HomeIndex_LogIn_LogOut()
        {
            this.driver = this.CreateDriver();

            base.HomeIndex_LogIn_LogOut(this.driver);
        }

        /**
         * \brief Runs the invalid log in test.
         * 
         * This runs the invalid log in test on the FireFox 4.0 browser.
         */
        [Test]
        public void HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication()
        {
            this.driver = this.CreateDriver();

            base.HomeIndex_LogOnInvalidUserNameAndPassword_ErrorMessageNoAuthentication(this.driver);
        }
    }
}
