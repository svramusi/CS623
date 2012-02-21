using System;

namespace StraightSkeletonLib
{
    public class IntersectionQueue
    {
        private Element head;
        private Element last;

        public IntersectionQueue()
        {
            head = null;
            last = null;
        }

        public double GetLast()
        {
            return last.Distance;
        }

        private void UpdateLast()
        {
            while (last.Parent != null && last.Parent.Right == last)
                last = last.Parent;

            if (last.Parent != null)
            {
                last = last.Parent;
                last = last.Right;
            }

            while (last.Left != null)
                last = last.Left;
        }

        private void GetPredecessor()
        {
            while (last.Parent != null && last.Parent.Left == last)
                last = last.Parent;

            if (last.Parent != null)
            {
                last = last.Parent;
                last = last.Left;
            }

            while (last.Right != null)
                last = last.Right;

            last = last.Parent;
        }

        public void Push(double distance)
        {
            if (head == null)
            {
                head = new Element(distance, null);
                last = head;
            }
            else
            {
                if (last.Right != null)
                {
                    UpdateLast();
                }
                
                Element newElement = new Element(distance, last);

                if (last.Left == null)
                    last.Left = newElement;
                else if (last.Right == null)
                    last.Right = newElement;
                
                bool resetLast = false;
                if (last == head)
                    resetLast = true;
                
                if (newElement < newElement.Parent)
                    Heapify(newElement);
                
                if (resetLast)
                    last = head;
            }
        }

        private void FixLeft(Element node, Element newLeft, bool updateLast)
        {
            Element origLeft = node.Left;
            Element origRight = node.Right;

            node.Left = newLeft;
            node.Right = newLeft.Right;

            FixHelper(node, newLeft, origLeft, origRight, updateLast);
        }

        private void FixRight(Element node, Element newRight, bool updateLast)
        {
            Element origLeft = node.Left;
            Element origRight = node.Right;
            
            node.Right = newRight;
            node.Left = newRight.Left;

            FixHelper(node, newRight, origLeft, origRight, updateLast);
        }

        private void FixHelper(Element node, Element newHead, Element origLeft, Element origRight, bool updateLast)
        {
            node.Parent = newHead.Parent;

            if (updateLast)
            {
                FixLast(node, newHead);
            }

            FixParents(node, origLeft, origRight, newHead);

            newHead.Left = origLeft;
            newHead.Right = origRight;
        }

        private void FixLast(Element node, Element newHead)
        {
            if (last.Equals(node))
            {
                last = newHead;
            }
            else if (last.Equals(newHead))
            {
                last = node;
            }
        }

        private void FixParents(Element node, Element origLeft, Element origRight, Element newHead)
        {
            if (node.Right != null)
                node.Right.Parent = node;

            if (node.Left != null)
                node.Left.Parent = node;

            if (origLeft != null)
                origLeft.Parent = newHead;

            if (origRight != null)
                origRight.Parent = newHead;
        }

        private void FixGrandParent(Element node)
        {
            if (node.Parent == null)
                return;

            if (node.Parent.Parent == null)
                return;
            
            if (node.Parent.Parent.Left.Equals(node.Parent))
            {
                node.Parent.Parent.Left = node;
            }
            else if (node.Parent.Parent.Right.Equals(node.Parent))
            {
                node.Parent.Parent.Right = node;
            }
        }

        private void FixGrandParent(Element node, Element newChild)
        {
            if (node.Parent == null)
                return;
            
            if (node.Parent.Left.Equals(node))
            {
                node.Parent.Left = newChild;
            }
            else if(node.Parent.Right.Equals(node))
            {
                node.Parent.Right = newChild;
            }
        }

