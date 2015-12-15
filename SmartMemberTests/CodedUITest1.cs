using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Protractor;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace SmartMemberTests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        private IWebDriver driver;
        public CodedUITest1()
        {
        }

        
        public void Login()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("www.my.smartmember.com");
            Console.WriteLine("Beginning Login Test");

            driver.Navigate().GoToUrl("http://my.smartmember.com");
            Playback.Wait(15000);
            driver.FindElement(NgBy.Model("user.email")).SendKeys("alexmoll@knights.ucf.edu");
            driver.FindElement(NgBy.Model("user.password")).SendKeys("Password1!");
            driver.FindElement(By.ClassName("ui form ng-pristine ng-valid")).Submit();
            Playback.Wait(10000);
            

            Console.WriteLine("User logged in successfully");
        }
    

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
