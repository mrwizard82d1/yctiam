using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace TimeIsMoneyBin.Specs.Pages
{
    public interface IBasePage
    {
        /// <summary>
        /// Gets or sets the web driver.
        /// </summary>
        /// <value>The web driver.</value>
        IWebDriver WebDriver { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        string CurrentUrl { get; set; }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>IWebElement.</returns>
        IWebElement GetElement(string elementName);

        /// <summary>
        /// Changes the aliased page.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>IBasePage.</returns>
        IBasePage ChangeAliasedPage(string alias);
		
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="urlAlias">The URL alias.</param>
        /// <returns>System.String.</returns>
        string GetUrl(string urlAlias);
    }
}
