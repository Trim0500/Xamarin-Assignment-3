using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    public class Pet
    {
        public string key { get; set; }
        public string ownerId { get; set; }
        public string name { get; set; }
        public DateTime birthDate { get; set; }
        public string type { get; set; }
    }
}
