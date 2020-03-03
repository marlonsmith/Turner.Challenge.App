using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turner.Challenge.App.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsOnScreen { get; set; }
        public string RoleType { get; set; }
        public bool IsKey { get; set; }
    }
}
