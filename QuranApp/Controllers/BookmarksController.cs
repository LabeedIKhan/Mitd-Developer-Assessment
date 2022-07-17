using Microsoft.AspNetCore.Mvc;
using QuranApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuranApp.Controllers
{
    public class BookmarksController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable < Bookmarks >ieb  = Bookmarks.bm_list;
            return View(ieb);
        }

        public IActionResult RemoveBookmarks(string Id)
        {
            Bookmarks v = new Bookmarks(Id);

            foreach( var val in Bookmarks.bm_list)
            {
                if (val.Equals(v.verse))
                {
                    Bookmarks.bm_list.Remove(val);
                }
            }


            return Ok("Bookmark Removed Please go back on bookmarks page ");
        }
    }
}
