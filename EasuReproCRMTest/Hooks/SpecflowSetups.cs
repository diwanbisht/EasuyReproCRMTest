using System;
using System.Security;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using EasuReproCRMTest.StepDefinitions;
using EasyReproCRM;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using TechTalk.SpecFlow;

namespace EasuReproCRMTest.Hooks
{
    [Binding]
    public class SpecflowSetups
    {
        private static ExtentTest featureName;//
        private static ExtentTest scenario;

        public static ExtentTest test;
        public static ExtentReports extent = new ExtentReports();


        private readonly IObjectContainer _objectContainer;

        public static WebClient client = new WebClient(TestSettings.Options);
        //public static XrmApp? xrmApp;
        private readonly ScenarioContext _scenarioContext;


        public SpecflowSetups(IObjectContainer objectContainer)
        {
            // _scenarioContext = scenarioContext;
            this._objectContainer = objectContainer;
        }


        [AfterScenario]
        public static void Takescreenshot()
        {

        }

        [BeforeFeature]
        [Obsolete("Replaced by the automatic starter")]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }


        [BeforeTestRun]
        [Obsolete("Replaced by the automatic starter")]
        public static void RunBeforeAllTests()
        {
            string path = @"D:\TestAutomation\EasyRepro\EasuReproCRMTest\EasuReproCRMTest\Report\" + "ExtentReport - " + DateTime.Now.ToString("MM_dd_HH_mm") + ".html";
            ExtentV3HtmlReporter htmlReporter = new ExtentV3HtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);     

            LoginSteps.LoginCRMAplication();
        }

        [AfterTestRun]
        public static void RunAfterAllTests()
        {
            /*client.Browser.Driver.Close();
            client.Browser.Driver.Quit();*/
            extent.Flush();
        }

        [BeforeScenario]
        [Obsolete("Replaced by the automatic starter")]
        public static void RunBeforeScenario()
        {
            scenario = featureName.CreateNode<AventStack.ExtentReports.Gherkin.Model.Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        /// <summary>
        /// Used for after step feature of extent report.
        /// </summary>
        [AfterStep]
        [Obsolete("Replaced by the automatic starter")]
        public static void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>("And " + ScenarioStepContext.Current.StepInfo.Text);
                }
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                // var Takescreenshot = 
                //var referenceMessage = ScenarioContext.Current.TestError.Message + "<br /> <b> User: </b>" + "<br /> <b>URL: </b>" + client.Browser.Driver.Url.ToString();
                var referenceMessage = ScenarioContext.Current.TestError.Message + "<br /> <b> User: </b>" + "<br /> <b>URL: </b>";

                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text).Fail(referenceMessage);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text).Fail(referenceMessage);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text).Fail(referenceMessage);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>("And " + ScenarioStepContext.Current.StepInfo.Text).Fail(referenceMessage);
                }
            }
        }
    }
}