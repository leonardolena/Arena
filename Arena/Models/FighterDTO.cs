﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arena.Models
{
    public class FighterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }   
        public int Strength { get; set; }
    }
}
