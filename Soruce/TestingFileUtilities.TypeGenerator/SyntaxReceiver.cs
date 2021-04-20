using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestingFileUtilities.TypeGenerator
{
    internal class SyntaxReceiver : ISyntaxReceiver
    {
        internal List<FieldDeclarationSyntax> TargetFieldDeclarationSyntaxList { get; } =
            new List<FieldDeclarationSyntax>();
        
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is FieldDeclarationSyntax fieldSyntax)
            {
                var hasAttribute =
                    fieldSyntax.AttributeLists.SelectMany(_ => _.Attributes)
                        .Any(_ => _.Name.ToFullString().EndsWith("FolderTypeGeneratorInitialValue"));
                if (hasAttribute == false)
                {
                    return;
                }
                TargetFieldDeclarationSyntaxList.Add(fieldSyntax);
            }
        }
    }
}