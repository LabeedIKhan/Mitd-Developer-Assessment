using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuranApp.Models;

namespace QuranApp.Controllers
{
    public class ChapterListController : Controller
    {
        static List<Chapter> chapter_list = new List<Chapter>();
        static List<Verse> verse_list = new List<Verse>();
       

        public IActionResult Index()
        {
            GetAllChapters();
            IEnumerable< Chapter> chap_view = chapter_list;
            return View(chap_view);
        }

        public IActionResult OpenChapter(string id)
        {
            GetVerseByChapter(id);
            //return Ok(id);
            IEnumerable<Verse> ver = verse_list;
            return View(ver);
        }

        public IActionResult AddBookmarkList(string Id)
        {

            Bookmarks bm = new Bookmarks(Id);
            bm.AddBookMarks(bm);
            return Ok("Book Mark Added Please go back and press Book mark button on nav bar to see book marks");
            
        }

        public static void GetAllChapters()
        {
            string json = "";

            using (WebClient webc = new WebClient())
            {
                try
                {
                    json = webc.DownloadString("https://api.quran.com/api/v4/chapters?language=en");
                }
                catch (Exception)
                {
                    return;
                }
            }

            JObject jobj = JObject.Parse(json);
            JToken chapters = jobj["chapters"];
            foreach (var x in chapters)
            {
                string id = (string)x["id"];
                string name = (string)x["name_simple"];
                string place = (string)x["revelation_place"];

                Chapter ch = new Chapter(id,name,place);

                chapter_list.Add(ch);
            }
        }

        public static void GetVerseByChapter(string id)
        {
            List<string> for_ver = new List<string>();
            string json = "";
            using (WebClient webc = new WebClient())
            {
                try
                {
                    json = webc.DownloadString("https://api.quran.com/api/v4/verses/by_chapter/" + id + "?language=en&words=true");
                    verse_list.Clear();
                }
                catch (Exception)
                {
                    return;
                }
            }

            JObject jobj = JObject.Parse(json);
            string verses = jobj["verses"].ToString();
            JArray jarr1 = JArray.Parse(verses);

            foreach (JObject x in jarr1)
            {
                string words = x["words"].ToString();
                JArray jarr2 = JArray.Parse(words);

                foreach (JObject y in jarr2)
                {
                    //Verse v = new Verse(y["translation"]["text"].ToString());
                    //verse_list.Add(v);
                   
                    for_ver.Add(y["translation"]["text"].ToString());
                }
            }
            FormtVerses(for_ver);
        }

        public static void FormtVerses(List<string> list)
        {
            List<string> format = new List<string>();

            string temp = "";
            foreach (var ver in list)
            {
                if (!IsVerseNumber(ver))
                {
                    temp += ver;
                }
                else
                {
                    format.Add(temp);
                    temp = "";
                }
            }

            foreach (var v in format)
            {
                Verse ve = new Verse(v);
                verse_list.Add(ve);
            }
        }

        internal class WebClientCustomize : WebClient
        {
            // This internal class has been created to set
            // timeout though not the best way but 
            // it will cause the webclient to through exception 
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);
                // Timeout will occur after 6 seconds if no response 
                // is recieved from apis within that time 
                request.Timeout = 6000;
                return request;
            }

        }

        public static bool IsVerseNumber(string val)
        {
            List<string> num_list = new List<string>();
            for (var i = 1; i < 300; i++)
            {
                num_list.Add("(" + i.ToString() + ")");
            }
            foreach (var x in num_list)
            {
                if (x.Equals(val))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
