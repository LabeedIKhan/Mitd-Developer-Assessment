using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuranApp.Models
{
    public class Chapter
    {
        public string id { get; set; }
        public string name { get; set; }
        public string place { get; set; }

        public Chapter(string id, string name, string place)
        {
            this.id = id;
            this.name = name;
            this.place = place;
        }
    }
}
