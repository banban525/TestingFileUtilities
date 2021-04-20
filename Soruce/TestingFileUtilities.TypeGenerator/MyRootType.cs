using System.Collections.Generic;

namespace TestingFileUtilities.TypeGenerator
{
    class MyRootType
    {
        public MyRootType(string namespaceName, string name, IReadOnlyCollection<MyProperty> properties, IReadOnlyCollection<MyType> childTypes)
        {
            NamespaceName = namespaceName;
            Name = name;
            Properties = properties;
            ChildTypes = childTypes;
        }

        public string NamespaceName { get; }
        public string Name { get; }
        public IReadOnlyCollection<MyProperty> Properties { get; }
        public IReadOnlyCollection<MyType> ChildTypes { get; }

        public override string ToString()
        {
            return $"MyRootType:{NamespaceName}.{Name}";
        }
    }
}