using System.Collections.Generic;

namespace TestingFileUtilities.TypeGenerator
{
    class MyType
    {
        public MyType(string name, IReadOnlyCollection<MyProperty> properties)
        {
            Name = name;
            Properties = properties;
        }

        public string Name { get; }
        public IReadOnlyCollection<MyProperty> Properties { get; }

        public override string ToString()
        {
            return $"class {Name}";
        }
    }
}