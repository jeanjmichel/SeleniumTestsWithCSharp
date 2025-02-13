using System;
   using OpenQA.Selenium;
   using OpenQA.Selenium.Chrome;
   using OpenQA.Selenium.Support.UI;
   using System.Threading;

   class Program
   {
       static void Main()
       {
           IWebDriver driver = new ChromeDriver();
           string url = "http://www.pudim.com.br";
           driver.Navigate().GoToUrl(url);
           Thread.Sleep(10 * 1000);
           driver.Quit();
       }
   }
