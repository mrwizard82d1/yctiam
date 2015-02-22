using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using TimeIsMoneyBin.Specs.Pages;

namespace TimeIsMoneyBin.Specs.Utils
{
    [Binding]
    public class SeleniumExtender
    {
        public enum DurationType { Millisecond, Second, Minute }

        /// <summary>
        /// Gets or sets the web driver.
        /// </summary>
        /// <value>The web driver.</value>
        public static IWebDriver WebDriver { get; set; }

        /// <summary>
        /// The current page
        /// </summary>
        private IBasePage currentPage;

        /// <summary>
        /// Setups the feature.
        /// </summary>
        /// <returns>IWebDriver.</returns>
        [BeforeFeature()]
        public static IWebDriver SetupFeature()
        {
            WebDriver = new ChromeDriver();
            return WebDriver;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T GetPage<T>() where T: IBasePage
        {
            return (T)currentPage;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public IBasePage GetPage()
        {
            return currentPage;
        }

        /// <summary>
        /// Sets the page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetPage<T>(T page) where T : IBasePage
        {
            currentPage = page;
        }

        /// <summary>
        /// Waits the specified duration.
        /// </summary>
        /// <param name="durationType">Type of the duration.</param>
        /// <param name="duration">The duration.</param>
        public void Wait(DurationType durationType, int duration)
        {
            switch (durationType)
            {
                case DurationType.Millisecond:
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(duration));
                    break;
                case DurationType.Second:
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(duration));
                    break;
                case DurationType.Minute:
                    System.Threading.Thread.Sleep(TimeSpan.FromMinutes(duration));
                    break;
            }
        }

        /// <summary>
        /// Checks the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool CheckUrl(string url)
        {
            return WebDriver.Url == url;
        }
		
        /// <summary>
        /// Tears down feature UI.
        /// </summary>
        [AfterFeature()]
        public static void TearDownFeatureUi()
        {
            if (WebDriver != null)
            {
                WebDriver.Quit();
            }
        }
    }
}
