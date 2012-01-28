using System;
using System.Collections.Generic;
using System.Text;

using StraightSkeletonLib;

namespace InputReaders
{
    public class InputReader
    {
        IInputReader inputReader;

        public InputReader(IInputReader inputReader)
        {
            this.inputReader = inputReader;
        }

        public void ReadInput(out LAV listOfActiveVertices)
        {
            listOfActiveVertices = new LAV();

            while (inputReader.IsNotEmpty())
            {
                listOfActiveVertices.Add(inputReader.GetCurrentVertex());
            }
        }
    }
}
