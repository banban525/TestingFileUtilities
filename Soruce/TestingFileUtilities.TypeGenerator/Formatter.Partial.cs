using System;
using System.Collections.Generic;
using System.Text;

namespace TestingFileUtilities.TypeGenerator
{
    public partial class Formatter
    {
        private readonly IReadOnlyCollection<MyRootType> _types;
        internal Formatter(IReadOnlyCollection<MyRootType> types)
        {
            _types = types;
        }
        
    }
}
