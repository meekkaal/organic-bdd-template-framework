﻿using OpenQA.Selenium;
using Organic_BDD_Test_Template_Project.Drivers;
using Organic_BDD_Test_Template_Project.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Organic_BDD_Test_Template_Project.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private ScenarioContext _scenarioContext;
        private IWebDriver driver;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
        }

        [BeforeScenario]
        public void BeforeScenario(BrowserDriver browserDriver)
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public void AfterScenario(BrowserDriver browserDriver)
        {
            browserDriver.Dispose();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            //TODO: implement logic that has to run after each run
        }
    }
}
