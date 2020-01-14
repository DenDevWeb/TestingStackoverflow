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
        [Test]
        public void FailedLogin()
        {
            LoginPage loginPage = new LoginPage(_driver);
            //неправильный mail
            User user = User.GetRandomUser();
            user.Email = "test";
            loginPage.Navigate().FillUser(user).Submit();
            Assert.True(loginPage.AreEqual());
            
            //user.Email = "test@test.ru";
            user = User.GetValidUserForLogin();
            user.Password = "";
            HomePage homePage = null; //= loginPage.Navigate().FillUser(user).Submit();
            try
            {
                homePage = loginPage.Navigate().FillUser(user).Submit();
            }
            catch (TextException e)
            {
                Assert.AreEqual("Password or password is empty",e.Message);
            }
            //Assert.True(homePage.AreEqual());
            //string tmp = _driver.Title;
            //Assert.AreEqual(result, "Registration was successful");

        }
        
        [Test]
                 public void SuccessLogin()
                 {
                     LoginPage loginPage = new LoginPage(_driver);
         
                     //user.Email = "test@test.ru";
                     User user = User.GetValidUserForLogin();
                     HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
                     
                     Assert.True(homePage.AreEqual());
                     
                     //string tmp = _driver.Title;
                     //Assert.AreEqual(result, "Registration was successful");
         
                 }

                 [Test]
                 public void FailedAvatar()
                 {
                     LoginPage loginPage = new LoginPage(_driver);
         
                     //user.Email = "test@test.ru";
                     User user = User.GetValidUserForLogin();
                     try
                     {
                         loginPage.Navigate().FillUser(user).Submit().ToProfile().Navigate().Submit();
                     }
                     catch (TextException e)
                     {
                         Assert.AreEqual("The file is too large",e.Message);
                     }
                     
                 }
        
    }
}