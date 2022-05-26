using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace URLShortnerTests
{
	public class URLShortenerTests

	{
		private WebDriver driver;
		
		[SetUp]
		public void OpenAndNavigate()
		{
			this.driver = new ChromeDriver();
			driver.Url = "https://shorturl.nakov.repl.co/";
			driver.Manage().Window.Maximize();
		}

		[TearDown]
		public void ShutDown()
		{
			driver.Quit();
		}

		[Test]
		public void Test_Home_Page()
		{
			Assert.That(driver.Title, Is.EqualTo("URL Shortener"));
		}

		[Test]
		public void Test_ShortUrl_Page()
		{
			driver.FindElement(By.CssSelector("header > a:nth-of-type(2)")).Click();

			var table = driver.FindElements(By.CssSelector("table tr > td"));

			Assert.That(driver.Title,Is.EqualTo ("Short URLs"));
			Assert.That(table[0].Text, Is.EqualTo("https://nakov.com"));
			Assert.That(table[1].Text, Is.EqualTo("http://shorturl.nakov.repl.co/go/nak"));
		}

		[Test]
		public void Test_Visit_URL()
		{
			driver.FindElement(By.CssSelector("header > a:nth-of-type(2)")).Click();
			
			var shortLink = driver.FindElement(By.CssSelector("tr:nth-of-type(1) > td:nth-of-type(2) > .shorturl"));
			
			var visitsBeforeClick = driver.FindElement(By.CssSelector("tr:nth-of-type(1) > td:nth-of-type(4)")).Text;
			var visitsBeforeClicktoInt = int.Parse(visitsBeforeClick);

			shortLink.Click();

			var shortLinkWindowHandle = driver.SwitchTo().Window(driver.WindowHandles[1]);

			string expectedTitle = "Svetlin Nakov - Svetlin Nakov – Official Web Site and Blog";

			Assert.That(shortLinkWindowHandle.Title, Is.EqualTo(expectedTitle));

			driver.SwitchTo().Window(driver.WindowHandles[0]);

			var visitsAfterClick = driver.FindElement(By.CssSelector("tr:nth-of-type(1) > td:nth-of-type(4)")).Text;
			var visitsAfterClickToInt = int.Parse(visitsAfterClick);

			Assert.That(visitsBeforeClicktoInt < visitsAfterClickToInt);
		}

		[Test]
		public void Test_Add_Url_Invalid_Data()
		{
			driver.FindElement(By.CssSelector("header > a:nth-of-type(3)")).Click();

			var UrlField = driver.FindElement(By.Id("url"));
			var createButton = driver.FindElement(By.CssSelector("td > button[type='submit']"));
			
			UrlField.SendKeys("hello");
			createButton.Click();

			var invalidUrlField = driver.FindElement(By.CssSelector("body > .err"));

			var expectedError = "Invalid URL!";

			Assert.That(invalidUrlField.Text, Is.EqualTo(expectedError));
		}

		[Test]
		public void Test_Add_Url_Valid_Data()
		{
			driver.FindElement(By.CssSelector("header > a:nth-of-type(3)")).Click();

			var UrlField = driver.FindElement(By.Id("url"));
			var shortCode = driver.FindElement(By.Id("code"));
			var createButton = driver.FindElement(By.CssSelector("td > button[type='submit']"));
			var uniqueNum = DateTime.Now.Ticks.ToString();

			UrlField.SendKeys("https://softuni.bg/");
			shortCode.Clear();
			shortCode.SendKeys(uniqueNum);
			createButton.Click();

			var table = driver.FindElements(By.CssSelector("table tr > td"));

			foreach (var item in table)
			{
				if (item.Text.Contains(uniqueNum)) 
				{
					Assert.That(item.Text.Contains(uniqueNum));
					break;
				}
			}
		}
	}
}