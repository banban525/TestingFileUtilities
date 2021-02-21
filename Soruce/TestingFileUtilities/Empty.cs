using System;

namespace TestingFileUtilities
{
    public class Empty : INode
    {
        public string Name => "";
        public IPhysicalNode CreateTo(PhysicalFolder directory)
        {
            throw new NotImplementedException();
        }

        public static bool NotMatch(INode node)
        {
            return node is Empty == false;
        }
    }
}