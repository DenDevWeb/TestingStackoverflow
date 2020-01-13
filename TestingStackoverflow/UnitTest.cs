using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using TestingStackoverflow.Exception;
using TestingStackoverflow.Helpers;
using TestingStackoverflow.Models;
using TestingStackoverflow.Pages;

namespace TestingStackoverflow
{
    public class UnitTest
    {
        private IWebDriver _driver;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _driver = new ChromeDriver("D:\\TestingStackoverflow\\TestingStackoverflow\\bin\\Debug\\netcoreapp3.0\\");
            //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //_driver.Quit();
        }

        [Test]
        public void FailedRegistration()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            User user = User.GetRandomUser();
            user.Email = "test";
            //user.Password = "qwerty123456";
            try
            {
                registerPage.Navigate().FillUser(user).Submit();
            }
            catch (TextException e)
            {
                Assert.AreEqual("Email is incorrect",e.Message);
            }
            
             user.Email = TextHelpers.GetRandomWord(10) + "@" + TextHelpers.GetRandomWord(6) + "." + TextHelpers.GetRandomWordWithoutNumbers(2);
             user.Password = "";
             try
             {
                 registerPage.Navigate().FillUser(user).Submit();
             }
             catch (TextException e)
             {
                 Assert.AreEqual("Password is empty",e.Message);
             }
             
        }

        [Test]
        public void SuccessRegistration()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            User user = User.GetRandomUser();
            string result = registerPage.Navigate().FillUser(user).Submit();
            Assert.AreEqual(result, "Registration was successful");
            
        }
    }
}