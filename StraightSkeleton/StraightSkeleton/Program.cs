using System;
using System.Collections.Generic;
using System.IO;

using InputReaders;
using StraightSkeletonLib;

namespace StraightSkeleton
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToUpper() == "-INPUT")
                {
                    input = args[i + 1];
                    i++;
                }
                else if (args[i].ToUpper() == "-OUTPUT")
                {
                    output = args[i + 1];
                    i++;
                }
            }

            IInputReader reader = null;
            if (input.EndsWith(".xml"))
                reader = new XMLReader(input);

            SLAV slav = new SLAV();
            //LAV listOfActiveVertices = new LAV();
            while (reader.IsNotEmpty())
                slav.Insert(reader.GetCurrentVertex(), 0);

            SSLOperations.SetVertexType(slav.Get(0));
            SSLOperations.ComputeAngleBisectors(slav.Get(0));
            List<LineSegment> result = SSLOperations.GenerateSkeleton(slav);

            using (StreamWriter sw = new StreamWriter(output))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<linesegments>");
                foreach (LineSegment ls in result)
                {
                    sw.WriteLine("\t<linesegment>");
                    sw.WriteLine("\t\t<x>" + ls.Start.GetX() + "</x><y>" + ls.Start.GetY() + "</y>");
                    sw.WriteLine("\t\t<x>" + ls.End.GetX() + "</x><y>" + ls.End.GetY() + "</y>");
                    sw.WriteLine("\t</linesegment>");
                }
                sw.WriteLine("</linesegments>");
            }                
        }
    }
}
