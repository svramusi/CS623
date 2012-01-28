using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StraightSkeletonLib;

namespace InputReadersHelpers
{
    public interface IInputReadersHelpers
    {
        bool IsNotEmpty();
        Vertex GetCurrentVertex();        
    }
}
