using System;
using System.Security;
using BoDi;
using EasyReproCRM;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace EasuReproCRMTest.StepDefinitions
{

    [Binding]
    public class LoginSteps : StepDefiners
    {
        public static void LoginCRMAplication()
        {
            SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
            SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
            SecureString _mfaSecretKey = System.Configuration.ConfigurationManager.AppSettings["MfaSecretKey"].ToSecureString();
            Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
            string appName = System.Configuration.ConfigurationManager.AppSettings["AppName"].ToString();

            XrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);
            if (!string.IsNullOrEmpty(appName))
            {
                XrmApp.Navigation.OpenApp(appName);
                XrmApp.ThinkTime(5000);
            }
        }

        public static void GoBack()
        {
            try
            {

                if (Client.Browser.Driver.HasElement(By.XPath("//button[@id='navigateBackButtontab-id-0']")))
                {
                    XrmApp.Navigation.GoBack();
                }

                var inlineDialog = Client.Browser.Driver.HasElement(By.XPath("//*[contains(@id, 'modalDialogView')]"));
                if (inlineDialog)
                {
                    Client.Browser.Driver.FindElement(By.Id("cancelButton")).Click();
                }
            }
            catch (Exception)
            {

            }
        }

        [Given(@"I am logged in to the app")]
        public void GivenIAmLoggedInToTheAppAs()
        {
            LoginCRMAplication();
        }
    }
}