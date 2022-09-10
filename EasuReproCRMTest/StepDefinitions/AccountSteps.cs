using EasyReproCRM;
using FluentAssertions;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;

namespace EasuReproCRMTest.StepDefinitions
{

    [Binding]
    public class AccountSteps : StepDefiners
    {

        [When(@"I enter the Account Name")]
        public void WhenIEnterTheAccountName()
        {
            XrmApp.ThinkTime(500);
            XrmApp.Entity.SetValue("name", TestSettings.GetRandomString(5, 10));
            XrmApp.ThinkTime(2000);
        }

        [When(@"I enter the PhoneNumber")]
        public void WhenIEnterThePhoneNumber()
        {
            XrmApp.Entity.SetValue("telephone1", "555-555-5555");
        }

        [When(@"I enter the WebSiteURL")]
        public void WhenIEnterTheWebSiteURL()
        {
            XrmApp.Entity.SetValue("websiteurl", "https://TestEasyrepro.crm.dynamics.com");
        }
      
        [Then(@"I can see the value of '([^']*)' as the header title")]
        public void ThenICanSeeTheValueOfAsTheHeaderTitle(string validationMessage)
        {
            XrmApp.Entity.GetHeaderTitle().Should().Be(validationMessage);
            XrmApp.Entity.GetHeaderTitle().Should().Contain(validationMessage);
        }
    }
}
