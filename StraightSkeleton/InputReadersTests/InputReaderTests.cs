using System;
using System.Collections.Generic;
using System.Text;

using StraightSkeletonLib;
using InputReaders;

using NUnit.Framework;

namespace InputReadersTests
{
    class InputReaderTests
    {
        /*
         * Instead of worrying about reading an actual XML file or text file, just mock what the XML reader or text reader will do
         * It will parse the input, create Vertex objects, and store them in a data structure
         * All ReadInput needs to know is how to get the Vertex's back from the InputReader
        */

        MOCInputReader inputHelper;
        [SetUp]
        protected void SetUp()
        {
            inputHelper = new MOCInputReader();
            inputHelper.Add(0, 0);
            inputHelper.Add(1, 1);
            inputHelper.Add(2, 2);
            inputHelper.Add(3, 3);
        }
        
        [Test]
        public void TestInputReader()
        {
            InputReader inputReader = new InputReader(inputHelper);
            LAV listOfActiveVertices;
            inputReader.ReadInput(out listOfActiveVertices);

            Vertex v1 = new Vertex(0, 0);
            Vertex v2 = new Vertex(1, 1);
            Vertex v3 = new Vertex(3, 3);

            Vertex startNode = listOfActiveVertices.GetStart();

            Assert.IsTrue(v1.Equals(startNode));
            Assert.IsTrue(startNode.GetPrevVertex().Equals(v3));
            Assert.IsTrue(startNode.GetNextVertex().Equals(v2));
        }
    }

    class MOCInputReader : IInputReader
    {
        private List<Vertex> vertexList;

        private int counter;

        public MOCInputReader()
        {
            vertexList = new List<Vertex>();
            counter = 0;
        }

        public void Add(double x, double y)
        {
            Vertex v = new Vertex(x, y);
            vertexList.Add(v);
        }

        public bool IsNotEmpty()
        {
            return counter < vertexList.Count;
        }

        public Vertex GetCurrentVertex()
        {
            Vertex v = vertexList[counter];
            counter++;
            return v;
        }
    }
}
