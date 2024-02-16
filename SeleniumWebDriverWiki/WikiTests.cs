using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriverWiki
{
    public class WikiTests
    {
        IWebDriver driver;

        public object ExpectedConditions { get; private set; }

        [SetUp]
        public void Setup()
        {
            //driver = new ChromeDriver();
            var options = new ChromeOptions();
            this.driver = new ChromeDriver(options);

            driver.Manage().Window.Maximize();
            driver.Url = "https://www.wikipedia.org";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Check_Title_Wiki()
        {
            var pageTitle = driver.Title;
            Assert.That("Wikipedia", Is.EqualTo(pageTitle));
        }

        [Test]
        public void testWikititle()
        {
            // test export from Selenium IDE

            driver.Navigate().GoToUrl("https://www.wikipedia.org/");
            driver.Manage().Window.Size = new System.Drawing.Size(1586, 630);
            Assert.That(driver.Title, Is.EqualTo("Wikipedia"));
        }

        [Test]
        public void Test_Wikipedia_Search_Field_By_ID()
        {
            var searchFiled = driver.FindElement(By.Id("searchInput"));
            searchFiled.SendKeys("Pyramid" + Keys.Enter);
                        
            //Thread.Sleep(10000);

            var pageTitle = driver.Title;

            //Assert
            Assert.That("Pyramid - Wikipedia", Is.EqualTo(pageTitle));
        }        

        [Test]
        public void Test_Search_For_Pyramid_Wikipedia()
        {
            // Get elemment by CssSelector

            driver.FindElement(By.CssSelector("input#searchInput")).Click();
            driver.FindElement(By.CssSelector("input#searchInput")).SendKeys("Pyramid");
            driver.FindElement(By.CssSelector("input#searchInput")).SendKeys(Keys.Enter);

            Assert.AreEqual("https://en.wikipedia.org/wiki/Pyramid", driver.Url);
        }

        [Test]
        public void Test_Wikipedia_Search_Field_By_Name()
        {
            // Get elemment by Name - this is not unique !!!
            //Act
            var searchFiled = driver.FindElement(By.Name("search"));
            searchFiled.SendKeys("Pyramid" + Keys.Enter);
            var pageTitle = driver.Title;

            //Assert
            Assert.That("Pyramid - Wikipedia", Is.EqualTo(pageTitle));
        }

        [Test]
        public void Test_Wikipedia_Check_Deutsch()
        {
            // Get elemment by TagName
            var allStrongElements = driver.FindElements(By.TagName("strong"));

            string deutschLinkText = allStrongElements[5].Text;

            Assert.That("Deutsch", Is.EqualTo(deutschLinkText));
        }

        [Test]
        public void Test_Wikipedia_Check_French()
        {
            // Get elemment by TagName
            var allStrongElements = driver.FindElements(By.TagName("strong"));

            var frenchLink = allStrongElements[6];

            frenchLink.Click();

            var pageTitle = driver.Title;

            Assert.That("Wikipédia, l'encyclopédie libre", Is.EqualTo(pageTitle));
        }

        [Test]
        public void Test_Wikipedia_Check_French2()
        {
            // Get elemment by PartialLinkText (search for the word)

            var allStrongElements = driver.FindElements(By.TagName("strong"));

            var frenchLink = driver.FindElement(By.PartialLinkText("Français"));

            frenchLink.Click();

            var pageTitle = driver.Title;

            Assert.That("Wikipédia, l'encyclopédie libre", Is.EqualTo(pageTitle));
        }

        [Test]
        public void Test_Wikipedia_Search_Field_By_Link()
        {
            // Get elemment by LinkText

            var allStrongElements = driver.FindElements(By.TagName("strong"));
            var english = driver.FindElement(By.PartialLinkText("English"));
            english.Click();
            var pageTitle = driver.Title;

            Assert.That("Wikipedia, the free encyclopedia", Is.EqualTo(pageTitle));

            driver.FindElement(By.LinkText("English")).Click();

            var expectedTitle = "English language - Wikipedia";

            Assert.That(expectedTitle, Is.EqualTo(driver.Title));
        }
    }
}