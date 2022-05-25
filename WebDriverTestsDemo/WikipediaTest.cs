using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

var driver = new ChromeDriver();

driver.Url = "https://www.wikipedia.org/";

Console.WriteLine($"CURRENT TITLE: {driver.Title}");

var searchField = driver.FindElement(By.Id("searchInput"));
searchField.Click();
searchField.SendKeys("QA" + Keys.Enter);

Console.WriteLine($"TITLE AFTER SEARCH: {driver.Title}");

driver.Quit();




