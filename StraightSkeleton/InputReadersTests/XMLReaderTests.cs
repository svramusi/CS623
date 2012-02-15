using System;
using System.Collections.Generic;

using StraightSkeletonLib;
using InputReaders;

using NUnit.Framework;

namespace InputReadersTests
{
    class XMLReaderTests
    {
        [Test]
        public void TestXMLReader()
        {
            XMLReader xmlReader = new XMLReader("Input1.xml");

            List<Vertex> actualVertexList = new List<Vertex>();
            while (xmlReader.IsNotEmpty())
            {
                actualVertexList.Add(xmlReader.GetCurrentVertex());
            }

            List<Vertex> expectedVertexList = new List<Vertex>();
            expectedVertexList.Add(new Vertex(2, 15));
            expectedVertexList.Add(new Vertex(2, 2));
            expectedVertexList.Add(new Vertex(20, 5));
            expectedVertexList.Add(new Vertex(17, 7));
            expectedVertexList.Add(new Vertex(22, 9));
            expectedVertexList.Add(new Vertex(22, 16));
            expectedVertexList.Add(new Vertex(9, 16));
            expectedVertexList.Add(new Vertex(7, 13));
            expectedVertexList.Add(new Vertex(5, 15));

            for (int i = 0; i < expectedVertexList.Count; i++)
            {
                Assert.AreEqual(expectedVertexList[i], actualVertexList[i]);
            }

            Assert.AreEqual(expectedVertexList.Count, actualVertexList.Count);
        }
    }
}
