using System;
using System.Collections.Generic;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();
            tree.Add(10);
            tree.Add(3);
            tree.Add(2);
            tree.Add(1);
            tree.Add(0);
            tree.Add(17);
            tree.Add(15);
            tree.Add(13);
            tree.Add(14);
        }
    }
}