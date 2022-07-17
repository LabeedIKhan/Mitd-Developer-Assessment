using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuranApp.Models
{
    public class Verse
    {
        public string text { get; set; }

        public Verse(string text)
        {
            this.text = text;
        }

    }
}
