using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestingFileUtilities.TypeGenerator
{
    class FieldDeclarationSyntaxParser
    {
        public static IReadOnlyCollection<MyRootType> Parse(IReadOnlyCollection<FieldDeclarationSyntax> fieldDeclarationSyntaxList)
        {
            var results = new List<MyRootType>();
            foreach (var fieldDeclarationSyntax in fieldDeclarationSyntaxList)
            {
                var type = Parse(fieldDeclarationSyntax);
                if (type != null)
                {
                    results.Add(type);
                }
            }

            return results;
        }
        private static MyRootType Parse(FieldDeclarationSyntax fieldDeclarationSyntax)
        {
            var list = fieldDeclarationSyntax.Declaration.Variables.ToList();
            if (list.Count == 0)
            {
                return null;
            }

            var variableDeclarationSyntax = list.First();
            if (variableDeclarationSyntax.Initializer == null)
            {
                return null;
            }

            var fieldName = variableDeclarationSyntax.Identifier.Text;

            var classDeclarationSyntax = GetClassDeclarationSyntax(fieldDeclarationSyntax);
            var className = classDeclarationSyntax.Identifier.Text;
            var namespaceName = GetNamespaceDeclarationSyntax(fieldDeclarationSyntax);

            var fieldValueSyntax = variableDeclarationSyntax.Initializer.Value;

            if (fieldValueSyntax is AnonymousObjectCreationExpressionSyntax anonymousObjectCreationExpressionSyntax)
            {
                var types = AnonymousObjectCreationParser.Parse(
                    new[] { fieldName },
                    anonymousObjectCreationExpressionSyntax);
                
                var rootType = new MyRootType(
                    namespaceName,
                    className,
                    types.Last().Properties,
                    types.Take(types.Count - 1).ToArray());

                return rootType;
            }

            return null;
        }

        private static ClassDeclarationSyntax GetClassDeclarationSyntax(FieldDeclarationSyntax fieldDeclarationSyntax)
        {
            var classDeclarationSyntax = fieldDeclarationSyntax.Parent;
            while (classDeclarationSyntax is ClassDeclarationSyntax == false)
            {
                if (classDeclarationSyntax == null)
                {
                    throw new InvalidOperationException();
                }

                classDeclarationSyntax = classDeclarationSyntax.Parent;
            }

            return (ClassDeclarationSyntax)classDeclarationSyntax;
        }


        private static string GetNamespaceDeclarationSyntax(FieldDeclarationSyntax fieldDeclarationSyntax)
        {
            var namespaceList = new List<string>();

            var node = fieldDeclarationSyntax.Parent;
            while (node != null)
            {
                if (node is NamespaceDeclarationSyntax namespaceDeclarationSyntax)
                {
                    var namespaceName = namespaceDeclarationSyntax.Name.ToFullString().Replace("\r","");
                    namespaceName = namespaceName.Replace("\r", "");
                    namespaceName = namespaceName.Replace("\n", "");
                    namespaceName = namespaceName.Replace(" ", "");
                    namespaceName = namespaceName.Replace("\t", "");
                    namespaceList.Add(namespaceName);
                } 
                node = node.Parent;
            }

            return string.Join(".", namespaceList);
        }
    }


    class AnonymousObjectCreationParser
    {

        public static IReadOnlyCollection<MyType> Parse(string[] parentFieldNames, AnonymousObjectCreationExpressionSyntax anonymousObjectCreationExpressionSyntax)
        {
            var allTypes = new List<MyType>();
            var properties = new List<MyProperty>();
            foreach (var initializer in anonymousObjectCreationExpressionSyntax.Initializers)
            {
                var name = initializer.NameEquals.Name.Identifier.Text;

                if (initializer.Expression is AnonymousObjectCreationExpressionSyntax subDir)
                {
                    var subDirTypes =
                        AnonymousObjectCreationParser.Parse(parentFieldNames.Concat(new[] { name }).ToArray(), subDir);
                    allTypes.AddRange(subDirTypes);

                    var className = subDirTypes.Last()?.Name ?? throw new InvalidOperationException();
                    var property = new MyProperty(
                        name,
                        className,
                        $"new {className}({parentFieldNames.First()}.{name})");
                    properties.Add(property);
                }
                else
                {
                    var property = new MyProperty(name, "IPhysicalFile", $"{parentFieldNames.First()}.{name}");
                    properties.Add(property);
                }
            }

            var newClassName = parentFieldNames.Last() + "Type";
            var counter = 2;
            while (allTypes.Any(_ => _.Name == newClassName))
            {
                newClassName = parentFieldNames.Last() + $"Type{counter}";
                counter++;
            }

            allTypes.Add(new MyType(newClassName, properties));
            return allTypes;
        }
    }
}