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
    public class HomePage : IPage
    {
        private readonly IWebDriver _driver;
        //private readonly string _url = @"https://ru.stackoverflow.com/users/signup?ssrc=head";
        private readonly string _url =
            //@"https://stackoverflow.com/users/story/12696228";
            @"https://lingualeo.com/ru/";
        
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver,this);
        }
        
        public HomePage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            
            return this;
        }
        public string GetPageName()
        {
            return "Dashboard";
        }

        public bool AreEqual()
        {
            return _driver.Title == GetPageName();
        }
    }
}