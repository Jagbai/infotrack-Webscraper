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
            WebClient web = new WebClient();
            string CurrURL = scrapeSearch.URL;
            
                String html = web.DownloadString(CurrURL);                          
                MatchCollection m3 = Regex.Matches(html, @"class=""ZINbbc xpd O9g5cc uUPGi""><div class=""kCrYT""><a href=(.+?)""", RegexOptions.Singleline);
                foreach (Match m in m3) 
                {
                    if (m.Groups[1].Value.Contains(scrapeSearch.SearchString))
                        counter++;
                }
            scrapeSearch.Counter = counter;
            
            return View(scrapeSearch);
        }
    }
}