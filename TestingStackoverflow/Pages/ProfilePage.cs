using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        
        //[FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[1]/div[3]/span")]
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[1]/div[3]")]
        private IWebElement _saveProfileButton;
        
            
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[1]/div/div/div[3]/div/div[1]")]
        private IWebElement _header;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[3]/div/input")]
        private IWebElement _nameUserInput;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[4]/div/input")]
        private IWebElement _patronimUserInput;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[5]/div/input")]
        private IWebElement _nicknameUserInput;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[6]/div/input")]
        private IWebElement _cityUserInput;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[7]/div[1]/div[1]/div[1]/input")]
        private IWebElement _birthDayUserSelect;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[7]/div[2]/div[1]/div[1]/input")]
        private IWebElement _birthMonthUserSelect;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[7]/div[3]/div[1]/div[1]/input")]
        private IWebElement _birthYearUserSelect;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[8]/div")]
        private IWebElement _genderUserRadio;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[8]/div/div[1]/label/span[3]")]
        private IWebElement _maleRadio;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[8]/div/div[2]/label/span[3]")]
        private IWebElement _femaleRadio;
        
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[7]/div[1]/div[1]/div[2]/div[2]")]
        private IWebElement _birthDayUserSelectArrow;

        private static readonly string UPDATE_AVATAR =
            "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/input";
        
        private static readonly string EDIT_PROFILE =
            "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div[1]/div[2]/span";
        
        private static readonly string FILE_LARGE = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[2]/div/div[3]/div[2]/div";
        private static readonly string BIRTH_DAY = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[7]/div[1]/div[1]/div[2]/div[2]";

        private static readonly string MALE_RADIO =
            "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[8]/div/div[1]/label/input";

        private static readonly string FEMALE_RADIO = "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[8]/div/div[2]/label/input";
        //private static readonly string EDIT_PROFILE = "";
        
        //private static readonly string BIRTH_DAY = 
            
        
        
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

        public ProfilePage GoToEditProfile()
        {
            if (ElementHelper.HasElement(_driver, By.XPath(EDIT_PROFILE), TimeSpan.FromMilliseconds(1000)))
            {
                _editProfileButton.Click();
            }
            return this;
        }
        
        public ProfilePage GoToSaveProfile()
        {
            //((IJavaScriptExecutor) _driver).ExecuteScript("arguments[0].scrollIntoView(true);", _header);
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight);");
            _saveProfileButton.Click();
            return this;
        }

        public void LoadAvatar(UserProfile user)
        {
            if (ElementHelper.HasElement(_driver, By.XPath(UPDATE_AVATAR), TimeSpan.FromMilliseconds(500)))
            {
                _driver.FindElement(
                        By.XPath(UPDATE_AVATAR))
                    .SendKeys(user.PathAvatar);
            }
            
            if (ElementHelper.HasElementText(_driver, By.XPath(FILE_LARGE), TimeSpan.FromMilliseconds(500)))
            {
                throw new TextException("The file is too large");
            }
        }
        
        public ProfilePage FillUser(UserProfile user)
        {
            //_editProfileButton.Click();

            if (ElementHelper.HasElement(_driver, By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[3]/div/input"), TimeSpan.FromMilliseconds(50)))
            {
                _nameUserInput.Clear();
                _nameUserInput.SendKeys(user.Name);
            }
            _patronimUserInput.Clear();
            _patronimUserInput.SendKeys(user.Patronim);
            _nicknameUserInput.Clear();
            _nicknameUserInput.SendKeys(user.Nickname);
            _cityUserInput.Clear();
            _cityUserInput.SendKeys(user.City);
            
            // _birthDayUserSelect.SendKeys(user.BirthDate.Day.ToString());
            // _birthMonthUserSelect.SendKeys(user.BirthDate.Month.ToString());
            // _birthYearUserSelect.SendKeys(user.BirthDate.Year.ToString());
            ((IJavaScriptExecutor) _driver).ExecuteScript("arguments[0].scrollIntoView(true);", _birthDayUserSelectArrow);
            //_driver.FindElement(By.XPath("/html/body/div[1]/div/div[4]/div/div[4]/div[3]")).Click();
            
            //((Locatable)webElement).getLocationOnScreenOnceScrolledIntoView();
            _birthDayUserSelectArrow.Click();
            IWebElement birthDay = _driver.FindElements(By.ClassName("ll-leokit__select-option")).Where(x => x.Text == user.BirthDate.Day.ToString()).FirstOrDefault();
            birthDay.Click();
            
            _birthMonthUserSelect.Click();
            IWebElement birthMonth = _driver.FindElements(By.ClassName("ll-leokit__select-option")).Where(x => x.Text == user.BirthDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru-RU"))).FirstOrDefault();
            birthMonth.Click();
            
            _birthYearUserSelect.Click();
            IWebElement birthYear = _driver.FindElements(By.ClassName("ll-leokit__select-option")).Where(x => x.Text == user.BirthDate.Year.ToString()).FirstOrDefault();
            birthYear.Click();

            switch (user.GetGender())
            {
                case "male":
                    _maleRadio.Click();
                    break;
                case "female":
                    _femaleRadio.Click();
                    break;
            }    
                
                
            
            
            //new WebDriverWait(_driver, TimeSpan.FromMilliseconds(50)).Until(ExpectedConditions.ElementToBeClickable(By.Id("male"))).Click();
            //_driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[8]/div/div[2]/label/input")).SendKeys("");

            return this;
        }

        public bool CheckUserProfile(UserProfile user)
        {
            bool check_gender = false;
            //     = _driver
            //     .FindElement(By.XPath(
            //         "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[8]/div/div[1]/label/input"))
            //     .Selected;
            // bool tmp2 = _driver
            //     .FindElement(By.XPath(
            //         "/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[8]/div/div[2]/label/input"))
            //     .Selected;
            if (ElementHelper.HasElement(_driver, By.XPath(UPDATE_AVATAR), TimeSpan.FromMilliseconds(500)))
            {
                switch (user.GetGender())
                {
                    case "male":
                        check_gender = _driver
                            .FindElement(By.XPath(
                                MALE_RADIO))
                            .Selected;
                        break;
                    case "female":
                        check_gender = _driver
                            .FindElement(By.XPath(
                                FEMALE_RADIO))
                            .Selected;
                        break;
                }

                //string str = _nameUserInput.GetAttribute("value");
                if (_nameUserInput.GetAttribute("value") == user.Name &&
                    _patronimUserInput.GetAttribute("value") == user.Patronim &&
                    _nicknameUserInput.GetAttribute("value") == user.Nickname &&
                    _cityUserInput.GetAttribute("value") == user.City &&
                    _birthDayUserSelect.GetAttribute("value") == user.BirthDate.Day.ToString() &&
                    _birthMonthUserSelect.GetAttribute("value") ==
                    user.BirthDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru-RU")) &&
                    _birthYearUserSelect.GetAttribute("value") == user.BirthDate.Year.ToString() &&
                    check_gender
                )
                {
                    return true;
                }
            }

            return false;

    }
        
        public ProfilePage Submit(UserProfile user)
        {
            //_editProfileButton.Click();

            // if (ElementHelper.HasElement(_driver, By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/input"), TimeSpan.FromMilliseconds(500)))
            // {
            //     _driver.FindElement(
            //         By.XPath("/html/body/div[1]/div/div[2]/div[2]/div/div/div/div/div[2]/div[2]/div/div[2]/input"))
            //         .SendKeys("D:\\TestingStackoverflow\\TestingStackoverflow\\Images\\test_big_avatar.jpg");
            // }
            
            if (ElementHelper.HasElementText(_driver, By.XPath(FILE_LARGE), TimeSpan.FromMilliseconds(500)))
            {
                throw new TextException("The file is too large");
            }

            if (!CheckUserProfile(user))
            {
                throw new TextException("Data profile is incorrect");
            }

            return this;

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