using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace TestingFileUtilities.TypeGenerator
{
    [Generator]
    public class FolderTypeGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif

            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var code = @"
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class FolderTypeGeneratorInitialValueAttribute : System.Attribute
    {
    }
";

            context.AddSource(
                $"FolderTypeGeneratorInitialValueAttribute",
                SourceText.From(code, System.Text.Encoding.UTF8));

            if (!(context.SyntaxReceiver is SyntaxReceiver receiver)) return;

            var types = 
                FieldDeclarationSyntaxParser.Parse(receiver.TargetFieldDeclarationSyntaxList);

            var text = new Formatter(types).TransformText();

            context.AddSource(
                $"FolderType.Generated",
                SourceText.From(text, System.Text.Encoding.UTF8));
        }
    }
}
