using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using Protractor;

namespace SmartMemberTests
{
    [CodedUITest]
    public class LoginRegistrationTests
    {
        private IWebDriver _driver = new FirefoxDriver();

        private string _userEmail = "alexmoll@knights.ucf.edu";
        private string _userPassword = "Password1";
        private string _siteName = "Testing Awesome Site";
        private string _companyName = "What a Company!";
        private string _siteSubDomain = "testingMANIA";
        private string _signupuserName = "Alexx";
        private string _signupuserPassword = "Password1";
        private string _signupuserEmail = "alexmoll@knights.ucf.edu";
        private string _moduleTitle = "Test Module Fun";

        public string SignupUserName
        {
            get { return _signupuserName; }

            set { _signupuserName = value; }
        }

        public string UserEmail
        {
            get { return _userEmail; }

            set { _userEmail = value; }
        }

        public string UserPassword
        {
            get { return _userPassword; }

            set { _userPassword = value; }
        }

        public string SiteName
        {
            get { return _siteName; }

            set { _siteName = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }

            set { _companyName = value; }
        }

        public string SiteSubDomain
        {
            get { return _siteSubDomain; }

            set { _siteSubDomain = value; }
        }

        public string SignupuserPassword
        {
            get { return _signupuserPassword; }

            set { _signupuserPassword = value; }
        }

        public string SignupuserEmail
        {
            get { return _signupuserEmail; }

            set { _signupuserEmail = value; }
        }

        public string ModuleTitle
        {
            get { return _moduleTitle; }

            set { _moduleTitle = value; }
        }

        [TestMethod]
        public void Login(string email, string pass)
        {

            _driver.Navigate().GoToUrl("www.my.smartmember.com");
            Console.WriteLine("Beginning Login Test");

            _driver.Navigate().GoToUrl("http://my.smartmember.com");


            Playback.Wait(10000);

            _driver.FindElement(NgBy.Model("user.email")).SendKeys(email);
            _driver.FindElement(NgBy.Model("user.password")).SendKeys(pass);
            _driver.FindElement(By.XPath("//form[@ng-submit='login()']")).Submit();
            Playback.Wait(15000);

            Console.WriteLine("User logged in successfully");

        }

        [TestMethod]
        public void SignUp(string userName, string eMail, string pass)
        {
            _driver.Navigate().GoToUrl(" http://sm.smartmember.com/sign/up/hash=5d99d2de51be1c522546d17f4cab7d30");
            _driver.Manage().Window.Maximize();
            Playback.Wait(6000);
            _driver.FindElement(NgBy.Model("user.first_name")).SendKeys(userName);
            _driver.FindElement(NgBy.Model("user.email")).SendKeys(eMail);
            _driver.FindElement(NgBy.Model("user.password")).SendKeys(pass);
            _driver.FindElement(By.XPath("//button[@type='submit']")).Submit();
            Playback.Wait(15000);
            if (_driver.PageSource.Contains("My Profile"))
            {
                Console.WriteLine("User logged in successfully");
            }
            Console.WriteLine("Login failed");

            Playback.Wait(10000);
        }

        public void UnloadDriver()
        {
            Console.WriteLine("TearDown");
            _driver.Quit();
        }

        [TestMethod]
        public void UpdateTeamName(string name)
        {
            
            _driver.FindElement(NgBy.Model("company.name")).SendKeys(name);
            _driver.FindElement(By.XPath("//button[text()='Save & Continue']")).Click();
            Playback.Wait(5000);
        }

        [TestMethod]
        public void CreateNewSite(string name, string subDomain)
        {
            
            _driver.FindElement(NgBy.Model("site.name")).SendKeys(name);
            _driver.FindElement(NgBy.Model("site.subdomain")).SendKeys(subDomain);
            _driver.FindElement(By.XPath("//button[text()='Save & Continue']")).Click();
            Playback.Wait(10000);
            //account for new theme update popup
            if (_driver.FindElement(By.XPath("//button[@ng-click='ok()']")).Displayed)
                _driver.FindElement(By.XPath("//button[@ng-click='ok()']")).Click();
        }
        [TestMethod]
        public void CreateNewModule(string moduleTitle)
        {
            
            _driver.FindElement(NgBy.Model("module.title")).SendKeys(moduleTitle);
            _driver.FindElement(By.XPath("//button[text()='Save & Continue']")).Click();
        }
        [TestMethod]
        public void AddLessonContent(string lessonTitle, string permaLink)
        {
            _driver.FindElement(NgBy.Model("next_item.title")).SendKeys(lessonTitle);
            _driver.FindElement(NgBy.Model("next_item.permalink")).SendKeys(permaLink);
            //Text Tests
            _driver.FindElement(By.XPath("//div[@spellcheck='true' and @dir='auto']")).SendKeys("Test Text Now");
            SendKeys.SendWait("^a");
            _driver.FindElement(
                By.XPath(
                    "html/body/div[3]/div/div[3]/div[1]/div[1]/ui-view/div/div[2]/div/div/div/div/div[3]/div[2]/ng-include/div/div[1]/div/div/fieldset/div[3]/div/div/div[1]/a[2]/i"))
                .Click();
            string boldText = _driver.FindElement(By.XPath("//strong[text()='asdfadsfasdf']"))
                .GetCssValue("font-weight");
            Assert.IsTrue(boldText.Equals("700"));
            //Click List Button after selection text
            _driver.FindElement(By.XPath("//div[@spellcheck='true' and @dir='auto']")).Click();
            SendKeys.SendWait("^a");
            _driver.FindElement(By.XPath("//i[@class='fa fa-list-ul']")).Click();
            string listText =
                _driver.FindElement(By.XPath("//strong[text()='asdfadsfasdf']")).GetCssValue("list-style-type");
            Assert.IsTrue(listText.Equals("disc"));
            _driver.FindElement(NgBy.Model("selectedModule"))
                .FindElement(NgBy.SelectedOption("next_item.module_id==module.id"))
                .Click();
            _driver.FindElement(By.XPath("//button[@type][2]")).Click();
        }
        [TestMethod]
        public void RestrictAccess(string access)
        {
            if (access == "Public")
            {
                new SelectElement(_driver.FindElement(NgBy.Model("access_type"))).SelectByValue("1");
                _driver.FindElement(By.XPath("//button[text()='Save & Continue']")).Click();
            }
            if (access == "Any Member")
            {
                new SelectElement(_driver.FindElement(NgBy.Model("access_type"))).SelectByValue("3");
                _driver.FindElement(By.XPath("//button[text()='Save & Continue']")).Click();
            }
            if (access == "Restricted Members")
            {
                new SelectElement(_driver.FindElement(NgBy.Model("access_type"))).SelectByValue("1");
                //Might want to parameterize this
                _driver.FindElement(NgBy.Model("access.access_level_name")).SendKeys("Gold");
                _driver.FindElement(By.XPath("//button[text()='Save & Continue']")).Click();
            }
            else
            {
                new SelectElement(_driver.FindElement(NgBy.Model("access_type"))).SelectByValue("1");
                _driver.FindElement(By.XPath("//button[text()='Save & Continue']")).Click();
            }


        }
    }
}


    