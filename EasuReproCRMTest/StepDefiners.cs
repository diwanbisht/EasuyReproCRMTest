using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using OpenQA.Selenium;

namespace EasyReproCRM
{

    /// <summary>
    /// Base class for defining step bindings.
    /// </summary>
    public abstract class StepDefiners
    {
        private static WebClient? client;
        private static XrmApp? xrmApp;
        //

        /// <summary>
        /// Gets the EasyRepro WebClient.
        /// </summary>
        public static WebClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new WebClient(TestSettings.Options);
                }
                return client;
            }
        }

        /// <summary>
        /// Gets the EasyRepro XrmApp.
        /// </summary>
        public static XrmApp XrmApp => xrmApp ?? (xrmApp = new XrmApp(Client));

        /// <summary>
        /// Gets the Selenium WebDriver.
        /// </summary>
        protected static IWebDriver Driver => Client.Browser.Driver;


        protected static void Quit()
        {
            var driver = client?.Browser?.Driver;

            xrmApp?.Dispose();

            // Ensuring that the driver gets disposed. Previously we were left with orphan processes and were unable to clean up profile folders.
            driver?.Dispose();

            xrmApp = null;
            client = null;
        }


    }
}