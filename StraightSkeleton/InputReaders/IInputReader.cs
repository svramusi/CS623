using System;
using System.Collections.Generic;
using System.Text;

using StraightSkeletonLib;

namespace InputReaders
{
    public interface IInputReader
    {
        bool IsNotEmpty();
        Vertex GetCurrentVertex();
    }
}
