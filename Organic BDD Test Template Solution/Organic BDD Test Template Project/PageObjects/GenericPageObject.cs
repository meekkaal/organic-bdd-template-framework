﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organic_BDD_Test_Template_Project.PageObjects
{
    class GenericPageObject
    {
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        public GenericPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Finding elements by ID
        private IWebElement SecondNumberElement => _webDriver.FindElement(By.Id("second-number"));
        private IWebElement AddButtonElement => _webDriver.FindElement(By.Id("add-button"));
        private IWebElement ResultElement => _webDriver.FindElement(By.Id("result"));
        private IWebElement ResetButtonElement => _webDriver.FindElement(By.Id("reset-button"));

        public string WaitForTitle()
        {
            return WaitUntil(
                () => _webDriver.Title,
                result => !string.IsNullOrEmpty(result));
        }

        public void EnterSecondNumber(string number)
        {
            //Clear text box
            SecondNumberElement.Clear();
            //Enter text
            SecondNumberElement.SendKeys(number);
        }

        public void ClickAdd()
        {
            //Click the add button
            AddButtonElement.Click();
        }

        public void EnsureCalculatorIsOpenAndReset()
        {
            //Open the calculator page in the browser if not opened yet
            if (_webDriver.Url != "")
            {
                _webDriver.Url = "";
            }
            //Otherwise reset the calculator by clicking the reset button
            else
            {
                //Click the rest button
                ResetButtonElement.Click();

                //Wait until the result is empty again
                WaitForEmptyResult();
            }
        }

        public string WaitForNonEmptyResult()
        {
            //Wait for the result to be not empty
            return WaitUntil(
                () => ResultElement.GetAttribute("value"),
                result => !string.IsNullOrEmpty(result));
        }

        public string WaitForEmptyResult()
        {
            //Wait for the result to be empty
            return WaitUntil(
                () => ResultElement.GetAttribute("value"),
                result => result == string.Empty);
        }

        /// <summary>
        /// Helper method to wait until the expected result is available on the UI
        /// </summary>
        /// <typeparam name="T">The type of result to retrieve</typeparam>
        /// <param name="getResult">The function to poll the result from the UI</param>
        /// <param name="isResultAccepted">The function to decide if the polled result is accepted</param>
        /// <returns>An accepted result returned from the UI. If the UI does not return an accepted result within the timeout an exception is thrown.</returns>
        private T WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            return wait.Until(driver =>
            {
                var result = getResult();
                if (!isResultAccepted(result))
                    return default;

                return result;
            });

        }
    }
}
