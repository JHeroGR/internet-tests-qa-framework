using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace InternetTests;

public class Tests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        driver = new ChromeDriver(options);
    }

    [Test]
    public void TestInternetHomePage()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com");
        Assert.That(driver.Title, Contains.Substring("The Internet"));
    }

    [Test]
    public void TestWebTitlePage()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com");
        var webtitle = driver.FindElement(By.ClassName("heading")).Text;
        Assert.That(webtitle, Is.EqualTo("Welcome to the-internet"));
    }

    [Test]
    public void TestWebSiteLists()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com");
        var listOfWebsites = driver.FindElements(By.CssSelector("#content ul li"));
        Assert.That(listOfWebsites.Count, Is.Not.EqualTo(0));
    }

    [Test]
    public void TestForkImage()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com");
        var forkImage = driver.FindElement(By.TagName("img"));
        Assert.That(forkImage.Displayed, Is.True);
    }

    [Test]
    public void TestABTestingVariationPage()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/abtest");
        var variationText = driver.FindElement(By.TagName("h3")).Text;
        var paragraphText = driver.FindElement(By.TagName("p")).Text;
        Assert.That(variationText, Does.Contain("A/B Test Variation 1"));
        Assert.That(paragraphText, Does.Contain("Also known as split testing. This is a way in which businesses"));
    }

    [Test]
    public void TestNavigationToABTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/");
        var abTestLink = driver.FindElement(By.LinkText("A/B Testing"));
        abTestLink.Click();
        Assert.That(driver.Url, Is.EqualTo("https://the-internet.herokuapp.com/abtest"));
        var variationText = driver.FindElement(By.TagName("h3")).Text;
        Console.WriteLine("Variation Text: " + variationText);
        var paragraphText = driver.FindElement(By.TagName("p")).Text;
        Assert.That(variationText, Does.Contain("A/B Test Variation 1"));
        Assert.That(paragraphText, Does.Contain("Also known as split testing. This is a way in which businesses"));
    }

    [TearDown]
    public void TearDown() => driver.Quit();
}