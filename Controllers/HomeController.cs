using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webscraper.Models;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Webscraper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Search(ScrapeSearch scrapeSearch)
        {
            int counter = 0;
            string nextpageurl = "";
            WebClient web = new WebClient();
            string CurrURL = scrapeSearch.URL;
            for (int i = 1; i <= scrapeSearch.Pages; i++) {
                String html = web.DownloadString(CurrURL);
                MatchCollection m1 = Regex.Matches(html, scrapeSearch.SearchString, RegexOptions.Singleline);
                counter = counter + m1.Count;
                MatchCollection m2 = Regex.Matches(html, @"<a class=""nBDE1b G5eFlf"" href=""(.+?)"" aria-", RegexOptions.Singleline);
                foreach (Match match in m2)
                {
                    nextpageurl = match.Groups[1].Value;
                }
                nextpageurl = nextpageurl.Replace("amp;","");
                nextpageurl = "https://www.google.com" + nextpageurl;
                CurrURL = nextpageurl;
            }
            scrapeSearch.Counter = counter;
            return View(scrapeSearch);
        }
    }
}