namespace TestingFileUtilities.TypeGenerator
{
    class MyProperty
    {
        public MyProperty(string name, string type, string expression)
        {
            Name = name;
            Type = type;
            Expression = expression;
        }

        public string Name { get; }
        public string Type { get; }
        public string Expression { get; }

        public override string ToString()
        {
            return $"{Type} {Name} {{ get; }} = {Expression}";
        }
    }
}