using System;
using System.Collections.Generic;
using System.Xml;

using StraightSkeletonLib;

namespace InputReaders
{
    public class XMLReader : IInputReader
    {
        private List<Vertex> vertexList;
        private int currentVertex;

        public XMLReader(string fileName)
        {
            vertexList = new List<Vertex>();
            currentVertex = 0;

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlNodeList vertices = doc.SelectNodes("/vertices/vertex");
            foreach (XmlNode vertex in vertices)
            {
                XmlNode xNode = vertex.SelectSingleNode("x");
                XmlNode yNode = vertex.SelectSingleNode("y");

                vertexList.Add(new Vertex(Convert.ToDouble(xNode.InnerText), Convert.ToDouble(yNode.InnerText)));
            }
        }

        public bool IsNotEmpty()
        {
            return (currentVertex < vertexList.Count);
        }

        public Vertex GetCurrentVertex()
        {
            currentVertex++;
            return vertexList[currentVertex - 1];
        }
    }
}
