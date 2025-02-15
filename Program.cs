using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Serilog;

class Program
{
    static void Main()
    {
        // Configuring WebDriver
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); // Run in batch mode, without a browser window
        options.AddArgument("--disable-gpu");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        // Configuring the logger
        Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

        Log.Information("Starting the process");

    	/*
         * The motivation of this experiment is accessing a website and get information 
         * to print here in the Console.
         */
        using (IWebDriver driver = new ChromeDriver(options))
        {
            try
            {
                Log.Information("Accessing the URL");
                string url = "http://www.pudim.com.br";
                driver.Navigate().GoToUrl(url);

                // Waiting 10 seconds the page renderize.
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElement(By.TagName("body"))); // Waiting body load

                // Looking for the div 'email' in the page source
                string pageSource = driver.PageSource;
                Console.WriteLine("Page source:");
                Console.WriteLine(pageSource);

                Log.Information("Looking for the div 'email' in the page content");
                try
                {
                    IWebElement emailDiv = driver.FindElement(By.ClassName("email"));
                    IWebElement emailLink = emailDiv.FindElement(By.TagName("a")); //Looking for a '<a href' element
                    string emailTexto = emailLink.Text;
                    Log.Information("Success, the e-mail in the page is: " + emailTexto);
                    Console.WriteLine($"E-mail link: {emailTexto}");
                }
                catch (NoSuchElementException e)
                {
                    Log.Error($"Error, 'email' not found: {e.Message}");
                    Console.WriteLine("The 'email' not found");
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error: {e.Message}");
                Console.WriteLine($"Erro: {e.Message}");
            }
            finally
            {
                Log.Information("Closing the browser instance used");
                driver.Quit();
            }
        }

        Log.Information("Process ended");
    }
}
