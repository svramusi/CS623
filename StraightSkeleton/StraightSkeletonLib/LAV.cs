using System;
using System.Collections;

namespace StraightSkeletonLib
{
    public class LAV : IEnumerable
    {
        private Vertex startVertex;
        private Vertex endVertex;
        private int length;

        public LAV()
        {
            this.startVertex = null;
            this.endVertex = null;
            this.length = 0;
        }

        public Vertex GetStart()
        {
            return startVertex;
        }

        public void Add(Vertex v)
        {
            if (startVertex == null)
            {
                startVertex = v;
            }
            else
            {
                endVertex.SetNextVertex(v);
                v.SetPrevVertex(endVertex);
                v.SetNextVertex(startVertex);
            }

            length++;

            endVertex = v;
            if(!startVertex.Equals(endVertex))
                startVertex.SetPrevVertex(endVertex);
        }

        public int Length
        {
            get { return this.length; }
        }

        public Vertex Get(int index)
        {
            Vertex currentVertex = this.startVertex;

            int count = 1;
            while (count < index)
            {
                count++;
                currentVertex = currentVertex.GetNextVertex();
            }

            return currentVertex;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public LAVEnum GetEnumerator()
        {
            return new LAVEnum(startVertex, length);
        }
    }

    public class LAVEnum : IEnumerator
    {
        private Vertex startVertex;
        private Vertex currentVertex;

        private int length;
        private int position;

        public LAVEnum(Vertex startVertex, int length)
        {
            this.startVertex = startVertex;
            this.currentVertex = this.startVertex;

            this.length = length;
            this.position = -1;
        }

        public bool MoveNext()
        {
            if(position != -1)
                currentVertex = currentVertex.GetNextVertex();

            position++;

            return (position < length);
        }

        public void Reset()
        {
            position = -1;
            currentVertex = startVertex;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public Vertex Current
        {
            get { return currentVertex; }
        }
    }
}
