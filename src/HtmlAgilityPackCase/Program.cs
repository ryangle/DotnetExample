// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using System.Net;

Console.WriteLine("Hello, World!");
var url = "http://www.czce.com.cn/cn/exchange/jyxx/hq/hq20050509.html";
var jsUrl = "http://www.czce.com.cn/cZD7JRRQ6byg/gYcDL0OnrqA2.9887446.js";
CookieContainer cookies1 = new CookieContainer();
HttpClientHandler handler1 = new HttpClientHandler();
handler1.CookieContainer = cookies1;
handler1.UseCookies = true;
handler1.AllowAutoRedirect = true;

HttpClient httpClient1 = new HttpClient(handler1);

httpClient1.DefaultRequestHeaders.Add("Connection", "keep-alive");
httpClient1.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
httpClient1.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
httpClient1.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate");
httpClient1.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36 Edg/100.0.1185.50");

Uri site = new Uri(jsUrl);
var response1 = httpClient1.SendAsync(new HttpRequestMessage(HttpMethod.Get, site)).Result;
Console.WriteLine($"cookies count:{cookies1.Count}");
var setCookies = response1.Headers.GetValues("Set-Cookie");
Console.WriteLine($"SetCookie:{setCookies.First()}");
cookies1.GetAllCookies().ToList().ForEach(cookie =>Console.WriteLine($"cookies1:{cookie}"));
//foreach (var setCookie in setCookies)
//{
//    cookies.Add(new Cookie(setCookie));
//}
//httpClient.DefaultRequestHeaders.Add("Cookie", setCookies.First());

//cookies.Add(setCookies);

//CookieContainer cookies2 = new CookieContainer();
HttpClientHandler handler2 = new HttpClientHandler();
handler2.CookieContainer = cookies1;
handler2.UseCookies = true;
handler2.AllowAutoRedirect = true;

HttpClient httpClient2 = new HttpClient(handler2);

httpClient2.DefaultRequestHeaders.Add("Connection", "keep-alive");
httpClient2.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
httpClient2.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
httpClient2.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate");
httpClient2.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36 Edg/100.0.1185.50");

var response12 = httpClient2.SendAsync(new HttpRequestMessage(HttpMethod.Get, site)).Result;

Console.WriteLine("Hello, World!");

public class MyWebClient
{
    //The cookies will be here.
    private CookieContainer _cookies = new CookieContainer();

    //In case you need to clear the cookies
    public void ClearCookies()
    {
        _cookies = new CookieContainer();
    }

    public HtmlDocument? GetPage(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";

        //Set more parameters here...
        //...

        //This is the important part.
        request.CookieContainer = _cookies;
        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();

            //When you get the response from the website, the cookies will be stored
            //automatically in"_cookies".

            using (var reader = new StreamReader(stream))
            {
                string html = reader.ReadToEnd();
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc;
            }
        }
        catch (Exception)
        {
            return null;
        }

    }
}
