﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    public class Figure
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Figure()
        {
            
        }

        public Figure(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
        }
    }
}