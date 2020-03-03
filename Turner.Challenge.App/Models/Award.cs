using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turner.Challenge.App.Models
{
    public class Award
    {
        public bool Won { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string[] Participants { get; set; }
    }
}
