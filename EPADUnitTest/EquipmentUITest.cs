using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
//help from http://dotnet.kapenilattex.com/?p=1097
namespace EPADUnitTest
{
    [TestFixture]
    public class Driver
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Create a new instance of the IE driver
            driver = new InternetExplorerDriver();
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        [Test]
        public void ViewEquipment()
        {

            AdminLogin();

            IWebElement equipmentLink = driver.FindElement(By.LinkText("Equipment"));
            equipmentLink.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until((d) => { return d.Title.StartsWith("Index"); });

            Assert.IsTrue(driver.Title.StartsWith(("Index")));



            IWebElement details = driver.FindElement(By.LinkText("Details"));
            details.Click();

            wait.Until((d) => { return d.Title.StartsWith("Details"); });

            Assert.IsTrue(driver.Title.StartsWith(("Details")));


        }
        [Test]
        public void AdminLogin()
        {
            //Navigate to the site
            driver.Navigate().GoToUrl("http://localhost:12330/Account/Login");

            //Find the elements of the login form we need
            IWebElement userName = driver.FindElement(By.Name("UserName"));
            IWebElement password = driver.FindElement(By.Name("Password"));

            //Enter our account details
            userName.SendKeys("AdminAcc");
            password.SendKeys("password");

            // Now submit the form
            password.SendKeys(Keys.Return);

            //Selenium is very slow, so make sure the page has loaded
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until((d) => { return d.Title.StartsWith("Home Page"); });

            //Check that we are logged in to our account
            IWebElement login = driver.FindElement(By.Id("login"));
            Assert.IsTrue(login.Text.Contains("AdminAcc"));
        }
    }
}