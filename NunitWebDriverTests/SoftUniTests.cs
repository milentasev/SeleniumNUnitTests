using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NunitWebDriverTests
{
	public class SoftUniTests
	{
		private WebDriver driver;

		[OneTimeSetUp]
		public void OpenBrowserAndNavigate()
		{
			this.driver = new ChromeDriver();
			driver.Manage().Window.Maximize();
			driver.Url = "https:/softuni.bg";
		}

		[OneTimeTearDown]
		public void ShutDown()
		{
			driver.Quit();
		}

		[Test]
		public void Test_AssertMainPageTitle()
		{
			driver.Url = "https:/softuni.bg";
			string expectedTitle = "Обучение по програмиране - Софтуерен университет";
			
			Assert.AreEqual(expectedTitle, driver.Title);
		}

		[Test]
		public void Test_AssertAboutUsTitle()
		{	
			var forUsElement = driver.FindElement(By.CssSelector("#header-nav > div.toggle-nav.toggle-holder > ul > li:nth-child(1) > a > span"));
			forUsElement.Click();

			string expectedTitle = "За нас - Софтуерен университет";
			
			Assert.AreEqual(expectedTitle, this.driver.Title);
		}

		[Test]
		public void Test_Login_InvalidUserNameAndPassword()
		{
			driver.FindElement(By.CssSelector(".softuni-btn-primary")).Click();
			driver.FindElement(By.CssSelector(".authentication-page")).Click();
			driver.FindElement(By.Id("username")).SendKeys("user1");
			driver.FindElement(By.CssSelector(".authentication-page")).Click();
			driver.FindElement(By.Id("password-input")).SendKeys("user1");
			driver.FindElement(By.CssSelector(".softuni-btn")).Click();
			Assert.That(driver.FindElement(By.CssSelector("li")).Text, Is.EqualTo("Невалидно потребителско име или парола"));
		}

		[Test]
		public void Test_Search_PositiveResult()
		{
			driver.Url = "https:/softuni.bg";
			var searchField = driver.FindElement(By.CssSelector(".header-search-dropdown-link .fa-search"));
			searchField.Click();

			var searchBox = driver.FindElement(By.CssSelector("div#search-dropdown  form[method='get']  input#search-input"));
			searchBox.Click();
			searchBox.SendKeys("QA");
			searchBox.SendKeys(Keys.Enter);

			var resultField = driver.FindElement(By.CssSelector(".search-title")).Text;
			string expectedValue = "Резултати от търсене на “QA”";
			Assert.That(resultField, Is.EqualTo(expectedValue));

		}

	}
}