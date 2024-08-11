using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace PhpSystemAutomation
{
    public class PhpSystemTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Initialize the ChromeDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/");
        }

        [Test]
        public void Login_ValidCredentials_ShouldLoginSuccessfully()
        {
            Thread.Sleep(5000);
            driver.FindElement(By.Name("txtuser")).SendKeys("admin");
            driver.FindElement(By.Name("txtpass")).SendKeys("root");
            driver.FindElement(By.ClassName("button")).Click();

            // Verify successful login by checking for a logout button
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.ClassName("school-period")).Displayed);
        }

        [Test]
        public void Login_InvalidCredentials_ShouldShowErrorMessage()
        {
            Thread.Sleep(5000);
            driver.FindElement(By.Name("txtuser")).SendKeys("invalidUsername");
            driver.FindElement(By.Name("txtpass")).SendKeys("invalidPassword");
            driver.FindElement(By.ClassName("button")).Click();

            // Verify error message is displayed
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.ClassName("label")).Displayed);
        }

        [Test]
        public void Logout_RedirectToLogin_ShouldRedirectToLoginPage()
        {
            Thread.Sleep(5000);

            driver.FindElement(By.Name("txtuser")).SendKeys("admin");
            driver.FindElement(By.Name("txtpass")).SendKeys("root");
            driver.FindElement(By.ClassName("button")).Click();

            // Verify successful login by checking for a logout button
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.ClassName("school-period")).Displayed);

            Thread.Sleep(1000);
            driver.FindElement(By.ClassName("icon")).Click();
            driver.FindElement(By.XPath("/html/body/aside/div[1]/div/span[2]/ul/li[2]/a/span")).Click();

            // Verify that the user is redirected to the login page
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(driver.FindElement(By.Name("txtuser")).Displayed);
        }

        [TearDown]
        public void Teardown()
        {
            Thread.Sleep(5000);
            // Close the browser
            driver.Quit();
        }
    }
}
