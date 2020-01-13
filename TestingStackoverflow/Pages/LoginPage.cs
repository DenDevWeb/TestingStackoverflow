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
    public class LoginPage : IPage
    {
        private readonly IWebDriver _driver;
        //private readonly string _url = @"https://ru.stackoverflow.com/users/signup?ssrc=head";
        private readonly string _url =
            //@"https://stackoverflow.com/users/story/12696228";
            @"https://lingualeo.com/ru/";

        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[1]/div/div/div[1]/div[3]/div[2]/div[2]/div[2]/div[1]/input")] 
        private IWebElement _emailInput;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[1]/div/div/div[1]/div[3]/div[2]/div[2]/div[2]/div[2]/input")] 
        private IWebElement _passwordInput;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[1]/div/div/div[1]/div[3]/div[2]/div[2]/div[2]/div[4]/button")] 
        //[FindsBy(How = How.XPath, Using = "/html/body/div[4]/div[2]/div/div/div[3]/form/div[5]/button")]
        private IWebElement _submitButton;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[1]/div/div/div[1]/div[3]/div[2]/div[2]/div[2]/div[6]")] 
        //[FindsBy(How = How.XPath, Using = "/html/body/div[4]/div[2]/div/div/div[3]/form/div[5]/button")]
        private IWebElement _have_an_account;
        
        //private static readonly string EMAIL_EMPTY = "/html/body/div[3]/div[2]/div/div/div[3]/form/div[2]/p";
        //private static readonly string COOKIE_POLICE = "/html/body/div[3]/div/div[2]/a";

        private static readonly string PASSWORD_EMPTY = "/html/body/div[4]/div[2]/div/div[3]/form/div[2]/p";
        
        private static readonly string EMAIL_OR_PASSWORD_INCORRECT ="/html/body/div[1]/div/div[1]/div/div/div[1]/div[3]/div[2]/div[2]/div[2]/div[1]";
        
        private static readonly string HAVE_AN_ACCOUNT ="/html/body/div[1]/div/div[1]/div/div/div[1]/div[3]/div[2]/div[2]/div[2]/div[6]";

        private static readonly string PLAN_LEARN = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div/div/h1";
        // [FindsBy(How = How.XPath, Using = "/html/body/div[4]/div[2]/div/div/div[3]/form/div[2]/p/text()[1]")] 
        // private IWebElement _emailEmptyLine;
        
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver,this);
        }
        
        public LoginPage Navigate()
                 {
                     _driver.Navigate().GoToUrl(_url);
                     //HAVE_AN_ACCOUNT
                     try
                     {
                         WebDriverWait wait = new WebDriverWait(_driver,TimeSpan.FromMilliseconds(50)); 
                         if (wait.Until(d => _driver.FindElements(By.XPath(HAVE_AN_ACCOUNT)).Count>0))
                         {
                             _have_an_account.Click();
                         }
                     }
                     catch (WebDriverTimeoutException e)
                     {
                     }
                     // string trt = _driver.Title;
                     // IWebElement temp =_driver.FindElement(
                     //     By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/div/span"));//.SendKeys("D:\\Загрузки\\Temp.jpg");
                     // string t = temp.Text;
                     // var tt = temp.GetAttribute("value");
                     // _driver.FindElement(
                     //     By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/input")).SendKeys("D:\\Загрузки\\Dishonored_2_using_VoidEngine_v1_77_5_0_16_12_2018_0_58_13.png");
                     //temp.Click();
                     //_recaptchaAnchor.FindElement(By.Id("recaptcha-anchor")).Click();
                     //IWebElement checkbox = _driver.FindElement(By.CssSelector("div[style=background-image: url(\"https://contentcdn.lingualeo.com/uploads/avatar/noavatar.png\");]"));
                     // string checkboxXPath = "/html/body/div[2]/div[3]/div[1]/div/div/span/div[5]";
                     // IWebElement elementToClick = _driver.FindElement(By.XPath(checkboxXPath));
                     // elementToClick.Click();
                     // elementToClick.
                     
                     return this;
                 }
        
        public LoginPage FillUser(User user)
        {
            _emailInput.SendKeys(user.Email);
            _passwordInput.SendKeys(user.Password);
            return this;
        }

        public HomePage Submit()
        {
            //_cookiePolicyButton.Click();
            // try
            // {
            //     WebDriverWait wait = new WebDriverWait(_driver,TimeSpan.FromMilliseconds(50)); 
            //     if (wait.Until(d => _driver.FindElements(By.XPath(COOKIE_POLICE)).Count>0))
            //     {
            //         _cookiePolicyButton.Click();
            //     }
            // }
            // catch (WebDriverTimeoutException e)
            // {
            // }
            _submitButton.Click();
            
            if (ElementHelper.HasElementText(_driver, By.XPath(EMAIL_OR_PASSWORD_INCORRECT), TimeSpan.FromMilliseconds(50)))
            {
                throw new TextException("Password or password is empty");
            }
            
            if (ElementHelper.HasElement(_driver, By.XPath(PLAN_LEARN), TimeSpan.FromMilliseconds(50)))
            {
                return new HomePage(_driver);
            }
            
            //
            // if (ElementHelper.HasElementText(_driver, By.XPath(PASSWORD_EMPTY), TimeSpan.FromMilliseconds(50)))
            // {
            //     throw new TextException("Password is empty");
            // }
            //
            // if (ElementHelper.HasElementText(_driver, By.XPath(CONFIRMATION_MAIL), TimeSpan.FromMilliseconds(50)))
            // {
            //     return "Registration was successful";
            // }
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
            //return "Registration was unsuccessful";
            return null;
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