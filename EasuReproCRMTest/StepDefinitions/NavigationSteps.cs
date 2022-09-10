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
    public class NavigationSteps : StepDefiners
    {

        [When("I open the sub area '(.*)' under the '(.*)' area")]
        public static void WhenIOpenTheSubAreaUnderTheArea(string subAreaName, string areaName)
        {
            XrmApp.Navigation.OpenSubArea(areaName, subAreaName);
        }

        [When("I open global search")]
        public static void WhenIOpenGlobalSearch()
        {
            XrmApp.Navigation.OpenGlobalSearch();
        }

        /// <summary>
        /// Opens an area.
        /// </summary>
        /// <param name="areaName">The name of the area.</param>
        [When("I open the '(.*)' area")]
        public static void WhenIOpenTheArea(string areaName)
        {
            XrmApp.Navigation.OpenArea(areaName);
        }

        [When("I open the '(.*)' sub area of the '(.*)' group")]
        public static void WhenIOpenTheSubAreaOfTheGroup(string subAreaName, string groupName)
        {
            XrmApp.Navigation.OpenGroupSubArea(groupName, subAreaName);
        }


        [When("I open a quick create for the '(.*)' entity")]
        public static void WhenIOpenAQuickCreateForTheEntity(string entity)
        {
            XrmApp.Navigation.QuickCreate(entity);
        }

        /// <summary>
        /// Signs out.
        /// </summary>
        [When("I sign out")]
        public static void WhenISignOut()
        {
            XrmApp.Navigation.SignOut();
        }

        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <param name="area">The area name.</param>
        [Then(@"I see the '(.*)' area")]
        public static void ThenICanSeeTheArea(string area)
        {
            Driver
                .WaitUntilAvailable(By.Id("areaSwitcherContainer"))
                .Text
                .Should().Contain(area);
        }

        [Then(@"I see the '(.*)' subarea")]
        public static void ThenICanSeeTheSubArea(string subArea)
        {
            Driver
                .WaitUntilAvailable(By.XPath($"//img[@title='{subArea}']"))
                .Text
                .Should().NotBeNull();
        }

        /// <summary>
        /// Gets the sub-area.
        /// </summary>
        /// <param name="groupName">The group.</param>
        [Then(@"I see the '(.*)' group")]
        public static void ThenICanSeeTheGroup(string groupName)
        {
            var groupNameWithoutWhitespace = groupName?.Replace(" ", string.Empty);

            Driver
                .WaitUntilAvailable(By.XPath($"//span[@data-id='sitemap-sitemapAreaGroup-{groupNameWithoutWhitespace}']"))
                .Text
                .Should().Contain(groupName);
        }

        [Then(@"I click Go Back Navigation button")]
        public void ThenIClickGoBackNavigationButton()
        {
            XrmApp.Navigation.GoBack();
        }

        [Given(@"I navigated to '(.*)' Module")]
        public void GivenINavigatedToModule(string moduleName)
        {
            NavigatesToModule(moduleName);
        }

        [When(@"I Open '(.*)' App")]
        public void WhenIOpenApp(string appName)
        {
            XrmApp.Navigation.OpenApp(appName);
            XrmApp.ThinkTime(5000);
        }

        [When(@"I click button '(.*)'")]
        public void WhenIClickButton(string buttonname)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.FindVisible(By.XPath($"//button//span[text()='{buttonname}']")).Click();
            Driver.WaitForTransaction();////
        }

     
        [Given(@"I Navigated to '([^']*)' Home screen")]
        public void GivenINavigatedToHomeScreen(string leftMenuName)
        {
            Driver.FindElement(By.XPath($"//span[text()='{leftMenuName}']")).Click();
            Driver.WaitForTransaction();
        }


        [When(@"I click left  navigation Menu '(.*)'")]
        public void WhenIClickLeftNavigationMenu(string leftMenuName)
        {
            Driver.FindElement(By.XPath($"//span[text()='{leftMenuName}']")).Click();
            Driver.WaitForTransaction();
        }

        public static IWebElement NewPlusButton => Driver.FindElement(By.XPath("//button[@data-id='create-new-tab-button']"));
        public IList<IWebElement> GridColumnNames => Driver.FindElements(By.XPath("//div[@class='wj-cell wj-header']"));

        public static void NavigatesToModule(string ModuleName)
        {
            NewPlusButton.Click();
            XrmApp.ThinkTime(1000);
            NavigateModuleTypes(ModuleName);
        }
        public static void NavigateModuleTypes(string ModuleTypeName)
        {
            IWebElement ModuleName = Driver.FindElement(By.XPath("//li[@title= '" + ModuleTypeName + "']"));
            ModuleName.Click();
        }

        [When(@"I click On the '(.*)' button on the Confirm dialog")]
        public void WhenIClickOnTheButtonOnTheConfirmDialog(string option)
        {
            Driver.FindElement(By.XPath("//button/span[text()=  '" + option + "']")).Click();
            Driver.WaitForTransaction();
        }

        [When(@"Fill the Case form")]
        public void WhenFillTheCaseForm()
        {

            XrmApp.Entity.SetValue("title", TestSettings.GetRandomString(5, 10));

            // XrmApp.Entity.SetValue("subject", TestSettings.GetRandomString(5, 10));

            XrmApp.Entity.SetValue(new LookupItem { Name = "subjectid.fieldControl_container", Value = "General", Index = 3 });

            // To work around the bug below, clearvalue first to initalize the field and then set it
            // XrmApp.Entity.ClearValue(new LookupItem { Name = "customerid" });


            // Bug - System.InvalidOperationException: No Results Matching maria campbell Were Found. #769
            // XrmApp.Entity.SetValue(new LookupItem { Name = "customerid", Value = "maria campbell", Index = 0 });


            XrmApp.QuickCreate.Save();
        }

    }
}



