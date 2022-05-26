using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DataDrivenTestingCalculator
{
	public class WebDriverCalculatorTests
	{
		private WebDriver driver;
		private IWebElement field1;
		private IWebElement field2;
		private IWebElement operation;
		private IWebElement calculate;
		private IWebElement reset;
		private IWebElement resultField;

		[OneTimeSetUp]
		public void OpenAndNavigate()
		{
			this.driver = new ChromeDriver();
			driver.Url = "https://number-calculator.nakov.repl.co/";
			driver.Manage().Window.Maximize();
		}

		[OneTimeTearDown]
		public void ShutDown()
		{
			driver.Quit();
		}

		[TestCase("5", "+", "6", "Result: 11")]
		[TestCase("-3", "+", "-5", "Result: -8")]
		[TestCase("-3", "+", "5", "Result: 2")]
		[TestCase("3", "+", "-5", "Result: -2")]
		[TestCase("6", "-", "5", "Result: 1")]
		[TestCase("3", "-", "5", "Result: -2")]
		[TestCase("-3", "-", "-5", "Result: 2")]
		[TestCase("-5", "-", "3", "Result: -8")]
		[TestCase("5", "*", "3", "Result: 15")]
		[TestCase("-5", "*", "-3", "Result: 15")]
		[TestCase("-5", "*", "3", "Result: -15")]
		[TestCase("5", "*", "-3", "Result: -15")]
		[TestCase("5", "/", "3", "Result: 1.66666666667")]
		[TestCase("-5", "/", "3", "Result: -1.66666666667")]
		[TestCase("5", "/", "-3", "Result: -1.66666666667")]
		[TestCase("-5", "/", "-3", "Result: 1.66666666667")]
		[TestCase("3", "/", "5", "Result: 0.6")]
		[TestCase("3", "/", "-5", "Result: -0.6")]
		[TestCase("-3", "/", "5", "Result: -0.6")]
		[TestCase("6", "/", "3", "Result: 2")]
		[TestCase("-6", "/", "-3", "Result: 2")]
		[TestCase("-6", "/", "3", "Result: -2")]
		[TestCase("3", "/", "0", "Result: Infinity")]
		[TestCase("0", "/", "3", "Result: 0")]
		[TestCase("5", "", "6", "Result: invalid operation")]
		[TestCase("", "+", "6", "Result: invalid input")]
		[TestCase("5", "+", "", "Result: invalid input")]
		[TestCase("hello", "+", "hello", "Result: invalid input")]
		[TestCase("hello", "-", "hello", "Result: invalid input")]
		[TestCase("hello", "*", "hello", "Result: invalid input")]
		[TestCase("hello", "/", "hello", "Result: invalid input")]
		[TestCase("5", "+", "hello", "Result: invalid input")]
		[TestCase("5", "-", "hello", "Result: invalid input")]
		[TestCase("5", "*", "hello", "Result: invalid input")]
		[TestCase("5", "/", "hello", "Result: invalid input")]
		[TestCase("hello", "+", "5", "Result: invalid input")]
		[TestCase("hello", "-", "5", "Result: invalid input")]
		[TestCase("hello", "*", "5", "Result: invalid input")]
		[TestCase("hello", "/", "5", "Result: invalid input")]
		public void Test_Calculator(string firstNum, string operat, string secondNum, string expectedResult)
		{
			field1 = driver.FindElement(By.Id("number1"));
			field2 = driver.FindElement(By.Id("number2"));
			operation = driver.FindElement(By.Id("operation"));
			calculate = driver.FindElement(By.Id("calcButton"));
			reset = driver.FindElement(By.Id("resetButton"));
			resultField = driver.FindElement(By.Id("result"));

			field1.SendKeys(firstNum);
			operation.SendKeys(operat);
			field2.SendKeys(secondNum);

			calculate.Click();

			Assert.That(expectedResult, Is.EqualTo(resultField.Text));

			reset.Click();
		}
	}
}