using PuppeteerSharp;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void  button1_Click(object sender, EventArgs e)
        {
           

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Task.WaitAll(
                Task.Run(async () =>
                {
                    using var browserFetcher = new BrowserFetcher();
                    await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

                    var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                    {
                        Headless = false
                    });
                    var page = await browser.NewPageAsync();


                    await page.GoToAsync("https://www.bing.com/maps");

                    await page.WaitForSelectorAsync(".searchbox input");
                    await page.FocusAsync(".searchbox input");
                    await page.Keyboard.TypeAsync("CN Tower, Toronto, Ontario, Canada");
                    await page.ClickAsync(".searchIcon");
                    await page.WaitForNavigationAsync();

                    //Store the HTML of the current page
                    string content = await page.GetContentAsync();


                    await page.GoToAsync("https://www.baidu.com");

                    await page.WaitForSelectorAsync("#kw");

                    await page.FocusAsync("#kw");

                    await page.Keyboard.TypeAsync("狗是人类的朋友");

                    await page.ClickAsync(".s_btn");                    

                    await page.WaitForNavigationAsync();



                    




                    // Close the browser
                    await browser.CloseAsync();
                }) );
     

            return;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            var exchangeRate = 0d;

            Task.WaitAll(
           Task.Run(async () =>
           {
               using var browserFetcher = new BrowserFetcher();
               await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

               var browser = await Puppeteer.LaunchAsync(new LaunchOptions
               {
                   Headless = true
               });
               var page = await browser.NewPageAsync();

               var url = "https://www.oanda.com/currency-converter/en/?from=USD&to=CNH&amount=1";

               await page.GoToAsync(url);


               // 取得textbox的值
               var input =await page.QuerySelectorAsync("input.MuiInputBase-input.MuiFilledInput-input[tabindex='4']");

               var value = await (await input.GetPropertyAsync("value")).JsonValueAsync();

               exchangeRate = Convert.ToDouble(value);


               // label Avg 
               var text = await page.QuerySelectorAllAsync("td.MuiTableCell-root.MuiTableCell-head.ts5.MuiTableCell-alignCenter");


          

               // Close the browser
               await browser.CloseAsync();

           }));

            label1.Text = exchangeRate.ToString();
        }

    }
}