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

            LAV listOfActiveVertices = new LAV();
            while (reader.IsNotEmpty())
                listOfActiveVertices.Add(reader.GetCurrentVertex());


            SSLOperations.ComputeAngleBisectors(listOfActiveVertices);
            List<LineSegment> result = SSLOperations.GenerateSkeleton(listOfActiveVertices);
            /*

  
    <x>2</x>
    <y>6</y>
  </vertex>
  <vertex>
    <x>2</x>
    <y>2</y>
  </vertex>
  <vertex>
    <x>15</x>
    <y>2</y>
  </vertex>
  <vertex>
    <x>15</x>
    <y>6</y>
  </vertex>
*/

            using (StreamWriter sw = new StreamWriter(output))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<linesegments>");
                foreach (LineSegment ls in result)
                {
                    sw.WriteLine("\t<linesegment>");
                    sw.WriteLine("\t\t<x>" + ls.Start.X + "</x><y>" + ls.Start.Y + "</y>");
                    sw.WriteLine("\t\t<x>" + ls.End.X + "</x><y>" + ls.End.Y + "</y>");
                    sw.WriteLine("\t</linesegment>");
                }
                sw.WriteLine("</linesegments>");
            }

                
        }
    }
}
