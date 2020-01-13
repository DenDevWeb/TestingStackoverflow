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
    public class RegisterPage : IPage
    {
        private readonly IWebDriver _driver;
        //private readonly string _url = @"https://ru.stackoverflow.com/users/signup?ssrc=head";
        private readonly string _url =
            @"https://ru.stackoverflow.com/users/signup?ssrc=head&returnurl=https%3a%2f%2fru.stackoverflow.com%2f";
        
        [FindsBy(How = How.Id, Using = "display-name")] 
        private IWebElement _nameInput;
        
        [FindsBy(How = How.Id, Using = "email")] 
        private IWebElement _emailInput;
        
        [FindsBy(How = How.Id, Using = "password")] 
        private IWebElement _passwordInput;
        
        [FindsBy(How = How.Id, Using = "submit-button")] 
        //[FindsBy(How = How.XPath, Using = "/html/body/div[4]/div[2]/div/div/div[3]/form/div[5]/button")]
        private IWebElement _submitButton;
        
            
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div[2]/a")]
        private IWebElement _cookiePolicyButton;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div[2]/div/div/div[3]/form/div[2]/p")]
        private IWebElement _tmp;
        
        //private static readonly string EMAIL_EMPTY = "/html/body/div[3]/div[2]/div/div/div[3]/form/div[2]/p";
        private static readonly string COOKIE_POLICE = "/html/body/div[3]/div/div[2]/a";

        private static readonly string PASSWORD_EMPTY = "/html/body/div[3]/div[2]/div/div/div[3]/form/div[3]/p[2]";
        private static readonly string EMAIL_INCORRECT ="/html/body/div[3]/div[2]/div/div/div[3]/form/div[2]/p";
        // [FindsBy(How = How.XPath, Using = "/html/body/div[4]/div[2]/div/div/div[3]/form/div[2]/p/text()[1]")] 
        // private IWebElement _emailEmptyLine;
        
        public RegisterPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver,this);
        }
        
        public RegisterPage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            //_cookiePolicyButton.Click();
            
            return this;
        }
        
        public RegisterPage FillUser(User user)
        {
            _nameInput.SendKeys(user.Name);
            _emailInput.SendKeys(user.Email);
            _passwordInput.SendKeys(user.Password);
            return this;
        }

        public void Submit()
        {
            //_cookiePolicyButton.Click();
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver,TimeSpan.FromMilliseconds(50)); 
                if (wait.Until(d => _driver.FindElements(By.XPath(COOKIE_POLICE)).Count>0))
                {
                    _cookiePolicyButton.Click();
                }
            }
            catch (WebDriverTimeoutException e)
            {
            }
            _submitButton.Click();
            if (ElementHelper.HasElementText(_driver, By.XPath(EMAIL_INCORRECT), TimeSpan.FromMilliseconds(50)))
            {
                throw new TextException("Email is incorrect");
            }
            
            if (ElementHelper.HasElementText(_driver, By.XPath(PASSWORD_EMPTY), TimeSpan.FromMilliseconds(50)))
            {
                throw new TextException("Password is empty");
            }
            // WebDriverWait wait = new WebDriverWait(_driver,TimeSpan.FromMilliseconds(50)); 
            // //if (wait.Until(d => _driver.FindElements(By.XPath(EMAIL_INCORRECT)).Count>0))
            // if (wait.Until(d => _tmp.Text != ""))
            // {
            //     string tmp = _tmp.Text;
            //     string tmp2 = _driver.FindElements(By.XPath(EMAIL_INCORRECT)).FirstOrDefault().Text;
            //     throw new TextException("Email is incorrect");
            // }
            
            // if (wait.Until(d => _driver.FindElements(By.XPath(PASSWORD_EMPTY)).Count>0))
            // {
            //     throw new TextException("Password is empty");
            // }
        }
        
        public string GetPageName()
        {
            return "Регистрация";
        }

        public bool AreEqual()
        {
            return _driver.Title == GetPageName();
        }

    }
}