using System.Text;

namespace NandAndBeyond.Core.Chips;

public abstract class Chip
{
    // Fields
    private string? _id;
    
    // Properties
    public string Name { get; set; }
    
    public string[] Ins { get; set; }
    
    public string[] Outs { get; set; }
    
    public int ArraySize { get; set; }
    
    public bool IsArray { get; set; }
    
    public bool IsResult { get; set; }
    
    public bool Debug { get; set; }
    
    public abstract string InParams { get; }
    
    public abstract string OutParams { get; }
    
    public abstract string Print();

    protected string Id => _id ??= Guid.NewGuid().ToString().Replace("-", string.Empty);

    // Methods
    public string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"CHIP {Name} {{");
        sb.AppendLine(InParams);
        sb.AppendLine($"{OutParams}\n");
        sb.AppendLine("PARTS:");
        sb.AppendLine(Print());
        sb.AppendLine("}");
        return sb.ToString();
    }
    
    protected void Make<TChip>(bool debug, TChip chip) where TChip : Chip
    {
        var projectDirectory = NandCompiler.GetOutputFile(Name, debug);
        if (string.IsNullOrEmpty(projectDirectory))
        {
            Console.WriteLine("// Compilation Failure");
            return;
        }

        using var writer = new StreamWriter(projectDirectory);
        writer.WriteLine("// Compiled via NandAndBeyond.NET");
        writer.WriteLine("// Compiler made by Zach Champeau / Considera Software LLC");
        writer.WriteLine($"// {Name.ToUpper()}");
        writer.Write(chip.Build());
    }
}