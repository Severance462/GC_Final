﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_Final
{
    class MazeCell
    {
        public bool[] Walls = new bool[4] { true, true, true, true };
        public bool Visited = false;
    }
}
