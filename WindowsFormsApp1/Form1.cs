using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        readonly IWebDriver driver = new ChromeDriver();
        readonly WebDriverWait wait;
        int counter = 0;


        string[] lines;
        public Form1()
        {
            InitializeComponent();
     
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
                
                driver.FindElement(By.TagName("body")).SendKeys("Keys.ESCAPE");
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Confirmation", typeof(int));
            foreach(string line in lines)
            {
                currentlyOn.Text = line;
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                driver.Navigate().GoToUrl("https://eservices.moh.gov.sa/CoronaVaccineRegistrationMA/Home");
                    Task.Delay(2000);
                    IWebElement talabat = driver.FindElement(By.XPath("/html/body/div[2]/div/aside/div[2]/nav/ul/li/ul/li[2]/a/span"));
                executor.ExecuteScript("arguments[0].click();", talabat);
                Task.Delay(1000);
                    IWebElement daleh = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div/div/div[2]/div[1]/div[1]/input"));
                    daleh.SendKeys(line);
                    IWebElement search = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div/div/div[2]/div[6]/button[1]"));
                executor.ExecuteScript("arguments[0].click();", search);

                    IWebElement khaiart = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div/div/div[2]/table[2]/tbody/tr/th[2]"));
                    driver.Navigate().GoToUrl("https://eservices.moh.gov.sa/CoronaVaccineRegistrationMA/VaccineRequest/Action/" + khaiart.GetAttribute("innerHTML"));

                    Task.Delay(2000).Wait();
                    IWebElement ejra2 = driver.FindElement(By.CssSelector("#select2-Model_Status-container"));
                    ((IJavaScriptExecutor)driver).ExecuteScript(script: "arguments[0].scrollIntoView(true)", ejra2);
                    Task.Delay(1000).Wait();
                    ejra2.Click();


                    Task.Delay(1000).Wait();
                driver.FindElement(By.XPath("/html/body/span/span/span[2]/ul/li[2]")).Click();
               
               
                    try 
                    {
                        IWebElement submit = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div[10]/button"));
                        ((IJavaScriptExecutor)driver).ExecuteScript(script: "arguments[0].scrollIntoView(true)", submit);
                        submit.Click();

                        Task.Delay(5000).Wait();
                        IWebElement NVR = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/form/div/div[2]/div/div[10]/button[2]"));
                        executor.ExecuteScript("arguments[0].click();", NVR);

                        Task.Delay(4000).Wait();
                        IWebElement conf = driver.FindElement(By.XPath("/html/body/div[2]/div/div/main/div[3]/div/div/div[2]/div/h4"));


                        if (conf.GetAttribute("innerHTML").Contains("تم إرسال جميع الجرعات"))
                        {
                            dataGridView1.Rows.Add(line, "تم إرسال جميع الجرعات");
                            dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Green;

                        }
                        else if (conf.GetAttribute("innerHTML").Contains("تم إرسال جرعه واحدة من هذا الطلب"))
                        {
                            dataGridView1.Rows.Add(line, "تم إرسال جرعه واحدة من هذا الطلب");
                            dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dataGridView1.Rows.Add(line, "حدث خطأ أثناء ارسال الجرعات");
                            dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        }
                    }
                
                catch (NoSuchElementException)
                {
                    dataGridView1.Rows.Add(line, "معمول مسبقا");
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.White;
                }
                
                i++;
                total.Text = i + " / " + counter;
                comp.Text = line;
            }
            
          
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            lines = textBox1.Text.Split('\n');
            foreach (string line in lines)
            {
                listBox1.Items.Add(line);
                counter++;
            }

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
        public void CopyToClipboardWithHeaders(DataGridView _dgv)
        {
            //Copy to clipboard
            _dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = _dgv.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            CopyToClipboardWithHeaders(dataGridView1);
        }

        
    }
}
