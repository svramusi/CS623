using System;

using StraightSkeletonLib;

namespace InputReaders
{
    public interface IInputReader
    {
        bool IsNotEmpty();
        Vertex GetCurrentVertex();
    }
}
