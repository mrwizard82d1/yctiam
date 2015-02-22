using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Interactions;
using System.Drawing.Imaging;
using TimeIsMoneyBin.Specs.Utils;

namespace TimeIsMoneyBin.Specs.Pages
{
    public partial class BasePage
    {
        /// <summary>
        /// Enum DurationType
        /// </summary>
        public enum DurationType { Millisecond, Second, Minute }

        /// <summary>
        /// The web driver
        /// </summary>
        private IWebDriver webDriver;

        /// <summary>
        /// Gets or sets the web driver.
        /// </summary>
        /// <value>The web driver.</value>
        public IWebDriver WebDriver
        {
            get { return webDriver; }
            set { webDriver = value; PageFactory.InitElements(value, this); }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public virtual string CurrentUrl { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        public BasePage()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="WebDriver">The web driver.</param>
        public BasePage(OpenQA.Selenium.IWebDriver WebDriver)
        {
            this.WebDriver = WebDriver;
        }
        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>IWebElement.</returns>
        public IWebElement GetElement(string elementName)
        {
            IWebElement element = FindElement(elementName);
            if (element == null)
            {
                throw new SpecFlowSeleniumException("No element named \"" + elementName + "\" have been found in html page. You should check the accessor.");
            }

            return element;
        }

        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>IWebElement.</returns>
        /// <exception cref="System.NotImplementedException">Not Implemented</exception>
        protected virtual IWebElement FindElement(string elementName)
        {
            throw new NotImplementedException("Not Implemented");
        }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>List of IWebElement.</returns>
        public List<IWebElement> GetElements(string elementName)
        {
            List<IWebElement> element = FindElements(elementName);
            if (element == null)
            {
                throw new SpecFlowSeleniumException("No element named \"" + elementName + "\" have been found in html page. You should check the accessor.");
            }

            return element;
        }

        /// <summary>
        /// Finds the elements.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>IWebElement.</returns>
        /// <exception cref="System.NotImplementedException">Not Implemented</exception>
        protected virtual List<IWebElement> FindElements(string elementName)
        {
            throw new NotImplementedException("Not Implemented");
        }

        /// <summary>
        /// Waits the specified duration.
        /// </summary>
        /// <param name="durationType">Type of the duration.</param>
        /// <param name="duration">The duration.</param>
        internal void Wait(DurationType durationType, int duration)
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
        /// Changes page.
        /// </summary>
        /// <typeparam name="T">Type of the new Page</typeparam>
        /// <param name="driver">The driver.</param>
        /// <param name="page">The page.</param>
        /// <returns>The new page.</returns>
        public T ChangePage<T>(string page) where T : IBasePage
        {
            string urlBeforeClick = WebDriver.Url;
            WebDriver.Navigate().GoToUrl(page);
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(3d));
            wait.Until((d) => d.Url != urlBeforeClick);
            IBasePage newPage = (IBasePage)Activator.CreateInstance<T>();
            newPage.WebDriver = WebDriver;
            CurrentUrl = page;
            return (T)newPage;
        }

        /// <summary>
        /// Changes the aliased page.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The new page.</returns>
        public IBasePage ChangeAliasedPage(string alias)
        {
            string urlBeforeClick = WebDriver.Url;
            IBasePage targetPage = Url.Urls[alias];
            targetPage.WebDriver = WebDriver;
            string url = Sites.GetSiteUrl() + targetPage.GetUrl(alias);
            WebDriver.Navigate().GoToUrl(url);
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(3d));
            wait.Until((d) => d.Url != urlBeforeClick);
            CurrentUrl = url;
            return targetPage;
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="urlAlias">The URL alias.</param>
        /// <returns>No page.</returns>
        public virtual string GetUrl(string urlAlias)
        {
            return string.Empty;
        }

        /// <summary>
        /// Hovers the specified element name.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        internal void Hover(string elementName)
        {
            Actions action = new Actions(WebDriver);
            action.MoveToElement(GetElement(elementName)).Build().Perform();
            Wait(DurationType.Second, 2);
        }

        /// <summary>
        /// Clicks the specified element name.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        internal void Click(string elementName)
        {
            GetElement(elementName).Click();
        }

        /// <summary>
        /// Scrolls to WebElement.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        internal void ScrollTo(string elementName)
        {
            IWebElement element = GetElement(elementName);
            (WebDriver as IJavaScriptExecutor).ExecuteScript(string.Format("window.scrollTo(0, {0});", element.Location.Y));
        }

        /// <summary>
        /// Scrolls to WebElement.
        /// </summary>
        /// <param name="element">The element.</param>
        internal void ScrollTo(IWebElement element)
        {
            (WebDriver as IJavaScriptExecutor).ExecuteScript(string.Format("window.scrollTo(0, {0});", element.Location.Y));
        }

        /// <summary>
        /// Selects all (Ctrl+a).
        /// </summary>
        internal void SelectAll()
        {
            Actions copy = new Actions(WebDriver);
            copy.KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control);
            copy.Build().Perform();
        }

        /// <summary>
        /// Copies selected value (Ctrl+c)
        /// </summary>
        internal void Copy()
        {
            Actions copy = new Actions(WebDriver);
            copy.KeyDown(Keys.Control).SendKeys("c").KeyUp(Keys.Control);
            copy.Build().Perform();
        }

        /// <summary>
        /// Pastes clipboard (Ctrl+v)
        /// </summary>
        internal void Paste()
        {
            Actions copy = new Actions(WebDriver);
            copy.KeyDown(Keys.Control).SendKeys("v").KeyUp(Keys.Control);
            copy.Build().Perform();
        }

        /// <summary>
        /// Fills the form.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="valueName">Name of the value.</param>
        internal void FillForm(TechTalk.SpecFlow.Table table, string fieldName, string valueName)
        {
            if (table.Rows == null || (table.Rows != null && table.Rows.Count == 0))
                throw new SpecFlowSeleniumException("Table row shouldn't be null or empty");
            foreach (var row in table.Rows)
            {
                GetElement(row[fieldName]).SendKeys(row[valueName]);
            }
        }

        /// <summary>
        /// Evals the script.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script">The script.</param>
        /// <returns>``0.</returns>
        internal T EvalScript<T>(string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
            return (T)js.ExecuteScript(script);
        }

        /// <summary>
        /// Runs the script.
        /// </summary>
        /// <param name="script">The script.</param>
        internal void RunScript(string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
            js.ExecuteScript(script);
        }


    }
}
