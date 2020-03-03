using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turner.Challenge.App.Models
{
    public class MovieTitle
    {
        public ObjectId Id { get; set; }
        public int TitleId { get; set; }
        public string Name { get; set; }
        public string NameSortable { get; set; }
        public int ReleaseYear { get; set; }
    }
}
