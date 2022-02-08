using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;


        string[] lines;
        public Form1()
        {
            InitializeComponent();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try {
                driver.Navigate().GoToUrl("https://hsp.moh.gov.sa/");
                
                IWebElement user = driver.FindElement(By.XPath("/html/body/div[1]/form/div[3]/div/div/div/div/div[2]/div/input"));
                IWebElement password = driver.FindElement(By.XPath("/html/body/div[1]/form/div[3]/div/div/div/div/div[3]/div/input"));
         
                user.SendKeys("skofahi");
                password.SendKeys("68s@K2zJVQ");
            }
            catch (WebDriverTimeoutException)
            {
                label1.Text = "hey hey";
                driver.FindElement(By.TagName("body")).SendKeys("Keys.ESCAPE");
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {

            foreach(string line in lines)
            {
                try { driver.Navigate().GoToUrl("https://eservices.moh.gov.sa/CoronaVaccineRegistrationMA/Home");
                    Task.Delay(2000);
                    IWebElement talabat = driver.FindElement(By.XPath("/html/body/div[2]/div/aside/div[2]/nav/ul/li/ul/li[2]/a/span"));
                    talabat.Click();
                    Task.Delay(1000);
                    IWebElement daleh = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div/div/div[2]/div[1]/div[1]/input"));
                    daleh.SendKeys(line);
                    IWebElement search = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div/div/div[2]/div[6]/button[1]"));
                    search.Click();
                    IWebElement khaiart = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div/div/div[2]/table[2]/tbody/tr/th[2]"));
                    driver.Navigate().GoToUrl("https://eservices.moh.gov.sa/CoronaVaccineRegistrationMA/VaccineRequest/Action/" + khaiart.GetAttribute("innerHTML"));
                    
                  


               
                }
                catch (WebDriverTimeoutException) { }
            }
            
          
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            lines = textBox1.Text.Split('\n');
            foreach (string line in lines)
                listBox1.Items.Add(line);

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void ListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            driver.Close();
            driver.Quit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
           IWebElement ejra2 = driver.FindElement(By.CssSelector("#select2-Model_Status-container"));
            ((IJavaScriptExecutor)driver).ExecuteScript(script: "arguments[0].scrollIntoView(true)", ejra2);
            ejra2.Click();
            Task.Delay(500);
            driver.FindElement(By.XPath("/html/body/span/span/span[2]/ul/li[2]")).Click();
            IWebElement submit = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div[9]/button"));
            ((IJavaScriptExecutor)driver).ExecuteScript(script: "arguments[0].scrollIntoView(true)", submit);
            submit.Click();
            
           

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Task.Delay(2000).Wait();
            IWebElement NVR = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div[9]/button[2]"));
            NVR.Click();
        }
    }
}
