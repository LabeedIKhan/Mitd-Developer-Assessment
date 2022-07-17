using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuranApp.Models
{
    public class Bookmarks
    {

        public static List<Bookmarks> bm_list = new List<Bookmarks>();

        public string verse { get; set; }

        public Bookmarks(string verse)
        {
            this.verse = verse;
        }

        public void AddBookMarks(Bookmarks bm)
        {
            bm_list.Add(bm);
        }

    }
}