        private void Heapify(Element element)
        {            
            Element currentParent = element.Parent;

            FixGrandParent(element);

            if (element.Parent.Left.Equals(element))
            {
                FixLeft(element, element.Parent, false);
            }
            else
            {
                FixRight(element, element.Parent, false);
            }
            
            if (last.Equals(currentParent))
                last = element;
            else if (last.Equals(element))
                last = currentParent;
            
            if(element.Parent != null && element < element.Parent)
                Heapify(element);

            if (element.Parent == null)
                head = element;
        }

        public double GetMin()
        {
            double retVal = head.Distance;

            if (last.Right == null && last.Left == null)
            {
                if (last == head)
                {
                    head = null;
                    return retVal;
                }
                else
                {
                    GetPredecessor();
                }
            }
            
            Element lastElement = null;
            if (last.Right != null && last.Left != null)
            {
                if (last.Right > last.Left)
                {
                    lastElement = last.Right;
                    last.Right = null;
                }
                else
                {
                    lastElement = last.Left;
                    last.Left = last.Right;
                    last.Right = null;
                }
            }
            else if (last.Right != null && last.Left == null)
            {
                lastElement = last.Right;
                last.Right = null;
            }
            else if (last.Left != null && last.Right == null)
            {
                lastElement = last.Left;
                last.Left = null;
            }
            
            lastElement.Parent = null;
            lastElement.Left = head.Left;
            lastElement.Right = head.Right;

            if (lastElement.Left != null)
                lastElement.Left.Parent = lastElement;

            if (lastElement.Right != null)
                lastElement.Right.Parent = lastElement;

            bool resetLast = false;

            if (last == head)
                resetLast = true;
            
            head = PushDown(lastElement);
            
            if (resetLast)
            {
                last = head;
            }
            else
            {
                if (last.Equals(head))
                {
                    Element current = head;
                    while (current.Left != null)
                        current = current.Left;

                    last = current.Parent;
                }
            }
                        
            return retVal;
        }

        private Element PushDown(Element element)
        {            
            if (element.Left != null && element.Left < element.Right)
            {
                if (element.Left < element)
                {
                    Element newHead = element.Left;

                    FixGrandParent(element, element.Left);
                    FixLeft(element.Left, element, true);

                    PushDown(element);

                    return newHead;
                }
                else
                {
                    return element;
                }
            }
            else
            {
                if (element.Right != null && element.Right < element)
                {
                    Element newHead = element.Right;

                    FixGrandParent(element, element.Right);
                    FixRight(element.Right, element, true);

                    PushDown(element);

                    return newHead;
                }
                else
                {
                    return element;
                }
            }
        }
    }

    class Element
    {
        private double distance;

        private Element parent;
        private Element leftE;
        private Element rightE;

        public Element(double distance, Element parent)
        {
            this.distance = distance;

            this.leftE = null;
            this.rightE = null;
            this.parent = parent;
        }

        public double Distance
        {
            get { return this.distance; }
        }

        public Element Left
        {
            get { return this.leftE; }
            set { this.leftE = value; }
        }

        public Element Right
        {
            get { return this.rightE; }
            set { this.rightE = value; }
        }

        public Element Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        public static bool operator <(Element e1, Element e2)
        {
            if (e2 == null)
                return true;
            else
                return Comparison(e1, e2) < 0;
        }

        public static bool operator >(Element e1, Element e2)
        {
            if (e2 == null)
                return true;
            else
                return Comparison(e1, e2) > 0;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            if (this.distance == ((Element)obj).distance)
                return true;
            else
                return false;
        }

        public static bool operator <=(Element e1, Element e2)
        {
            if (e2 == null)
                return true;
            else
                return Comparison(e1, e2) <= 0;
        }

        public static bool operator >=(Element e1, Element e2)
        {
            if (e2 == null)
                return true;
            else
                return Comparison(e1, e2) >= 0;
        }

        public static int Comparison(Element e1, Element e2)
        {
            if (e1.distance < e2.distance)
                return -1;
            else if (e1.distance == e2.distance)
                return 0;
            else if (e1.distance > e2.distance)
                return 1;

            return 0;
        }
    }
}
