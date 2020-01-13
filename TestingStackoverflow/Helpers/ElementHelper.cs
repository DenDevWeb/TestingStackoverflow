using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestingStackoverflow.Helpers
{
    public class ElementHelper
    {
        public static bool HasElement(IWebDriver driver, By selector, TimeSpan time)
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            try
            {
                if (wait.Until(d => driver.FindElements(selector).Count>0))
                {
                    return true;
                }
            }
            catch (WebDriverTimeoutException e)
            {
                return false;
            }
            
            return false;
        }
        
        public static bool HasElementText(IWebDriver driver, By selector, TimeSpan time)
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            try
            {
                if (wait.Until(d => driver.FindElements(selector).FirstOrDefault().Text != ""))
                {
                    return true;
                }
            }
            catch (WebDriverTimeoutException e)
            {
                return false;
            }
            

            return false;
        }
    }
}