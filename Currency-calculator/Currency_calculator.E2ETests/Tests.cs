using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Xunit;

namespace Currency_calculator.E2ETests
{
    public class Tests : IDisposable
    {
        public IWebDriver driver;

        public Tests()
        {
            try
            {
                driver = new ChromeDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while starting Chrome: " + ex.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while stopping Chrome: " + ex.Message);
            }
        }

        [Fact]
        public void Test_SignUp()
        {
            SignUp();

            Assert.Equal("http://localhost/currency", driver.Url);
        }

        [Fact]
        public void Test_1()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("1", display);
        }

        [Fact]
        public void Test_12()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            driver.FindElement(By.ClassName("digit-2")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("12", display);
        }

        [Fact]
        public void Test_1plus()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            driver.FindElement(By.ClassName("operator-plus")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("1", display);
        }

        [Fact]
        public void Test_1plus2()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            driver.FindElement(By.ClassName("operator-plus")).Click();
            driver.FindElement(By.ClassName("digit-2")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("2", display);
        }

        [Fact]
        public void Test_1plus2equals3()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            driver.FindElement(By.ClassName("operator-plus")).Click();
            driver.FindElement(By.ClassName("digit-2")).Click();
            driver.FindElement(By.ClassName("operator-equals")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("3", display);
        }

        [Fact]
        public void Test_1Plus2Multiply4div3sub5equals()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            driver.FindElement(By.ClassName("operator-plus")).Click();
            driver.FindElement(By.ClassName("digit-2")).Click();
            driver.FindElement(By.ClassName("operator-multiply")).Click();
            driver.FindElement(By.ClassName("digit-4")).Click();
            driver.FindElement(By.ClassName("operator-divide")).Click();
            driver.FindElement(By.ClassName("digit-3")).Click();
            driver.FindElement(By.ClassName("operator-subtract")).Click();
            driver.FindElement(By.ClassName("digit-5")).Click();
            driver.FindElement(By.ClassName("operator-equals")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("-1", display);
        }

        [Fact]
        public void Test_OperatorAndEqualsOperatorMustBeSeparatedByNumbers()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            driver.FindElement(By.ClassName("operator-divide")).Click();
            driver.FindElement(By.ClassName("digit-0")).Click();
            driver.FindElement(By.ClassName("operator-equals")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("Invalid operation, reset", display);
        }

        [Fact]
        public void Test_AttemptedToDivideByZero()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            driver.FindElement(By.ClassName("operator-plus")).Click();
            driver.FindElement(By.ClassName("operator-equals")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("Invalid operation, reset", display);
        }

        [Fact]
        public void Test_TwoOperatorsMustBeSeparatedByNumbers()
        {
            // Arrange
            SignUp();

            //// Act
            driver.FindElement(By.ClassName("digit-1")).Click();
            driver.FindElement(By.ClassName("operator-plus")).Click();
            driver.FindElement(By.ClassName("operator-plus")).Click();
            Thread.Sleep(1000);
            string display = driver.FindElement(By.ClassName("display")).Text;

            // Assert
            Assert.Equal("Invalid operation, reset", display);
        }

        private void SignUp()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/signup");
            Assert.Equal("http://localhost/signup", driver.Url);
            driver.FindElement(By.Name("email")).SendKeys("test@gmail.com");
            driver.FindElement(By.Name("name")).SendKeys("test");
            driver.FindElement(By.Name("password")).SendKeys("Test1234" + Keys.Enter);
        }
    }
}
