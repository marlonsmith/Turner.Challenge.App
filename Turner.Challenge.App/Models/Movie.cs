using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turner.Challenge.App.Models
{
    public class Movie
    {
        public ObjectId Id { get; set; }
        public int TitleId { get; set; }
        public string Name { get; set; }
        public string NameSortable { get; set; }
        public int ReleaseYear { get; set; }
        public Award[] Awards { get; set; }
        public string[] Genres { get; set; }
        public Title[] OtherNames { get; set; }
        public Storyline[] Storylines { get; set; }
        public Participant[] Participants { get; set; }
    }
}
