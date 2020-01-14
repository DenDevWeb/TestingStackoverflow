using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using TestingStackoverflow.Exception;
//using TestingStackoverflow.Exception;
using TestingStackoverflow.Helpers;
using TestingStackoverflow.Models;

namespace TestingStackoverflow.Pages
{
    public class ProfilePage : IPage
    {
        private readonly IWebDriver _driver;
        //private readonly string _url = @"https://ru.stackoverflow.com/users/signup?ssrc=head";
        private readonly string _url =
            //@"https://stackoverflow.com/users/story/12696228";
            @"https://lingualeo.com/ru/profile";
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div[1]/div[2]/span")]
        private IWebElement _editProfileButton;

        private static readonly string EDIT_PROFILE =
            "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div[1]/div[2]/span";
        private static readonly string FILE_LARGE = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[2]/div/div[3]/div[2]/div";
        
        public ProfilePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver,this);
        }
        
        public ProfilePage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            
            return this;
        }
        
        public void Submit()
        {
            _editProfileButton.Click();
            
            _driver.FindElement(
            By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/input")).SendKeys("D:\\TestingStackoverflow\\TestingStackoverflow\\Images\\test_big_avatar.jpg");
            if (ElementHelper.HasElementText(_driver, By.XPath(FILE_LARGE), TimeSpan.FromMilliseconds(500)))
            {
                throw new TextException("The file is too large");
            }
            
            //return null;
        }
        public string GetPageName()
        {
            return "Lingualeo — английский язык онлайн";
        }

        public bool AreEqual()
        {
            return _driver.Title == GetPageName();
        }
    }
}