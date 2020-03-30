﻿using System.Collections.Generic;

namespace TechLider.Models
{
    public class Album
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

        public List<Photo> Photos { get; set; }
        public Album()
        {
            Photos = new List<Photo>();
        }
    }
}