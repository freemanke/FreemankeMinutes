using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using Xunit.Abstractions;

namespace FreemankeMinutes.Tests.End2End
{
    public class SeleniumTest
    {
        private ITestOutputHelper _testOutput;

        public SeleniumTest(ITestOutputHelper output)
        {
            _testOutput = output;
        }

        [Fact]
        public void OpenChrome()
        {
            _testOutput.WriteLine("Open chrome");
            var driver = new ChromeDriver(@".\");
            driver.Navigate().GoToUrl("http://tp.cmit.local/");
            Assert.Equal("测试平台 - 自动化测试平台", driver.Title); 

            var query = driver.FindElement(By.LinkText("测试代理守护程序"));
            Assert.NotNull(query);
            driver.Quit();
        }
    }
}