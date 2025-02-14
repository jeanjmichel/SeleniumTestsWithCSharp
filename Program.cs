using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

class Program
{
    static void Main()
    {
        // Configuração do WebDriver
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless"); // Roda sem abrir o navegador
        options.AddArgument("--disable-gpu");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        using (IWebDriver driver = new ChromeDriver(options))
        {
            try
            {
                // Acessa o site www.pudim.com.br
                string url = "http://www.pudim.com.br";
                driver.Navigate().GoToUrl(url);

                // Aguarda até 10 segundos para garantir que a página carregue
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElement(By.TagName("body"))); // Espera até o <body> carregar

                // Vamos inspecionar toda a página para ver se existe a div 'email'
                string pageSource = driver.PageSource;
                Console.WriteLine("Código-fonte da página:");
                Console.WriteLine(pageSource);

                // Tenta localizar a div com classe "email"
                try
                {
                    IWebElement emailDiv = driver.FindElement(By.ClassName("email"));

                    // Obtém o link dentro dessa div
                    IWebElement emailLink = emailDiv.FindElement(By.TagName("a"));
                    string emailTexto = emailLink.Text;

                    Console.WriteLine($"Descrição do link do e-mail: {emailTexto}");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("A classe 'email' não foi encontrada na página.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
            finally
            {
                // Fecha o navegador
                driver.Quit();
            }
        }
    }
}
